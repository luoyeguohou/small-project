using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using UnityEngine.UI;
using System.Collections.Generic;
using LitJson;

public class NetManager : MonoBehaviour
{
    // net about
    private static TcpClient tcpClient;
    private static NetworkStream networkStream;
    private static StreamReader reader;
    private static StreamWriter writer;
    private static object lockObj = new object();  // 用于锁定共享资源

    public static NetManager inst;
    public Dictionary<int, PlayerData> players = new Dictionary<int, PlayerData>();
    public int uid;
    public int turn = 0;
    public SeedDealer seedDealer;

    void Start()
    {
        // 只会有一份实例
        inst = this;
        DontDestroyOnLoad(inst);

        BindHandler(MsgStr.msg_recv_uid, OnRecvRequestUid);
        BindHandler(MsgStr.msg_update_status, OnRecvUpdateStatus);

        Screen.SetResolution(1920,1080,false);
    }

    public static void Connect(string ipAddress)
    {
        // 连接到服务端
        tcpClient = new TcpClient(ipAddress, 8080); // 修改为你的服务端地址和端口
        networkStream = tcpClient.GetStream();
        reader = new StreamReader(networkStream, Encoding.ASCII);
        writer = new StreamWriter(networkStream, Encoding.ASCII) { AutoFlush = true };

        Debug.Log("Connected to server. Type a message to send:");


        Thread receiveThread = new Thread(ReceiveMessages);
        receiveThread.Start();
    }

    public static void SendMessageToServer(string cmd, string message)
    {
        // 发送消息到服务器
        writer.WriteLine(JsonMapper.ToJson(new MsgRecv(cmd, message)));
    }
    static void ReceiveMessages()
    {
        try
        {
            while (true)
            {
                string message = reader.ReadLine();
                if (message != null)
                {
                    Debug.Log(message);
                    MsgRecv msg = JsonMapper.ToObject<MsgRecv>(message);
                    lock (lockObj)
                    {
                        if (handlers.ContainsKey(msg.cmd))
                        {
                            foreach (ReceiveMessageDelegate handler in new List<ReceiveMessageDelegate>(handlers[msg.cmd]))
                            {
                                handler(msg.data);
                            }
                        }
                    }
                }
            }
        }
        catch (IOException)
        {
            Debug.Log("Connection lost.");
        }
    }


    // msg
    public delegate void ReceiveMessageDelegate(string message);
    private static Dictionary<string, List<ReceiveMessageDelegate>> handlers = new Dictionary<string, List<ReceiveMessageDelegate>>();
    public static void BindHandler(string name, ReceiveMessageDelegate handler)
    {
        lock (lockObj)
        {
            if (!handlers.ContainsKey(name))
            {
                handlers[name] = new List<ReceiveMessageDelegate>();
            }
            handlers[name].Add(handler);
        }

    }

    public static void UnbindHandler(string name, ReceiveMessageDelegate handler)
    {
        lock (lockObj)
        {
            if (!handlers.ContainsKey(name))
            {
                return;
            }
            handlers[name].Remove(handler);
        }
    }

    private void OnRecvRequestUid(string rawData)
    {
        RMsgRequestUid data = JsonMapper.ToObject<RMsgRequestUid>(rawData);
        //set uid
        uid = data.uid;

        foreach (var item in data.players)
        {
            PlayerData d = new PlayerData(item.uid, item.name);
            if (item.data != "")
            {
                d.numbers = JsonMapper.ToObject<IntArray>(item.data);
            }
            Debug.Log("add uid :"+ item.uid.ToString());
            players.Add(item.uid, d);
        }
        MsgHandler.Dispatch(Message.UpdateView);
    }

    private void OnRecvUpdateStatus(string rawData)
    {
        MsgUpdateStatus data = JsonMapper.ToObject<MsgUpdateStatus>(rawData);
        // update status
        if (!players.ContainsKey(data.uid))
        {
            Debug.Log("add uid :" + data.uid.ToString());
            players.Add(data.uid, new PlayerData(data.uid, data.name));
        }
        var item = players[data.uid];
        Debug.Log(data.data);
        item.numbers = data.data == "" ? new IntArray( new int[0] ): JsonMapper.ToObject<IntArray>(data.data);
        Debug.Log(item.numbers.numbers.Length);
        item.name = data.name;
        players[data.uid] = item;
        Debug.Log(data.uid);
        Debug.Log(players[2].numbers.numbers.Length);
        // ui update view
        MsgHandler.Dispatch(Message.UpdateView);
    }

    public void ApplySeed(int seed)
    {
        seedDealer = new SeedDealer();
        seedDealer.Deal(seed);
    }

}


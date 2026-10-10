using System.Collections.Generic;
public struct PlayerData
{
    public IntArray numbers;
    public int uid;
    public string name;
    public PlayerData(int uid,string name) { 
        this.uid = uid;
        this.name = name;
        numbers = new IntArray();
    }
}

public struct PlayerRawData {

    public string data;
    public int uid;
    public string name;
}

public struct MsgRecv
{
    public string cmd;
    public string data;
    public MsgRecv(string cmd,string data) { 
        this.cmd = cmd;
        this.data = data;
    }
}

public class IntArray { 
    public int[] numbers = new int[0];
    public IntArray(int[] numbers)
    { 
        this.numbers = numbers;
    }
    public IntArray() { 
    
    }
}

public struct RMsgRequestUid
{
    public int uid;
    public string name;
    public PlayerRawData[] players;
}

public struct MsgRequestUid
{
    public string cmd;
    public string name;
    public MsgRequestUid(string name)
    {
        cmd = MsgStr.msg_recv_uid;
        this.name = name;
    }
}

public struct MsgUpdateStatus
{
    public int uid;
    public string data;
    public string name;
    public MsgUpdateStatus(int uid, string data,string name) { 
        this.uid = uid;
        this.data = data;
        this.name = name;
    }
}
public class MsgStr { 
    public const string msg_recv_uid = "RequestUid";
    public const string msg_update_status = "UpdateStatus";
}
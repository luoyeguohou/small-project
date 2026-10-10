using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using LitJson;

public class BtnEnterRoom : MonoBehaviour
{
    public TMP_InputField inputField;
    private bool readyNextScene = false;
    public void EnterRoom()
    {
        string name = inputField.text;
        NetManager.BindHandler(MsgStr.msg_recv_uid, onRecvMsg);
        // send msg
        NetManager.SendMessageToServer(MsgStr.msg_recv_uid,name);
    }

    private void onRecvMsg(string rawData)
    {
       
        RMsgRequestUid data = JsonMapper.ToObject<RMsgRequestUid>(rawData);
        //set uid
        NetManager.inst.uid = data.uid;
        NetManager.UnbindHandler(MsgStr.msg_recv_uid, onRecvMsg);
        readyNextScene = true;
    }

    private void Update()
    {
        if (readyNextScene) { 
            SceneManager.LoadScene(2);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Message { 
    UpdateView
}

public class MsgHandler : MonoBehaviour
{
    private static Dictionary<Message,List<Action>> handlers = new Dictionary<Message,List<Action>>();
    private  static List<Message> msg = new List<Message>();
    public static void Bind(Message m,Action handler) {
        if (!handlers.ContainsKey(m)) {
            handlers[m] = new List<Action>();
        }
        handlers[m].Add(handler);
    }

    public static void Dispatch(Message m) {
        msg.Add(m);
    }

    private void Update()
    {
        foreach (var m in msg)
        {
            if (!handlers.ContainsKey(m)) return;
            foreach (var item in handlers[m])
            {
                item();
            }
        }
        msg.Clear();
    }
}

using FairyGUI;
using FairyGUI.Utils;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public partial class Game1 : GComponent
    {
        public MapWithName[] maps;

        public override void ConstructFromResource()
        {
            base.ConstructFromResource();

            cont.btnUpload.onClick.Add(Upload);
            cont.btnApplySeed.onClick.Add(ApplySeed);
            maps = new MapWithName[6] { cont.map1, cont.map2, cont.map3, cont.map4, cont.map5, cont.map6 };
            MsgHandler.Bind(Message.UpdateView, UpdateView);
            UpdateView();
        }

        public void UpdateView()
        {
            Debug.Log("game update view");
            // map
            cont.playerNum.selectedIndex = NetManager.inst.players.Count - 1;

            maps[0].SetUid(NetManager.inst.uid);
            Debug.Log("first uid : " + NetManager.inst.uid.ToString());
            int idx = 1;
            foreach (var item in NetManager.inst.players)
            {
                if (item.Key != NetManager.inst.uid)
                {
                    Debug.Log(NetManager.inst.players[2].numbers.numbers.Length);
                    Debug.Log("then uid : " + item.Key.ToString());
                    maps[idx].SetUid(item.Key);
                    idx++;
                }
            }
        }

        public void ApplySeed()
        {
            NetManager.inst.ApplySeed(int.Parse(cont.txtSeed.text));
            cont.cardPanel.UpdateView();
            cont.taskPanel.UpdateView();
        }

        public void Upload()
        {
            MsgUpdateStatus data = new MsgUpdateStatus(NetManager.inst.uid, cont.map1.GetDataStr(), NetManager.inst.players[NetManager.inst.uid].name);
            NetManager.SendMessageToServer(MsgStr.msg_update_status, JsonMapper.ToJson(data));
        }

        public void OnRecvUpdateStatus()
        {
            foreach (var item in maps)
            {
                item.UpdateView();
            }
        }
    }
}
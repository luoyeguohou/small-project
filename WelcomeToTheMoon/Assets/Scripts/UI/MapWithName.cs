using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Main
{
    public partial class MapWithName : GComponent
    {
        private int uid;

        public void SetUid(int uid) { 
            this.uid = uid;
            UpdateView();
        }

        public string GetDataStr() { 
             return map.GetDataStr();
        }

        public void UpdateView()
        { 
            PlayerData data = NetManager.inst.players[uid];
            txtName.SetVar("name", data.name).FlushVars();
            map.UpdateView(data.numbers.numbers);
        }
    }
}
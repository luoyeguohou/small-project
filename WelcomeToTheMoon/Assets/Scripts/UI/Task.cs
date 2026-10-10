using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

namespace Main
{
    public partial class Task : GComponent
    {
        public void UpdateView()
        {
            task1.selectedIndex = NetManager.inst.seedDealer.Tasks[0];
            task2.selectedIndex = NetManager.inst.seedDealer.Tasks[1];
            task3.selectedIndex = NetManager.inst.seedDealer.Tasks[2];
        }
    }
}
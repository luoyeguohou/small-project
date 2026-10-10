using FairyGUI;
using Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIPackage.AddPackage("UI/Main");
        MainBinder.BindAll();
        GComponent gcom = UIPackage.CreateObject("Main", "Game1").asCom;
        GRoot.inst.AddChild(gcom);
        gcom.MakeFullScreen();
    }
}

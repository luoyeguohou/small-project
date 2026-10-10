using FairyGUI;
using JetBrains.Annotations;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public partial class Map1 : GComponent
    {
        public GTextInput[] inputFileds;
        public BtnChangeIcon2[] btnChangeIcon2s;
        public BtnChangeIcon3[] btnChangeIcon3s;

        public override void ConstructFromResource()
        {
            base.ConstructFromResource();
            inputFileds = new GTextInput[55];
            for (int i = 0; i <= 54; i++)
            {
                inputFileds[i] = (GTextInput)GetChild("n" + i.ToString());
            }
            btnChangeIcon2s = new BtnChangeIcon2[54];
            for (int i = 55; i <= 108; i++)
            {
                btnChangeIcon2s[i - 55] = (BtnChangeIcon2)GetChild("n" + i.ToString());
            }
            btnChangeIcon3s = new BtnChangeIcon3[3];
            for (int i = 109; i <= 111; i++)
            {
                btnChangeIcon3s[i - 109] = (BtnChangeIcon3)GetChild("n" + i.ToString());
            }
        }

        public void UpdateView(int[] numbers)
        {
            Debug.Log(numbers.Length);
            for (int i = 0; i <= 54; i++)
            {
                if (numbers.Length > i)
                    inputFileds[i].text = numbers[i] == 0 ? "" : numbers[i].ToString();
            }
            for (int i = 55; i <= 108; i++)
            {
                if (numbers.Length > i)
                    btnChangeIcon2s[i - 55].icon.selectedIndex = numbers[i];
            }
            for (int i = 109; i <= 111; i++)
            {
                if (numbers.Length > i)
                    btnChangeIcon3s[i - 109].icon.selectedIndex = numbers[i];
            }
        }

        public string GetDataStr()
        {
            int[] data = new int[112];
            for (int i = 0; i <= 54; i++)
            {
                data[i] = (inputFileds[i].text == "" ? 0 : int.Parse(inputFileds[i].text));
            }
            for (int i = 55; i <= 108; i++)
            {
                data[i] = (btnChangeIcon2s[i - 55].icon.selectedIndex);
            }
            for (int i = 109; i <= 111; i++)
            {
                data[i] = (btnChangeIcon3s[i - 109].icon.selectedIndex);
            }
            return JsonMapper.ToJson(new IntArray(data));
        }
    }
}
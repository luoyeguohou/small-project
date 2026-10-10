/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class BtnChangeIcon3 : GComponent
    {
        public Controller icon;
        public GLoader icon_2;
        public const string URL = "ui://ahvpgb8ni6rga";

        public static BtnChangeIcon3 CreateInstance()
        {
            return (BtnChangeIcon3)UIPackage.CreateObject("Main", "BtnChangeIcon3");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            icon = GetController("icon");
            icon_2 = (GLoader)GetChild("icon");
        }
    }
}
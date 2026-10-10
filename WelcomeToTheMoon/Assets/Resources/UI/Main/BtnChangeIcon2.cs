/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class BtnChangeIcon2 : GComponent
    {
        public Controller icon;
        public Controller iconType;
        public GLoader icon_2;
        public GLoader icon1;
        public const string URL = "ui://ahvpgb8ni6rg8";

        public static BtnChangeIcon2 CreateInstance()
        {
            return (BtnChangeIcon2)UIPackage.CreateObject("Main", "BtnChangeIcon2");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            icon = GetController("icon");
            iconType = GetController("iconType");
            icon_2 = (GLoader)GetChild("icon");
            icon1 = (GLoader)GetChild("icon1");
        }
    }
}
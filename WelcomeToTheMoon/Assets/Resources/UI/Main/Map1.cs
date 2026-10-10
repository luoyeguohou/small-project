/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class Map1 : GComponent
    {
        public GImage bg;
        public const string URL = "ui://ahvpgb8njx0d0";

        public static Map1 CreateInstance()
        {
            return (Map1)UIPackage.CreateObject("Main", "Map1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GImage)GetChild("bg");
        }
    }
}
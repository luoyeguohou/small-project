/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class MapWithName : GComponent
    {
        public Map1 map;
        public GTextField txtName;
        public const string URL = "ui://ahvpgb8ni6rgd";

        public static MapWithName CreateInstance()
        {
            return (MapWithName)UIPackage.CreateObject("Main", "MapWithName");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            map = (Map1)GetChild("map");
            txtName = (GTextField)GetChild("txtName");
        }
    }
}
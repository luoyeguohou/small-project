/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class Game1Cont : GComponent
    {
        public Controller playerNum;
        public MapWithName map1;
        public MapWithName map2;
        public MapWithName map3;
        public MapWithName map4;
        public MapWithName map5;
        public MapWithName map6;
        public Task taskPanel;
        public GTextInput txtSeed;
        public GButton btnApplySeed;
        public CardArea cardPanel;
        public GButton btnUpload;
        public const string URL = "ui://ahvpgb8ni6rgc";

        public static Game1Cont CreateInstance()
        {
            return (Game1Cont)UIPackage.CreateObject("Main", "Game1Cont");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            playerNum = GetController("playerNum");
            map1 = (MapWithName)GetChild("map1");
            map2 = (MapWithName)GetChild("map2");
            map3 = (MapWithName)GetChild("map3");
            map4 = (MapWithName)GetChild("map4");
            map5 = (MapWithName)GetChild("map5");
            map6 = (MapWithName)GetChild("map6");
            taskPanel = (Task)GetChild("taskPanel");
            txtSeed = (GTextInput)GetChild("txtSeed");
            btnApplySeed = (GButton)GetChild("btnApplySeed");
            cardPanel = (CardArea)GetChild("cardPanel");
            btnUpload = (GButton)GetChild("btnUpload");
        }
    }
}
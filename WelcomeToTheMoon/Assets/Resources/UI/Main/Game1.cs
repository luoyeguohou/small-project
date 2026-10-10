/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class Game1 : GComponent
    {
        public Game1Cont cont;
        public const string URL = "ui://ahvpgb8ni6rg7";

        public static Game1 CreateInstance()
        {
            return (Game1)UIPackage.CreateObject("Main", "Game1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cont = (Game1Cont)GetChild("cont");
        }
    }
}
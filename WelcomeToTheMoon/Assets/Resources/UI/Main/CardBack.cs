/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class CardBack : GComponent
    {
        public Controller cardID;
        public const string URL = "ui://ahvpgb8npvwq4a";

        public static CardBack CreateInstance()
        {
            return (CardBack)UIPackage.CreateObject("Main", "CardBack");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            cardID = GetController("cardID");
        }
    }
}
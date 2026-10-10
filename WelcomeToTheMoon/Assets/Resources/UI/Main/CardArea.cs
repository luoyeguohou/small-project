/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class CardArea : GComponent
    {
        public GButton btnLastTurn;
        public GButton btnNextTurn;
        public CardBack cardBack1;
        public CardBack cardBack2;
        public CardBack cardBack3;
        public CardFront cardFront1;
        public CardFront cardFront2;
        public CardFront cardFront3;
        public GTextField txtTurn;
        public const string URL = "ui://ahvpgb8npvwq4i";

        public static CardArea CreateInstance()
        {
            return (CardArea)UIPackage.CreateObject("Main", "CardArea");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            btnLastTurn = (GButton)GetChild("btnLastTurn");
            btnNextTurn = (GButton)GetChild("btnNextTurn");
            cardBack1 = (CardBack)GetChild("cardBack1");
            cardBack2 = (CardBack)GetChild("cardBack2");
            cardBack3 = (CardBack)GetChild("cardBack3");
            cardFront1 = (CardFront)GetChild("cardFront1");
            cardFront2 = (CardFront)GetChild("cardFront2");
            cardFront3 = (CardFront)GetChild("cardFront3");
            txtTurn = (GTextField)GetChild("txtTurn");
        }
    }
}
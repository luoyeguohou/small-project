/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class CardFront : GComponent
    {
        public Controller Type;
        public const string URL = "ui://ahvpgb8npvwq4h";

        public static CardFront CreateInstance()
        {
            return (CardFront)UIPackage.CreateObject("Main", "CardFront");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Type = GetController("Type");
        }
    }
}
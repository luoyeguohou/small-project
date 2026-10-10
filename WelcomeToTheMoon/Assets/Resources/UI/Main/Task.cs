/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class Task : GComponent
    {
        public Controller task1;
        public Controller task2;
        public Controller task3;
        public Controller finish1;
        public Controller finish2;
        public Controller finish3;
        public const string URL = "ui://ahvpgb8ni6rgk";

        public static Task CreateInstance()
        {
            return (Task)UIPackage.CreateObject("Main", "Task");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            task1 = GetController("task1");
            task2 = GetController("task2");
            task3 = GetController("task3");
            finish1 = GetController("finish1");
            finish2 = GetController("finish2");
            finish3 = GetController("finish3");
        }
    }
}
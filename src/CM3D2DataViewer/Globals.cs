using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class Globals
    {
        public static void Message(string s)
        {
            Form1.Instance.MessageTextBox.AppendText(s+Environment.NewLine);
        }

        public static void Message(string fmt, params object[] args)
        {
            Message(string.Format(fmt, args));
        }
    }
}

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
            var tb  = Form1.Instance.MessageTextBox;
            
            tb.AppendText(s+Environment.NewLine);
            tb.Select(tb.TextLength, 0);
        }

        public static void Message(string fmt, params object[] args)
        {
            Message(string.Format(fmt, args));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM3D2DataViewer
{
    public partial class NewMenuDialog : Form
    {
        public NewMenuControl           NewMenuControl          { get { return newMenuControl1; } }

        public NewMenuDialog()
        {
            InitializeComponent();
        }
    }
}

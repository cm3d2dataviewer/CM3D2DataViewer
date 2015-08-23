using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM3D2DataViewer
{
    public partial class CM3D2MorphControl : UserControl
    {
        private ParamMorph              data;

        public ParamMorph               Data                    { get { return data; } set { SetData(value); } }

        public CM3D2MorphControl()
        {
            InitializeComponent();
        }

        private void SetData(ParamMorph value)
        {
            if(value == data)
                return;

            data    = value;

            UpdateUI();
        }

        private void UpdateUI()
        {
            lvMorphs.Items.Clear();

            if(null == data)
                return;

            foreach(var i in data.Vertices)
            {
                var item= lvMorphs.Items.Add(i.Index.ToString());
                item.Tag= i;
                item.SubItems.Add(i.X.ToString("F10"));
                item.SubItems.Add(i.Y.ToString("F10"));
                item.SubItems.Add(i.Z.ToString("F10"));
                item.SubItems.Add(i.NX.ToString("F10"));
                item.SubItems.Add(i.NY.ToString("F10"));
                item.SubItems.Add(i.NZ.ToString("F10"));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public abstract class Exporter
    {
        public ExportSettings           Settings                { get; private set; }

        public Exporter(ExportSettings settings)
        {
            Settings    = settings;
        }

        public abstract void Export(string filename, ModelFile model);
    }

    public class ExportSettings
    {
        public bool                     Skin                    { get; set; }
        public bool                     Morph                   { get; set; }
        public float                    Scale                   { get; set; }
    }
}

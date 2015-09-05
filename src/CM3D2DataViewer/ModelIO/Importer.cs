using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public abstract class Importer
    {
        public ImportSettings           Settings                { get; private set; }

        public Importer(ImportSettings settings)
        {
            Settings    = settings;
        }

        public abstract void Import(string filename, ModelFile model);

        protected void ChangeShader(ModelFile model)
        {
            if(Settings.ChangeShader && Settings.Shader.Length > 0)
            {
                foreach(var i in model.Materials)
                {
                    i.Descriptions[1]   = "CM3D2/"  + Settings.Shader;
                    i.Descriptions[2]   = "CM3D2__" + Settings.Shader;
                }
            }
        }
    }

    public class ImportSettings
    {
        public ModelFile                RefModel                { get; set; }
        public float                    MorphMin                { get; set; }
        public float                    MorphMax                { get; set; }
        public float                    Scale                   { get; set; }
        public bool                     ChangeShader            { get; set; }
        public string                   Shader                  { get; set; }
    }
}

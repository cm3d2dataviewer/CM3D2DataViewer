using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class ObjImporter : Importer
    {
        public ObjImporter(ImportSettings settings)
            : base(settings)
        {
        }

        public override void Import(string filename, ModelFile model)
        {
            var obj     = ObjFile.FromFile(filename);

            obj.Dump();

            var mgen    = new ModelGenerator();
            var g       = obj.Groups.FirstOrDefault(i => i.Name == "base");
            var p       = obj.Positions;
            var n       = obj.Normals;
            var t       = obj.TexCoords;

            foreach(var i in g.Meshes)
            {
                mgen.BeginPrimitive();

                for(int j= 0; j < i.FaceCount; ++j)
                {
                    var fp  = i.PositionFaces[j];
                    var fn  = i.NormalFaces[j];
                    var ft  = i.TexCoordFaces[j];

                    mgen.AddVertex(p[fp.A], n[fn.A], t[ft.A]);
                    mgen.AddVertex(p[fp.B], n[fn.B], t[ft.B]);
                    mgen.AddVertex(p[fp.C], n[fn.C], t[ft.C]);
                }

                mgen.EndPrimitive();
            }

            mgen.MorphMin= Settings.MorphMin;
            mgen.MorphMax= Settings.MorphMax;

            mgen.ReplaceModel(model, Settings.RefModel);

            // shader
            ChangeShader(model);
        }
    }
}

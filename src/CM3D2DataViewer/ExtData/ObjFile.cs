using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class ObjFile
    {
        public List<ObjGroup>           Groups                  { get; private set; }
        public string                   FileName                { get; private set; }
        public ObjGroup                 CurrentGroup            { get; private set; }
        public ObjMesh                  CurrentMesh             { get; private set; }
        public int                      SmoothGroup             { get; set; }
        public MtlFile                  MtlData                 { get; private set; }
        public List<Vector3>            Positions               { get; set; }
        public List<Vector3>            Normals                 { get; set; }
        public List<Vector3>            TexCoords               { get; set; }
      //public List<Vector2>            TexCoords               { get; set; }

        public ObjFile()
        {
            Groups      = new List<ObjGroup>();
            Positions   = new List<Vector3>();
            Normals     = new List<Vector3>();
            TexCoords   = new List<Vector3>();
        }

        public static ObjFile FromFile(string file)
        {
            using(var r= new StreamReader(file, Encoding.Default))
                return ObjFile.FromReader(r);
        }

        public static ObjFile FromReader(StreamReader r)
        {
            var obj = new ObjFile();
            obj.Load(r);
            return obj;
        }

        public void Dump()
        {
            foreach(var i in Groups)
            {
                System.Diagnostics.Debug.Print(i.ToString());
                System.Diagnostics.Debug.Indent();

                foreach(var j in i.Meshes)
                    System.Diagnostics.Debug.Print(j.ToString());

                System.Diagnostics.Debug.Unindent();
            }
        }

        private void Load(StreamReader r)
        {
            if(r.BaseStream is FileStream)
                FileName= ((FileStream)r.BaseStream).Name;

            var delim   = " \t".ToArray();

            NewGroup();
            NewMesh();

            for(;;)
            {
                var line= r.ReadLine();

                if(null == line)
                    break;

                line    = line.Trim();

                if(line.Length == 0)
                    continue;

                if(line[0] == '#')
                    continue;

                LineReaded(line.Split(delim, StringSplitOptions.RemoveEmptyEntries));
            }

            if(CurrentMesh.IsEmpty)
                CurrentMesh.Remove();

            if(CurrentGroup.IsEmpty)
                CurrentGroup.Remove();
        }

        private void LineReaded(string[] t)
        {
            switch(t[0])
            {
            case "mtllib":  LoadMtlLib(t[1]);                       break;  // mtllib x.mtl
            case "v":       AddVector3(Positions, t);               break;  // v  -0.6977 12.0902 -1.4070
            case "vn":      AddVector3(Normals,   t);               break;  // vn -0.6329 0.1504 -0.7595
            case "vt":      AddVector3(TexCoords, t);               break;  // vt 0.8807 0.6059 0.0000
            case "o":
            case "g":       NewGroup().Name = t[1];                 break;  // g geom1
            case "usemtl":  NewMesh().Material= MtlData.Get(t[1]);  break;  // usemtl mat0
          //case "s":       NewSubMesh(int.Parse(t[1]));            break;  // s 1
          //case "s":       SmoothGroup = int.Parse(t[1]);          break;  // s 1
            case "s":                                               break;  // s 1
            case "f":       CurrentMesh.AddFace(t);                 break;  // f 1/1/1 2/2/2 3/3/3 
            default:        throw new FormatException();
            }
        }

        private void LoadMtlLib(string file)
        {
            var mtlfile = Path.IsPathRooted(file) ? file
                : FileName == null ? file : Path.Combine(Path.GetDirectoryName(FileName), file);

            MtlData     = MtlFile.FromFile(mtlfile);
        }

        public void AddVector3(List<Vector3> v, string[] t)
        {
            if(t.Length >= 4)
                    v.Add(new Vector3(float.Parse(t[1]), float.Parse(t[2]), float.Parse(t[3])));
            else    v.Add(new Vector3(float.Parse(t[1]), float.Parse(t[2]), 0));
        }

        public void AddVector2(List<Vector2> v, string[] t)
        {
            v.Add(new Vector2(float.Parse(t[1]), float.Parse(t[2])));
        }

        private ObjGroup NewGroup()
        {
            if(CurrentGroup != null && CurrentGroup.IsEmpty)
                return CurrentGroup;

            CurrentGroup    = new ObjGroup(this);

            Groups.Add(CurrentGroup);

            return CurrentGroup;
        }

        private ObjMesh NewMesh()
        {
            if(CurrentMesh != null && CurrentMesh.IsEmpty)
                return CurrentMesh;

            CurrentMesh     = new ObjMesh(CurrentGroup);

            CurrentGroup.Meshes.Add(CurrentMesh);

            return CurrentMesh;
        }
    }

    public class ObjGroup
    {
        public ObjFile                  Owner                   { get; private set; }
        public List<ObjMesh>            Meshes                  { get; private set; }
        public string                   Name                    { get; set; }
        public bool                     IsEmpty
        {
            get
            {
                return Meshes.Count == 0 || Meshes.All(i => i.IsEmpty);
            }
        }

        public ObjGroup(ObjFile owner)
        {
            Owner       = owner;
            Meshes   = new List<ObjMesh>();
        }

        public void Remove()
        {
            Owner.Groups.Remove(this);
        }

        public void MergeMeshes()
        {
            var submesh = new ObjMesh(this);

            submesh.PositionFaces   = Meshes.SelectMany(i => i.PositionFaces).ToList();
            submesh.NormalFaces     = Meshes.SelectMany(i => i.NormalFaces)  .ToList();
            submesh.TexCoordFaces   = Meshes.SelectMany(i => i.TexCoordFaces).ToList();

            Meshes.Clear();
            Meshes.Add(submesh);
        }

        public override string ToString()
        {
            return string.Format("g {0}", Name);
        }
    }

    public class ObjMesh
    {
        public ObjGroup                 Owner                   { get; private set; }
        public MtlMaterial              Material                { get; set; }
        public List<Face>               PositionFaces           { get; set; }
        public List<Face>               NormalFaces             { get; set; }
        public List<Face>               TexCoordFaces           { get; set; }
        public int                      SmoothGroup             { get; set; }
        public int                      FaceCount               { get { return PositionFaces.Count; } }
        public bool                     IsEmpty
        {
            get
            {
                return 0 == PositionFaces.Count
                    && 0 == NormalFaces.Count
                    && 0 == TexCoordFaces.Count;
            }
        }

        public ObjMesh(ObjGroup owner)
        {
            Owner           = owner;
            PositionFaces   = new List<Face>();
            NormalFaces     = new List<Face>();
            TexCoordFaces   = new List<Face>();
        }

        public void Remove()
        {
            Owner.Meshes.Remove(this);
        }

        public void AddFace(string[] t)
        {
            var a   = t[1].Split('/');
            var b   = t[2].Split('/');
            var c   = t[3].Split('/');

            if(a.Length >= 1 && a[0].Length > 0 && b[0].Length > 0 && c[0].Length > 0)
                    PositionFaces.Add(ParseFace(a[0], b[0], c[0]));
            else    PositionFaces.Add(new Face(0, 0, 0));

            if(a.Length >= 2 && a[1].Length > 0 && b[1].Length > 0 && c[1].Length > 0)
                    TexCoordFaces.Add(ParseFace(a[1], b[1], c[1]));
            else    TexCoordFaces.Add(new Face(0, 0, 0));

            if(a.Length >= 3 && a[2].Length > 0 && b[2].Length > 0 && c[2].Length > 0)
                    NormalFaces  .Add(ParseFace(a[2], b[2], c[2]));
            else    NormalFaces  .Add(new Face(0, 0, 0));
        }

        public static Face ParseFace(string a, string b, string c)
        {
            return new Face(int.Parse(a)-1, int.Parse(b)-1, int.Parse(c)-1);
        }

        public override string ToString()
        {
            return string.Format("usemtl {0}", Material == null ? "(null)" : Material.Name);
        }
    }

    public class Face
    {
        public int                      A, B, C;

        public Face()
        {
        }

        public Face(int a, int b, int c)
        {
            A   = a;
            B   = b;
            C   = c;
        }
    }
}

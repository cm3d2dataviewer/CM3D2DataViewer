using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CM3D2DataViewer
{
    public class RelationTree : FileComposition<MenuFile>
    {
        public MenuFile                 RootMenuFile            { get { return TypedFile; } }
        public List<FileComposition<MenuFile>>
                                        RelationMenuFiles       { get; set; }

        public RelationTree(MenuFile rootmenu)
            : base(null)
        {
            TypedFile           = rootmenu;
            RelationMenuFiles   = new List<FileComposition<MenuFile>>();

            // ルートのメニューファイルの解析
            foreach(var i in GetRelationFiles(RootMenuFile.Scripts))
            {
                if(i is MenuFile)
                {
                    var menu    = (MenuFile)i;

                    if(null != RelationMenuFiles.FirstOrDefault(j => j.File == menu))
                        continue;

                    var comp    = FileComposition.Create(this, menu);

                    ParseMenu(comp);
                    RelationMenuFiles.Add(comp);
                } else
                if(i is MateFile)
                {
                    var comp    = FileComposition.Create(this, (MateFile)i);

                    ParseMate(comp);
                    RelationFiles.Add(comp);
                } else
                if(i is ModelSummary)
                {
                    var comp    = FileComposition.Create(this, (ModelSummary)i);

                    ParseModel(comp);
                    RelationFiles.Add(comp);
                } else
                if(i is TexSummary)
                {
                    RelationFiles.Add(FileComposition.Create(this, (TexSummary)i));
                }
            }
        }

        private void ParseMenu(FileComposition<MenuFile> menu)
        {
            // ルートのメニューファイルの解析
            foreach(var i in GetRelationFiles(menu.TypedFile.Scripts))
            {
                if(Contains(i))
                    continue;

                if(i is MenuFile)
                {
                    var comp    = FileComposition.Create(this, (MenuFile)i);

                    // 循環参照していると無限に解析するのでこれ以上のメニュー解析はしない
                    if(comp.Level < 10)
                        ParseMenu(comp);

                    menu.RelationFiles.Add(comp);
                } else
                if(i is MateFile)
                {
                    var comp    = FileComposition.Create(this, (MateFile)i);

                    ParseMate(comp);
                    menu.RelationFiles.Add(comp);
                } else
                if(i is ModelSummary)
                {
                    var comp    = FileComposition.Create(this, (ModelSummary)i);

                    ParseModel(comp);
                    menu.RelationFiles.Add(comp);
                } else
                if(i is TexSummary)
                {
                    menu.RelationFiles.Add(FileComposition.Create(this, (TexSummary)i));
                }
            }
        }

        private void ParseMate(FileComposition<MateFile> mate)
        {
            foreach(var i in mate.TypedFile.Params.OfType<ParamTex>())
            {
                if(null == i.TexAsset)
                    continue;

                var name= Path.ChangeExtension(Path.GetFileName(i.TexAsset), ".tex");
                var tex = DataManager.Instance.FindItem(name) as TexSummary;

                if(null != tex)
                    mate.RelationFiles.Add(FileComposition.Create(this, tex));
            }
        }

        private void ParseModel(FileComposition<ModelSummary> model)
        {
            var modeldata   = ModelFile.FromFile(model.File.FileName);

            foreach(var i in modeldata.Materials.SelectMany(i => i.Params).OfType<ParamTex>())
            {
                if(null == i.TexAsset)
                    continue;

                var name= Path.ChangeExtension(Path.GetFileName(i.TexAsset), ".tex");
                var tex = DataManager.Instance.FindItem(name) as TexSummary;

                if(null != tex)
                    model.RelationFiles.Add(FileComposition.Create(this, tex));
            }
        }

        private IEnumerable<BaseFile> GetRelationFiles(List<List<string>> script)
        {
            return script
                .SelectMany(i => i)
                .Select(i => DataManager.Instance.FindItem(i))
                .OfType<BaseFile>();
        }

        public override void Dump()
        {
            System.Diagnostics.Debug.Print("* ROOT:  {0}", RootMenuFile.FileName);
            System.Diagnostics.Debug.Indent();

            foreach(var i in RelationFiles)
                i.Dump();

            foreach(var i in RelationMenuFiles)
                i.Dump();

            System.Diagnostics.Debug.Unindent();
        }
    }

    public abstract class FileComposition
    {
        public abstract BaseFile        File                    { get; }
        public FileComposition          ReferencedFrom          { get; private set; }
        public List<FileComposition>    RelationFiles           { get; private set; }
        public int                      Level                   { get { return ReferencedFrom == null ? 0 : ReferencedFrom.Level + 1; } }

        public FileComposition(FileComposition from)
        {
            ReferencedFrom  = from;
            RelationFiles   = new List<FileComposition>();
        }

        public static FileComposition<T> Create<T>(FileComposition from, T file) where T : BaseFile
        {
            var comp        = new FileComposition<T>(from);
            comp.TypedFile  = file;

            return comp;
        }

        public bool Contains(BaseFile file)
        {
            return null != RelationFiles.FirstOrDefault(i => i.File == file);
        }
        /*
        public void AddRelation(ModelSummary file)
        {
            if(RelationFiles.FirstOrDefault(i => i.File == file) == null)
                RelationFiles.Add(Create(file));
        }

        public void AddRelation(TexSummary file)
        {
            if(RelationFiles.FirstOrDefault(i => i.File == file) == null)
                RelationFiles.Add(Create(file));
        }

        public void AddRelation(MateFile file)
        {
            if(RelationFiles.FirstOrDefault(i => i.File == file) == null)
                RelationFiles.Add(Create(file));
        }

        public void AddRelation(MenuFile file)
        {
            if(RelationFiles.FirstOrDefault(i => i.File == file) == null)
                RelationFiles.Add(Create(file));
        }
        */
        public abstract void Dump();
    }

    public class FileComposition<T> : FileComposition where T : BaseFile
    {
        public override BaseFile        File                    { get { return TypedFile; } }
        public T                        TypedFile               { get; set; }

        public FileComposition(FileComposition from)
            : base(from)
        {
        }

        public override void Dump()
        {
            var type= File is MateFile     ? "MATE:  "
                    : File is ModelSummary ? "MODEL: "
                    : File is TexSummary   ? "TEX:   "
                    : File is MenuFile     ? "MENU:  " : "?:     ";

            System.Diagnostics.Debug.Print("+ {0}{1}", type, File.FileName);

            System.Diagnostics.Debug.Indent();

            foreach(var i in RelationFiles)
                i.Dump();

            System.Diagnostics.Debug.Unindent();
        }
    }
}

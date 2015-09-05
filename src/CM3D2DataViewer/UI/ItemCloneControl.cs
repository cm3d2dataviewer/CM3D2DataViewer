using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM3D2DataViewer
{
    public partial class ItemCloneControl : UserControl
    {
        private MenuFile                data;

        public List<BaseFile>           CopyFiles               { get; private set; }
        public List<BaseFile>           CopiedFiles             { get; private set; }
        public MenuFile                 Data                    { get { return data; } set { SetData(value); } }
        public DataReplace              Replacer                { get; private set; }

        public ItemCloneControl()
        {
            InitializeComponent();

            Replacer    = new DataReplace();
        }

        private void SetData(MenuFile value)
        {
            if(value == data)
                return;

            data    = value;

            UpdateView();
        }

        private void UpdateView()
        {
            if(data == null)
                return;

            var filename= Path.GetFileName(data.FileName);
            var index   = filename.IndexOf('_');

            if(index >= 0)
            {
                var text            = filename.Substring(0, index);
                tbOldString1.Text   = text;
                tbNewString1.Text   = text;
            }

            listView1.Items.Clear();

            bCopyFiles.Enabled  = false;
        }

        private void bListing_Click(object sender, EventArgs e)
        {
            if(null == data)
                return;

            CopyFiles   = new List<BaseFile>();

            AddFile(data);

            listView1.Items.Clear();

            foreach(var i in CopyFiles)
            {
                var item= listView1.Items.Add(Path.GetFileName(i.FileName));
                item.Tag= i;

                item.SubItems.Add(item.Text);
            }

            bCopyFiles.Enabled  = true;
        }

        #region 列挙処理
        private void AddFile(BaseFile file)
        {
            if(CopyFiles.Contains(file))
                return;

            CopyFiles.Add(file);

                 if(file is MateFile)       AddMate((MateFile)file);
            else if(file is MenuFile)       AddMenu((MenuFile)file);
            else if(file is ModelSummary)   AddModel(ModelFile.FromFile(file.FileName));
        }

        private void AddFiles(IEnumerable<BaseFile> files)
        {
            foreach(var i in files)
                AddFile(i);
        }

        private void AddMenu(MenuFile menu)
        {
            AddFiles(menu.Scripts
                .SelectMany(j => j)
                .Select(j => DataManager.Instance.FindItem(j) as BaseFile)
                .Where(j => j != null));
        }

        private void AddMate(MateFile mate)
        {
            AddFiles(mate.Params
                .OfType<ParamTex>()
                .Where(i => i.TexAsset != null)
                .Select(i => DataManager.Instance.FindItem(i.TexFileName) as BaseFile)
                .Where(i => i != null));
        }

        private void AddModel(ModelFile model)
        {
            AddFiles(model.Materials
                .SelectMany(i => i.Params)
                .OfType<ParamTex>()
                .Where(i => i.TexAsset != null)
                .Select(i => DataManager.Instance.FindItem(i.TexFileName) as BaseFile)
                .Where(i => i != null));
        }

        private void ReplaceList()
        {
            Replacer.OldString  = tbOldString1.Text.Trim().ToUpper();
            Replacer.NewString  = tbNewString1.Text.Trim();

            foreach(ListViewItem i in listView1.Items)
            {
                var t               = Replacer.Replace(i.Text);
                i.SubItems[1].Text  = t;
                i.ForeColor         = t.ToUpper() == i.Text.ToUpper() ? SystemColors.WindowText : Color.Red;
            }
        }

        private void tbOldString1_TextChanged(object sender, EventArgs e)
        {
            ReplaceList();
        }

        private void tbNewString1_TextChanged(object sender, EventArgs e)
        {
            ReplaceList();
        }
        #endregion

        #region コピー処理
        private void CloneFiles()
        {
            CloneFiles(CopyFiles);
        }

        private void CloneFiles(IEnumerable<BaseFile> files)
        {
            foreach(var i in CopyFiles)
                CloneFile(i);
        }

        private DialogResult ConfirmOverride(string file)
        {
            if(!File.Exists(file))
                return DialogResult.Yes;

            var rc  = MessageBox.Show(
                "ファイルを上書きします。よろしいですか？\n"+file,
                "上書き確認", MessageBoxButtons.YesNoCancel);

            if(rc == DialogResult.Cancel)
                throw new OperationCanceledException();

            return rc;
        }

        private void CloneFile(BaseFile file)
        {
            var newname = Replacer.ReplaceFileName(file.FileName);

            if(newname.ToUpper() == file.FileName.ToUpper())
                return;

                 if(file is MenuFile)       CloneMenuFile((MenuFile)file, newname);
            else if(file is MateFile)       CloneMateFile((MateFile)file, newname);
            else if(file is TexSummary)     CloneTexFile((TexSummary)file, newname);
            else if(file is ModelSummary)   CloneModelFile((ModelSummary)file, newname);
        }

        private void ReplaceDescription(List<string> descs)
        {
            for(int i= 0; i < descs.Count; ++i)
                descs[i]   = Replacer.ReplaceFileName(descs[i]);
        }

        private void CloneMenuFile(MenuFile file, string newname)
        {
            var clone       = MenuFile.FromFile(file.FileName);
            clone.FileName  = newname;

            ReplaceDescription(clone.Descriptions);

            foreach(var i in clone.Scripts)
            {
                for(int j= 0; j < i.Count; ++j)
                {
                    if(DataManager.Instance.FindItem(i[j]) == null)
                        continue;

                    i[j]    = Replacer.ReplaceFileName(i[j]);
                }
            }

            if(ConfirmOverride(clone.FileName) == DialogResult.Yes)
            {
                MenuFile.ToFile(clone.FileName, clone);
                AddCopiedFile(clone);
            }
        }

        private void CloneMateFile(MateFile file, string newname)
        {
            var clone       = MateFile.FromFile(file.FileName);
            clone.FileName  = newname;

            ReplaceDescription(clone.Descriptions);

            foreach(var i in clone.Params.OfType<ParamTex>())
            {
                if(null != i.TexName)
                    i.TexName  = Replacer.ReplaceFileName(i.TexName);

                if(i.TexAsset != null)
                    i.TexAsset  = Replacer.ReplaceFileName(i.TexAsset);
            }
 
            if(ConfirmOverride(clone.FileName) == DialogResult.Yes)
            {
                MateFile.ToFile(clone.FileName, clone);
                AddCopiedFile(clone);
            }
        }

        private void CloneTexFile(TexSummary file, string newname)
        {
            var clone       = TexFile.FromFile(file.FileName);
            clone.FileName  = newname;

            //ReplaceDescription(clone.Descriptions);

            clone.AssetPath = Replacer.ReplaceFileName(clone.AssetPath);

            if(ConfirmOverride(clone.FileName) == DialogResult.Yes)
            {
                TexFile.ToFile(clone.FileName, clone);
                AddCopiedFile(clone);
            }
        }

        private void CloneModelFile(ModelSummary file, string newname)
        {
            var clone       = ModelFile.FromFile(file.FileName);
            clone.FileName  = newname;

            ReplaceDescription(clone.Descriptions);

            foreach(var i in clone.Materials)
                i.Descriptions[0]   = Replacer.ReplaceFileName(i.Descriptions[0]);

            foreach(var i in clone.Materials.SelectMany(j => j.Params).OfType<ParamTex>())
            {
                if(null != i.TexName)
                    i.TexName  = Replacer.ReplaceFileName(i.TexName);

                if(null != i.TexName)
                    i.TexAsset  = Replacer.ReplaceFileName(i.TexAsset);
            }

            if(ConfirmOverride(clone.FileName) == DialogResult.Yes)
            {
                ModelFile.ToFile(clone.FileName, clone);
                AddCopiedFile(clone);
            }
        }
        #endregion

        public class DataReplace
        {
            public string               OldString               { get; set; }
            public string               NewString               { get; set; }

            public string ReplaceFileName(string s)
            {
                var index   = s.LastIndexOf('\\');

                if(index >= 0)
                {
                    var dir     = s.Substring(0, index);
                    var filename= s.Substring(index+1);
                    filename    = Replace(filename);

                    return dir + "\\" + filename;
                }

                index       = s.LastIndexOf('/');

                if(index >= 0)
                {
                    var dir     = s.Substring(0, index);
                    var filename= s.Substring(index+1);
                    filename    = Replace(filename);

                    return dir + "/" + filename;
                }

                return Replace(s);
            }

            public string Replace(string s)
            {
                if(OldString.Length > 0 && NewString.Length > 0)
                {
                    var index   = s.ToUpper().IndexOf(OldString);

                    if(index >= 0)
                        s       = s.Substring(0, index) + NewString + s.Substring(index+OldString.Length);
                }

                return s;
            }
        }

        private void AddCopiedFile(BaseFile file)
        {
            CopiedFiles.Add(file);

            // todo DataManagerに反映
        }

        private void bCopyFiles_Click(object sender, EventArgs e)
        {
            CopiedFiles = new List<BaseFile>();

            CloneFiles();

            var menu    = CopiedFiles.FirstOrDefault(i => i is MenuFile);

            Form1.Instance.LoadFile(menu.FileName);

            foreach(var i in CopiedFiles)
                DataManager.Instance.LoadFile(i.FileName);
        }
    }
}

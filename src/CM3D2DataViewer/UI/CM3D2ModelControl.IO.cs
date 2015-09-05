using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM3D2DataViewer
{
    partial class CM3D2ModelControl
    {
        private ImportSettings GetImportSettings()
        {
            var settings    = new ImportSettings();
            var config      = Config.Instance;

            settings.RefModel       = ModelFile.FromFile(cbRefModel.Text);
            settings.MorphMin       = config.MorphMin       = (float)nudMinDist.Value;
            settings.MorphMax       = config.MorphMax       = (float)nudMaxDist.Value;
            settings.Scale          = config.ImportScale    = (float)nudImportScale.Value;
            settings.ChangeShader   = config.ChangeShader   = cbChangeShader.Checked;
            settings.Shader         = config.Shader         = cbShader.Text;

            try { config.Save(); } catch {}

            return settings;
        }

        private ExportSettings GetExportSettings()
        {
            var settings    = new ExportSettings();
            var config      = Config.Instance;

            settings.Skin           = config.ExportSkin     = cbExportSkin.Checked;
            settings.Morph          = config.ExportMorph    = cbExportMorph.Checked;
            settings.Scale          = config.ImportScale    = (float)nudImportScale.Value;

            try { config.Save(); } catch {}

            return settings;
        }

        #region OBJ
        private void ImportOBJ()
        {
            if(null == data)
                return;

            try
            {
                var settings= GetImportSettings();
                var importer= new ObjImporter(settings);
                var filename= Path.ChangeExtension(data.FileName, ".obj");

                Globals.Message("開始 Import/OBJ {0}", Path.GetFileName(filename));

                importer.Import(filename, Data);

                ModelFile.ToFile(data.FileName, data);

                UpdateView();

                Globals.Message("完了");
            } catch(Exception ex)
            {
                Globals.Message("失敗");

                MessageBox.Show(ex.ToString());

                return;
            }
        }

        private void ExportOBJ()
        {
            if(null == data)
                return;

            try
            {
                var settings= GetExportSettings();
                var importer= new ObjExporter(settings);
                var filename= Path.ChangeExtension(data.FileName, ".obj");

                Globals.Message("開始 Export/OBJ {0}", Path.GetFileName(filename));

                importer.Export(filename, Data);

                Globals.Message("完了");
            } catch(Exception ex)
            {
                Globals.Message("失敗");

                MessageBox.Show(ex.ToString());

                return;
            }
        }
        #endregion

        #region DAE
        private void ImportDAE()
        {
            if(null == data)
                return;

            try
            {
                var settings= GetImportSettings();
                var importer= new DaeImporter(settings);
                var filename= Path.ChangeExtension(data.FileName, ".dae");

                Globals.Message("開始 Import/DAE {0}", Path.GetFileName(filename));

                importer.Import(filename, Data);

                ModelFile.ToFile(data.FileName, data);

                UpdateView();

                Globals.Message("完了");
            } catch(Exception ex)
            {
                Globals.Message("失敗");

                MessageBox.Show(ex.ToString());

                return;
            }
        }

        private void ExportDAE()
        {
            if(null == data)
                return;

            try
            {
                var settings= GetExportSettings();
                var importer= new DaeExporter(settings);
                var filename= Path.ChangeExtension(data.FileName, ".dae");

                Globals.Message("開始 Export/DAE {0}", Path.GetFileName(filename));

                importer.Export(filename, Data);

                Globals.Message("完了");
            } catch(Exception ex)
            {
                Globals.Message("失敗");

                MessageBox.Show(ex.ToString());

                return;
            }
        }
        #endregion


        #region MQO
        private void ImportMQO()
        {
            Globals.Message("MQOインポートは未対応です");

#if false
            if(null == data)
                return;

            try
            {
                var settings= GetImportSettings();
                var importer= new MqoImporter(settings);
                var filename= Path.ChangeExtension(data.FileName, ".mqo");

                Globals.Message("開始 Import/MQO {0}", Path.GetFileName(filename));

                importer.Import(filename, Data);

                ModelFile.ToFile(data.FileName, data);

                UpdateView();

                Globals.Message("完了");
            } catch(Exception ex)
            {
                Globals.Message("失敗");

                MessageBox.Show(ex.ToString());

                return;
            }
#endif
        }

        private void ExportMQO()
        {
            if(null == data)
                return;

            try
            {
                var settings= GetExportSettings();
                var importer= new MqoExporter(settings);
                var filename= Path.ChangeExtension(data.FileName, ".mqo");

                Globals.Message("開始 Export/MQO {0}", Path.GetFileName(filename));

                importer.Export(filename, Data);

                Globals.Message("完了");
            } catch(Exception ex)
            {
                Globals.Message("失敗");

                MessageBox.Show(ex.ToString());

                return;
            }
        }
#endregion
    }
}

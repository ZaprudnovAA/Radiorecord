using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Radio
{
    internal class Update
    {
        //public const string Url = @"https://github.com/ZaprudnovAA/Radiorecord/tree/master/Application/";
        public const string Url = @"http://rosvel.ru/Radiorecord/";
        private static readonly string ProgramName = $@"{Application.ProductName}.exe";
        private readonly string _programNameNew = $@"{ProgramName}.update";
        private const string UpdaterName = @"ZAAUniversalUpdaterConsole.exe";
        private const string VersionFile = @"radiorecord_version.xml";
        public const string StationsFile = @"radiorecord_stations.xml";


        public void Check()
        {
            try
            {
                if (File.Exists(UpdaterName))
                {
                    File.Delete(UpdaterName);
                }

                if (File.Exists(_programNameNew))
                {
                    File.Delete(_programNameNew);
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(Url + VersionFile);

                if (new Version(Application.ProductVersion) != new Version(doc.GetElementsByTagName("myprogram")[0].InnerText))
                {
                    if (MessageBox.Show(
                            $@"A new version of the program is detected. Would you like to update {Vars.AName} to a more recent version right now?", Vars.AName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;

                    Task.WaitAll(Task.Factory.StartNew(() => DownloadFile(UpdaterName + ".update")));
                    Task.WaitAll(Task.Factory.StartNew(() => DownloadFile(_programNameNew)));

                    if (File.Exists(UpdaterName + ".update") && new FileInfo(UpdaterName + ".update").Length != 0)
                    {
                        File.Move(UpdaterName + ".update", UpdaterName);
                    }
                    else
                    {
                        File.Delete(UpdaterName + ".update");
                    }

                    if (File.Exists(UpdaterName) && new FileInfo(UpdaterName).Length == 0)
                    {
                        File.Delete(UpdaterName);
                    }

                    if (File.Exists(UpdaterName) && new FileInfo(UpdaterName).Length != 0 &&
                        File.Exists(_programNameNew) && new FileInfo(_programNameNew).Length != 0)
                    {
                        Process.Start(UpdaterName,
                            "\"" + _programNameNew + "\" \"" + Application.ProductName + ".exe\"");
                        Process.GetCurrentProcess().CloseMainWindow();
                    }
                    else
                    {
                        File.Delete(_programNameNew);
                        File.Delete(UpdaterName);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private static void DownloadFile(string filename)
        {
            string filenamePath = filename;
            if (File.Exists(filenamePath) && new FileInfo(filenamePath).Length == 0) { File.Delete(filenamePath); }

            try
            {
                if (!File.Exists(filenamePath))
                {
                    using (var client = new WebClient())
                    {
                        try
                        {
                            client.DownloadFileAsync(new Uri(Url + filename), filenamePath);

                            while (client.IsBusy) { }
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}

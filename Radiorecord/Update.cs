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
        //private const string Url = @"https://github.com/ZaprudnovAA/Radiorecord/tree/master/Release/";
        private const string Url = @"http://rosvel.ru/Radiorecord/";
        private static readonly string ProgramName = Application.ProductName + ".exe";
        private readonly string programNameNew = ProgramName + ".update";
        private const string UpdaterName = @"ZAAUniversalUpdaterConsole.exe";
        private const string VersionFile = @"radiorecord_version.xml";


        public void Check()
        {
            try
            {
                if (File.Exists(UpdaterName))
                {
                    File.Delete(UpdaterName);
                }

                if (File.Exists(programNameNew))
                {
                    File.Delete(programNameNew);
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(Url + VersionFile);

                if (new Version(Application.ProductVersion) != new Version(doc.GetElementsByTagName("myprogram")[0].InnerText))
                {
                    Task.WaitAll(Task.Factory.StartNew(() => DownloadFile(UpdaterName + ".update")));
                    Task.WaitAll(Task.Factory.StartNew(() => DownloadFile(programNameNew)));

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

                    if (File.Exists(UpdaterName) && new FileInfo(UpdaterName).Length != 0 && File.Exists(programNameNew) && new FileInfo(programNameNew).Length != 0)
                    {
                        Process.Start(UpdaterName,
                            "\"" + programNameNew + "\" \"" + Application.ProductName + ".exe\"");
                        Process.GetCurrentProcess().CloseMainWindow();
                    }
                    else
                    {
                        File.Delete(programNameNew);
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

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
        private const string Url = @"https://github.com/ZaprudnovAA/Radiorecord/tree/master/Release/";
        private static readonly string ProgramName = Application.ProductName + ".exe";
        private readonly string _programNameNew = ProgramName + ".update";
        private const string UpdaterName = @"ZAAUniversalUpdaterConsole.exe";


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
                doc.Load(Url + "radiorecord_version.xml");

                if (new Version(Application.ProductVersion) <
                    new Version(doc.GetElementsByTagName("myprogram")[0].InnerText))
                {
                    var task0 = Task.Factory.StartNew(() => DownloadFile(UpdaterName));
                    var task1 = Task.Factory.StartNew(() => DownloadFile(_programNameNew));

                    Task.WaitAll(task0, task1);

                    if (File.Exists(_programNameNew) &&
                        new Version(FileVersionInfo.GetVersionInfo(_programNameNew).FileVersion) >
                        new Version(Application.ProductVersion))
                    {

                        Process.Start(UpdaterName,
                            "\"" + _programNameNew + "\" \"" + Application.ProductName + ".exe\"");
                        Process.GetCurrentProcess().CloseMainWindow();
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

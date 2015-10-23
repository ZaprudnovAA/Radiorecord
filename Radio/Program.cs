using System;
using System.Threading;
using System.Windows.Forms;

namespace Radio
{
    static class Program
    {
        private static Mutex m_instance;
        public static Form1 F;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool tryCreateNewApp;
            m_instance = new Mutex(true, new vars().aName, out tryCreateNewApp);
            if (tryCreateNewApp)
            {
                Thread t = new Thread(new ThreadStart(ThreadProc));
                t.Name = new vars().aName;
                t.Start();
                return;
            }
            else
            {
                MessageBox.Show(string.Format("Приложение {0} уже запущено", new vars().aName), new vars().aName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private static void ThreadProc()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            F = new Form1();
            Application.Run(F);
        }
    }
}

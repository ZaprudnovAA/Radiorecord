using System;
using System.Threading;
using System.Windows.Forms;

namespace Radio
{
    static class Program
    {
        public static Form1 F;
        private static bool mutexWasCreated;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex m_instance = new Mutex(true, vars.aName, out mutexWasCreated);
            if (mutexWasCreated)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                F = new Form1();
                Application.Run(F);
                return;
            }
            else
            {
                MessageBox.Show(string.Format("Приложение {0} уже запущено", vars.aName), vars.aName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}

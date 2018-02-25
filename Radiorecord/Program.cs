using System;
using System.Threading;
using System.Windows.Forms;

namespace Radio
{
    internal static class Program
    {
        public static Form1 F;
        private static bool _mutexWasCreated;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            var mutex = new Mutex(true, Vars.AName, out _mutexWasCreated);
            if (_mutexWasCreated)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                F = new Form1();
                Application.Run(F);
                return;
            }
            else
            {
                MessageBox.Show(string.Format("{0} is already running", Vars.AName), Vars.AName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}

using Microsoft.Win32;
using System;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Radio
{
    internal static class RegistryWorker
    {
        private static readonly RegistryKey CurrentUserKey = Registry.CurrentUser;
        private const string SoftwareKey = "Software";
        private const string ProgramDeveloperKey = "ZaprudnovAA";
        internal const string FavoriteStationName = "FavoriteStation";
        internal const string FavoriteVolumeName = "FavoriteVolume";
        internal const string FavoriteBitrateName = "FavoriteBitrate";

        public static void CreateSubKey()
        {
            try
            {
                using (RegistryKey softwareKeyOpened = CurrentUserKey.OpenSubKey(SoftwareKey, true))
                {
                    if (softwareKeyOpened != null)
                    {
                        softwareKeyOpened.CreateSubKey(ProgramDeveloperKey);
                        softwareKeyOpened.Close();
                    }

                    using (RegistryKey programDeveloperKeyOpened = CurrentUserKey.OpenSubKey(SoftwareKey + @"\" + ProgramDeveloperKey, true))
                    {
                        if (programDeveloperKeyOpened != null)
                        {
                            programDeveloperKeyOpened.CreateSubKey(Vars.AName);
                            programDeveloperKeyOpened.Close();
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void SetValue(string name, string value)
        {
            try
            {
                using (RegistryKey programKeyOpened = CurrentUserKey.OpenSubKey(SoftwareKey + @"\" + ProgramDeveloperKey + @"\" + Vars.AName, true))
                {
                    if (programKeyOpened == null) return;
                    programKeyOpened.SetValue(name, value);
                    programKeyOpened.Close();
                }
            }
            catch
            {
                // ignored
            }
        }

        public static string GetValue(string name)
        {
            string value = null;

            try
            {
                using (RegistryKey programKeyOpened = CurrentUserKey.OpenSubKey(SoftwareKey + @"\" + ProgramDeveloperKey + @"\" + Vars.AName, true))
                {
                    if (programKeyOpened != null)
                    {
                        value = programKeyOpened.GetValue(name).ToString();
                        programKeyOpened.Close();
                    }
                }
            }
            catch
            {
                // ignored
            }

            return value;
        }
    }

    internal static class Funks
    {
        private static readonly StringBuilder NotifyUserText = new StringBuilder();

        public delegate void AddNotifyUserDelegate(string message);
        public static void NotifyUser(string message)
        {
            Program.F.notifyIcon1.Visible = true;
            Program.F.notifyIcon1.ShowBalloonTip(Vars.NotifyTimeout, Vars.AName, string.Format("{0}\r\n", message), ToolTipIcon.Info);
            NotifyUserText.Clear();
        }

        public static void PlayJingle()
        {
            try
            {
                new SoundPlayer(Properties.Resources.Record_Jingle).Play();
            }
            catch
            {
                // ignored
            }
        }

        public static void SetFavoriteStation(int id)
        {
            Vars.WhoIsPlaying = id;
            RegistryWorker.SetValue(RegistryWorker.FavoriteStationName, id.ToString());
        }

        public static void SetFavoriteBitrate()
        {
            RegistryWorker.SetValue(RegistryWorker.FavoriteBitrateName, Vars.UsersBitrate.ToString());
        }

        public static void SetFavoriteVolume()
        {
            RegistryWorker.SetValue(RegistryWorker.FavoriteVolumeName, Vars.UsersVolume.ToString());
        }

    }

    public class Hook : IDisposable
    {
        #region Declare WinAPI functions
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookProc callback, IntPtr hInstance, uint threadId);
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hInstance);
        #endregion
        #region Constants
        private const int WhKeyboardLl = 13;
        private const int WhKeydown = 0x0100;
        #endregion

        private readonly int _key;
        public event KeyPressEventHandler KeyPressed;

        private delegate IntPtr KeyboardHookProc(int code, IntPtr wParam, IntPtr lParam);
        private readonly KeyboardHookProc _proc;
        private IntPtr _hHook = IntPtr.Zero;

        public Hook(int keyCode)
        {
            _key = keyCode;
            _proc = HookProc;
        }

        public void SetHook()
        {
            var hInstance = LoadLibrary("User32");
            _hHook = SetWindowsHookEx(WhKeyboardLl, _proc, hInstance, 0);
        }

        public void Dispose()
        {
            UnHook();
        }

        private void UnHook()
        {
            UnhookWindowsHookEx(_hHook);
        }

        private IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0 || wParam != (IntPtr)WhKeydown || Marshal.ReadInt32(lParam) != _key)
                return CallNextHookEx(_hHook, code, (int)wParam, lParam);

            if (KeyPressed != null) KeyPressed.Invoke(this, new KeyPressEventArgs(Convert.ToChar(code)));

            return CallNextHookEx(_hHook, code, (int)wParam, lParam);
        }

    }
}

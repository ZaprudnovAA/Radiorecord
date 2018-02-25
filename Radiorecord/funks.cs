﻿using System;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Radio
{
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
            new SoundPlayer(Properties.Resources.Record_Jingle).Play();
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
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_KEYDOWN = 0x0100;
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
            _hHook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hInstance, 0);
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
            if (code < 0 || wParam != (IntPtr) WH_KEYDOWN || Marshal.ReadInt32(lParam) != _key)
                return CallNextHookEx(_hHook, code, (int) wParam, lParam);

            if (KeyPressed != null) KeyPressed.Invoke(this, new KeyPressEventArgs(Convert.ToChar(code)));

            return CallNextHookEx(_hHook, code, (int)wParam, lParam);
        }
    }
}

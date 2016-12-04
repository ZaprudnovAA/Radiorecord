using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Radio
{
    class funks
    {
        private static System.Text.StringBuilder NotifyUserText = new System.Text.StringBuilder();

        public delegate void AddNotifyUserDelegate(string message, string type);
        public void NotifyUser(string message, string type)
        {
            ToolTipIcon notifyIconType = new ToolTipIcon();
            switch (type)
            {
                case "info":
                    notifyIconType = ToolTipIcon.Info;
                    break;
                case "warning":
                    notifyIconType = ToolTipIcon.Warning;
                    break;
                case "error":
                    notifyIconType = ToolTipIcon.Error;
                    break;
                default:
                    notifyIconType = ToolTipIcon.Info;
                    break;
            }
            NotifyUserText.Append(string.Format("{0}\r\n", message));
            Program.F.notifyIcon1.Visible = true;
            Program.F.notifyIcon1.ShowBalloonTip(vars.notifyTimeout, vars.aName, NotifyUserText.ToString(), notifyIconType);
            NotifyUserText.Clear();
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

        // код клавиши на которую ставим хук
        private int _key;
        public event KeyPressEventHandler KeyPressed;

        private delegate IntPtr KeyboardHookProc(int code, IntPtr wParam, IntPtr lParam);
        private KeyboardHookProc _proc;
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

        public void UnHook()
        {
            UnhookWindowsHookEx(_hHook);
        }

        private IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if ((code >= 0 && wParam == (IntPtr)WH_KEYDOWN) && Marshal.ReadInt32(lParam) == _key)
            {
                // бросаем событие
                KeyPressed?.Invoke(this, new KeyPressEventArgs(Convert.ToChar(code)));
            }

            // пробрасываем хук дальше
            return CallNextHookEx(_hHook, code, (int)wParam, lParam);
        }
    }


    /* Hooks
    VK_NUMPAD7	0x67	VK_BACK	0x08
    VK_NUMPAD8	0x68	VK_TAB	0x09
    VK_NUMPAD9	0x69	VK_RETURN	0x0D
    VK_MULTIPLY	0x6A	VK_SHIFT	0x10
    VK_ADD	0x6B	VK_CONTROL	0x11
    VK_SEPARATOR	0x6C	VK_MENU	0x12
    VK_SUBTRACT	0x6D	VK_PAUSE	0x13
    VK_DECIMAL	0x6E	VK_CAPITAL	0x14
    VK_DIVIDE	0x6F	VK_ESCAPE	0x1B
    VK_F1	0x70	VK_SPACE	0x20
    VK_F2	0x71	VK_END	0x23
    VK_F3	0x72	VK_HOME	0x24
    VK_F4	0x73	VK_LEFT	0x25
    VK_F5	0x74	VK_UP	0x26
    VK_F6	0x75	VK_RIGHT	0x27
    VK_F7	0x76	VK_DOWN	0x28
    VK_F8	0x77	VK_PRINT	0x2A
    VK_F9	0x78	VK_SNAPSHOT	0x2C
    VK_F10	0x79	VK_INSERT	0x2D
    VK_F11	0x7A	VK_DELETE	0x2E
    VK_F12	0x7B	VK_LWIN	0x5B
    VK_NUMLOCK	0x90	VK_RWIN	0x5C
    VK_SCROLL	0x91	VK_NUMPAD0	0x60
    VK_LSHIFT	0xA0	VK_NUMPAD1	0x61
    VK_RSHIFT	0xA1	VK_NUMPAD2	0x62
    VK_LCONTROL	0xA2	VK_NUMPAD3	0x63
    VK_RCONTROL	0xA3	VK_NUMPAD4	0x64
    VK_LMENU	0xA4	VK_NUMPAD5	0x65
    VK_RMENU	0xA5	VK_NUMPAD6	0x66
     */
}

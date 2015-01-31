namespace SteamControl
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using Microsoft.Win32;

    public class SteamControl
    {
        #region # WinApi #

        [DllImport("user32.dll",EntryPoint="FindWindow")]
        private static extern IntPtr FindWindow(string sClass, string sWindow);

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
            
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_CLOSE = 0xF060;

        #endregion # WinApi #

        public static bool IsBigPictureRunning()
        {
            return FindBigPictureWindow() != IntPtr.Zero;
        }

        public static void StartBigPicture()
        {
            Process.Start(GetSteamPath(),
                Process.GetProcessesByName("steam").Length > 0 ? "steam://open/bigpicture" : "-bigpicture");
        }

        public static void CloseBigPicture()
        {
            var handle = FindBigPictureWindow();
            if (handle == IntPtr.Zero) return;
            SendMessage(handle, WM_SYSCOMMAND, SC_CLOSE, 0);
        }

        public static void WaitBigPictureState(bool start)
        {
            for (;;)
            {
                if (IsBigPictureRunning() == start) return;
                Thread.Sleep(1000);
            }
        }

        public static string GetSteamPath()
        {
            return (string) Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam",
                "SteamExe",
                null);
        }

        private static IntPtr FindBigPictureWindow()
        {
            return FindWindow("CUIEngineWin32", "Steam");
        }
    }
}

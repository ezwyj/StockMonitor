using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonitorCore
{


    [StructLayout(LayoutKind.Sequential)]
    
    public class Win32API
    {
        public struct WindowInfo
        {
            public IntPtr hWnd;
            public string szWindowName;
            public string szClassName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ProcessEntry32
        {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;


            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }

        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }

        #region GetWindowCapture的dll引用
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(
         IntPtr hdc // handle to DC
         );
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(
         IntPtr hdc,         // handle to DC
         int nWidth,      // width of bitmap, in pixels
         int nHeight      // height of bitmap, in pixels
         );
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(
         IntPtr hdc,           // handle to DC
         IntPtr hgdiobj    // handle to object
         );
        [DllImport("gdi32.dll")]
        public static extern int DeleteDC(
         IntPtr hdc           // handle to DC
         );
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(
         IntPtr hwnd,                // Window to copy,Handle to the window that will be copied.
         IntPtr hdcBlt,              // HDC to print into,Handle to the device context.
         UInt32 nFlags               // Optional flags,Specifies the drawing options. It can be one of the following values.
         );
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(
         IntPtr hwnd
         );
        #endregion

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("KERNEL32.DLL ")]
        public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
        [DllImport("KERNEL32.DLL ")]
        public static extern int CloseHandle(IntPtr handle);
        [DllImport("KERNEL32.DLL ")]
        public static extern int Process32First(IntPtr handle, ref ProcessEntry32 pe);
        [DllImport("KERNEL32.DLL ")]
        public static extern int Process32Next(IntPtr handle, ref ProcessEntry32 pe);

        public delegate bool WNDENUMPROC(IntPtr hWnd, int lParam);
        [DllImport("user32.dll")]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, int lParam);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]//查找窗口
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public const int WM_KEYDOWN = 0X100;
        public const int WM_KEYUP = 0X101;
        public const int WM_SYSCHAR = 0X106;
        public const int WM_SYSKEYUP = 0X105;
        public const int WM_SYSKEYDOWN = 0X104;
        public const int WM_CHAR = 0X102;

        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }

}

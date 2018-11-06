using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DzhMonitor
{
    static class Program
    {
        public static MonitorCore.CoreAnalysis CoreAnalysis { get; set; }
        public static bool Run { get; set; }
        public static StreamWriter LogStream;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                Application.ThreadException += Application_ThreadException;
                CoreAnalysis = new MonitorCore.CoreAnalysis();
                Run = false;
                Application.Run(new Form1());
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }
    }
}

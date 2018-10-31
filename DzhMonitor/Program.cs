using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DzhMonitor
{
    static class Program
    {
        public static MonitorCore.CoreAnalysis CoreAnalysis { get; set; }
      
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            CoreAnalysis = new MonitorCore.CoreAnalysis();
           
            Application.Run(new Form1());
        }
    }
}

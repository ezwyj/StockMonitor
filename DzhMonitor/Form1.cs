using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DzhMonitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBoxApp.SelectedIndex = 0;
            comboBoxInvalidate.SelectedIndex = 0;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.ExitThread();
            Application.Exit();
        }
        
        private void buttonStart_Click(object sender, EventArgs e)
        {

            var runObject = new MonitorCore.CoreAnalysis();
            runObject.SetAppHandler(comboBoxApp.SelectedItem.ToString());

        }
    }
}

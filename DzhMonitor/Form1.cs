using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            Program.CoreAnalysis.SetAppHandler(comboBoxApp.SelectedItem.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.ExitThread();
            Application.Exit();
        }
        
        private void buttonStart_Click(object sender, EventArgs e)
        {
            string stockName = "";
            Stopwatch sw = new Stopwatch();

            //while (true)
            //{
                sw.Start();
                string v0="", v1="", v2="";
                var bmp = Program.CoreAnalysis.Screenshot(out stockName);
            //Parallel.Invoke(() => v0= Program.CoreAnalysis.CatImageAnalysis(bmp, 0), () => v1 = Program.CoreAnalysis.CatImageAnalysis(bmp, 1), () => v2 = Program.CoreAnalysis.CatImageAnalysis(bmp, 2));
                v0 = Program.CoreAnalysis.CatImageAnalysis(bmp, 0);
                v1 = Program.CoreAnalysis.CatImageAnalysis(bmp, 1);
                v2 = Program.CoreAnalysis.CatImageAnalysis(bmp, 2);
                this.Text = stockName;
                label0.Text = v0;
                label1.Text = v1;
                label2.Text = v2;
                sw.Stop();
                TimeSpan ts = sw.Elapsed;
                label7.Text = "花费时间：" + ts.TotalMilliseconds;
                Thread.Sleep(int.Parse(comboBoxInvalidate.SelectedItem.ToString()));
            //}


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void buttonSZ_Click(object sender, EventArgs e)
        {
            string stockName = "";
            Program.CoreAnalysis.Screenshot(out stockName);
            this.Text = stockName;
            if (Program.CoreAnalysis.MarketList.SZ.Contains(this.Text))
            {
                Cursor.Current = Cursors.WaitCursor;
                Form2 w = new Form2();
                w.ShowDialog();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("当前监视股票未包含在深圳市场中");
            }
        }

        private void buttonSH_Click(object sender, EventArgs e)
        {
            string stockName = "";
            Program.CoreAnalysis.Screenshot(out stockName);
            this.Text = stockName;
            if (Program.CoreAnalysis.MarketList.SH.Contains(this.Text))
            {
                Cursor.Current = Cursors.WaitCursor;
                Form2 w = new Form2();
                w.ShowDialog();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("当前监视股票未包含在上海市场中");
            }
        }
    }
}

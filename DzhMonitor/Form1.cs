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
        private System.Threading.SynchronizationContext synchronizationContext;
        private void buttonStart_Click(object sender, EventArgs e)
        {
            Program.Run = true;
            this.buttonStop.Enabled = true;
            this.buttonStart.Enabled = false;
            string stockName = "";
            Stopwatch sw = new Stopwatch();
            synchronizationContext = SynchronizationContext.Current;
            Task.Run(() =>
            {
            while (Program.Run)
            {
                sw.Start();
                string v0 = "", v1 = "", v2 = "";
                var bmp = Program.CoreAnalysis.Screenshot(out stockName);
                //Parallel.Invoke(() => v0= Program.CoreAnalysis.CatImageAnalysis(bmp, 0), () => v1 = Program.CoreAnalysis.CatImageAnalysis(bmp, 1), () => v2 = Program.CoreAnalysis.CatImageAnalysis(bmp, 2));
                v0 = Program.CoreAnalysis.CatImageAnalysis(bmp, 0);
                v1 = Program.CoreAnalysis.CatImageAnalysis(bmp, 1);
                v2 = Program.CoreAnalysis.CatImageAnalysis(bmp, 2);


                synchronizationContext.Send(a =>
                {
                    this.label0.Text = v0;
                    this.label1.Text = v1;
                    this.label2.Text = v2;
                }, null);


                sw.Stop();
                TimeSpan ts = sw.Elapsed;

            
            synchronizationContext.Send(a =>
            {
                label7.Text = "花费时间：" + ts.TotalMilliseconds;
                this.Text = stockName;
            }, null);
            Thread.Sleep(1000);
            sw.Restart();
        }
            });
     }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void buttonSZ_Click(object sender, EventArgs e)
        {
            buttonStop_Click(sender, e);
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
            buttonStop_Click(sender, e);
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

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Program.Run = false;
            this.buttonStart.Enabled = true;
            this.buttonStop.Enabled = false;
        }
    }
}

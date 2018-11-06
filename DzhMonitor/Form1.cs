using MonitorCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        FileStream _file = new FileStream(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data_{DateTime.Now:yyyyMMdd}.txt"), FileMode.Create);
        private static Bitmap DeepCopyBitmap(Bitmap bitmap)

        {
            try
            {

                Bitmap dstBitmap = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, bitmap);
                    ms.Seek(0, SeekOrigin.Begin);
                    dstBitmap = (Bitmap)bf.Deserialize(ms);
                    ms.Close();
                }
                return dstBitmap;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            Program.Run = true;
            this.TopMost = true;
            this.buttonStop.Enabled = true;
            this.buttonStart.Enabled = false;
            string stockName = "";
            Stopwatch sw = new Stopwatch();
            var path = AppDomain.CurrentDomain.BaseDirectory;
            synchronizationContext = SynchronizationContext.Current;
            //FileStream fs = new FileStream(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data_{DateTime.Now:yyyyMMdd}.txt"), FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            //StreamReader sr = new StreamReader (fs,Encoding.Default);
            StreamWriter writer1 = new StreamWriter(_file);
            double MaxTs = 0;
            Task.Run(() =>
            {
                
            while (Program.Run)
            {
                sw.Start();
                string v0 = "", v1 = "", v2 = "";
                Bitmap p0= new Bitmap(10,10), p1 = new Bitmap(10, 10), p2 = new Bitmap(10, 10);
                var bmp = Program.CoreAnalysis.Screenshot(out stockName);
                    var bmp0 = DeepCopyBitmap(bmp);
                    var bmp1 = DeepCopyBitmap(bmp);
                    var bmp2 = DeepCopyBitmap(bmp);
                    var x = Program.CoreAnalysis.MarketList.SH.Contains(stockName) ? 3 : 0;
                    Parallel.Invoke(
                        () => {
                            if (radioButton0Local.Checked)
                            {
                                
                                v0 = Program.CoreAnalysis.CatImageAnalysis(bmp0, 0+x, out p0);
                            }
                            else
                            {
                                v0 = Program.CoreAnalysis.CatImageAnalysis2(bmp0, 0 + x, out p0);
                            }
                        }
                        , 
                        () =>
                        {
                            if (radioButton1Local.Checked)
                            {
                                v1 = Program.CoreAnalysis.CatImageAnalysis(bmp1, 1 + x, out p1);
                            }
                            else
                            {
                                v1 = Program.CoreAnalysis.CatImageAnalysis2(bmp1, 1 + x, out p1);
                            }
                        },
                        () =>
                        {
                            if (radioButton2Local.Checked)
                            {
                                v2 = Program.CoreAnalysis.CatImageAnalysis(bmp2, 2 + x, out p2);
                            }
                            else
                            {
                                v2 = Program.CoreAnalysis.CatImageAnalysis2(bmp2, 2 + x, out p2);
                            }
                        }
                       
                    );
                    //if (radioButton0Local.Checked)
                    //{
                    //    v0 = Program.CoreAnalysis.CatImageAnalysis(bmp, 0, out p0);
                    //}
                    //else
                    //{
                    //    v0 = Program.CoreAnalysis.CatImageAnalysis2(bmp, 0, out p0);
                    //}
                    //if (radioButton1Local.Checked)
                    //{
                    //    v1 = Program.CoreAnalysis.CatImageAnalysis(bmp, 1, out p1);
                    //}
                    //else
                    //{
                    //    v1 = Program.CoreAnalysis.CatImageAnalysis2(bmp, 1, out p1);
                    //}
                    //if (radioButton2Local.Checked)
                    //{
                    //    v2 = Program.CoreAnalysis.CatImageAnalysis(bmp, 2, out p2);
                    //}
                    //else
                    //{
                    //    v2 = Program.CoreAnalysis.CatImageAnalysis2(bmp, 2, out p2);
                    //}

                synchronizationContext.Send(a =>
                {


                    this.label0.Text = v0;
                    this.label1.Text = v1;
                    this.label2.Text = v2;
                    this.pictureBox0.Image = p0;
                    this.pictureBox1.Image = p1;
                    this.pictureBox2.Image = p2;
                }, null);


                    sw.Stop();
                TimeSpan ts = sw.Elapsed;

            
            synchronizationContext.Send(a =>
            {
                label7.Text = "花费时间：" + ts.TotalMilliseconds;
                if(ts.TotalMilliseconds>MaxTs)

                {
                    MaxTs = ts.TotalMilliseconds;
                    label8.Text = "最高花费时间：" + MaxTs.ToString();
                }
                this.Text = stockName;
                writer1.WriteLine("{0:yyyy-MM-dd HH:mm:ss},股票:{1}，买一价:{2}，买1量:{3},买一笔数:{4}", DateTime.Now, stockName, v0, v1, v2);
                writer1.Flush();
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
            
            string stockName = "";
            Program.CoreAnalysis.Screenshot(out stockName);
            this.Text = stockName;
            if (Program.CoreAnalysis.MarketList.SZ.Contains(this.Text))
            {
                Cursor.Current = Cursors.WaitCursor;
                Form2 w = new Form2("SZ");
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
                Form2 w = new Form2("SH");
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
            this.TopMost = false;
            Program.Run = false;
            this.buttonStart.Enabled = true;
            this.buttonStop.Enabled = false;

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Win32API.SetForegroundWindow(Program.CoreAnalysis.AppMainHandler);
            string Input = "SZ000532";
            byte[] ch = (ASCIIEncoding.ASCII.GetBytes(Input));
            for (int i = 0; i < ch.Length; i++)
            {
                KeyBoard.keyPress(ch[i]);
            }
            KeyBoard.keyPress(KeyBoard.vKeyExecute);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Win32API.SetForegroundWindow(Program.CoreAnalysis.AppMainHandler);
            string Input = "SZ000020";
            byte[] ch = (ASCIIEncoding.ASCII.GetBytes(Input));
            for (int i = 0; i < ch.Length; i++)
            {
                KeyBoard.keyPress(ch[i]);
            }
            KeyBoard.keyPress(KeyBoard.vKeyExecute);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

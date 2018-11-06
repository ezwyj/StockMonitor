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
    public partial class Form2 : Form
    {
        private int currentControl = 0;
        private Bitmap screen = null;
        private int _market;
        public Form2(string v)
        {
            InitializeComponent();
            if(v=="SZ")
            {
                _market = 0;
            }
            else
            {
                _market = 3;
            }

        }

        public void Init()
        {
            string stockTitle = "";
            currentControl = 0;
            screen = Program.CoreAnalysis.Screenshot(out stockTitle);
            radioButton1.Checked = true;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            pictureBoxScreen.Image = screen;
            screen.Save(Application.StartupPath + "\\main.png");
            int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;

            int iActulaHeight = Screen.PrimaryScreen.Bounds.Height;
            var fileName = "for" + iActulaWidth.ToString() + iActulaHeight.ToString() + ".json";
            var pathFile = Application.StartupPath + "\\" + fileName;
            if (System.IO.File.Exists(pathFile))
            {
                var str = System.IO.File.ReadAllText(pathFile);
                var x = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MonitorCore.Entity.MonitorLocation>>(str);
                Program.CoreAnalysis.MonitorList = x;
            }

            this.Text = stockTitle;
            //Program.CoreAnalysis.LayoutAnalysis(Application.StartupPath + "\\main.png");
            for (int i = 0; i < 3; i++)
            {
                currentControl = i;
                buttonLeft_Click(null, null);
                
            }
            currentControl = 0;
        }
        public Bitmap CatImage(int i)
        {
            System.Drawing.Rectangle rectangle = new Rectangle();
            rectangle.X = Convert.ToInt16(Program.CoreAnalysis.MonitorList[i + _market].X );
            rectangle.Y = Convert.ToInt16(Program.CoreAnalysis.MonitorList[i + _market].Y );
            rectangle.Width = Program.CoreAnalysis.MonitorList[i + _market].Width;
            rectangle.Height = Program.CoreAnalysis.MonitorList[i + _market].Height;
            Bitmap catedImage = screen.Clone(rectangle, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            return catedImage;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {


            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            currentControl = 1;
            radioButton1.Checked = false;
            radioButton3.Checked = false;
            numericUpDownWidth.Text = Program.CoreAnalysis.MonitorList[currentControl + _market].Width.ToString();
            numericUpDownHeight.Text = Program.CoreAnalysis.MonitorList[currentControl + _market].Height.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            currentControl = 0 ;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            numericUpDownWidth.Text = Program.CoreAnalysis.MonitorList[currentControl + _market].Width.ToString();
            numericUpDownHeight.Text = Program.CoreAnalysis.MonitorList[currentControl + _market].Height.ToString();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            currentControl = 2;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            numericUpDownWidth.Text = Program.CoreAnalysis.MonitorList[currentControl + _market].Width.ToString();
            numericUpDownHeight.Text = Program.CoreAnalysis.MonitorList[currentControl + _market].Height.ToString();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var fileContent = Newtonsoft.Json.JsonConvert.SerializeObject(Program.CoreAnalysis.MonitorList);
            int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;

            int iActulaHeight = Screen.PrimaryScreen.Bounds.Height;
            var fileName = "for" + iActulaWidth.ToString() + iActulaHeight.ToString() + ".json";
            System.IO.File.WriteAllText(Application.StartupPath+"\\"+fileName, fileContent);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            Program.CoreAnalysis.MonitorList[currentControl + _market].Y = Program.CoreAnalysis.MonitorList[currentControl + _market].Y - 1;
            var tempBitmap =  CatImage(currentControl);
            switch (currentControl)
            {
                case 0:
                    pictureBox0.Image = tempBitmap;
                    break;
                case 1:
                    pictureBox1.Image = tempBitmap;
                    break;
                case 2:
                    pictureBox2.Image = tempBitmap;
                    break;
            }
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Init();
            buttonExit.Focus();
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            Program.CoreAnalysis.MonitorList[currentControl + _market].Y = Program.CoreAnalysis.MonitorList[currentControl + _market].Y + 1;
            if (Program.CoreAnalysis.MonitorList[currentControl + _market].Y < 0)
            {
                Program.CoreAnalysis.MonitorList[currentControl + _market].Y = 0;
            }
            var tempBitmap = CatImage(currentControl);
            switch (currentControl)
            {
                case 0:
                    pictureBox0.Image = tempBitmap;
                    break;
                case 1:
                    pictureBox1.Image = tempBitmap;
                    break;
                case 2:
                    pictureBox2.Image = tempBitmap;
                    break;
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            Program.CoreAnalysis.MonitorList[currentControl + _market].X = Program.CoreAnalysis.MonitorList[currentControl + _market].X - 1;
            if (Program.CoreAnalysis.MonitorList[currentControl + _market].X < 0)
            {
                Program.CoreAnalysis.MonitorList[currentControl + _market].X = 0;
            }
            draw();
        }
        private void draw()
        {
            var tempBitmap = CatImage(currentControl);
            switch (currentControl)
            {
                case 0:
                    pictureBox0.Image = tempBitmap;
                    break;
                case 1:
                    pictureBox1.Image = tempBitmap;
                    break;
                case 2:
                    pictureBox2.Image = tempBitmap;
                    break;
            }
        }
        private void buttonRight_Click(object sender, EventArgs e)
        {
            Program.CoreAnalysis.MonitorList[currentControl + _market].X = Program.CoreAnalysis.MonitorList[currentControl + _market].X + 1;
            draw();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Left ))
            {
                buttonLeft_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Right))
            {
                buttonRight_Click(null, null);
                return true;
            }

            if (keyData == (Keys.Control | Keys.Down))
            {
                buttonDown_Click(null, null);
                return true;
            }

            if (keyData == (Keys.Control | Keys.Up))
            {
                buttonUp_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
          
        //    switch(e.KeyCode )
        //    {
        //        case Keys.Left:
                    
        //            break;
        //        case Keys.Right:
        //            buttonRight_Click(sender, e);
        //            break;
        //        case Keys.Up:
        //            buttonUp_Click(sender, e);
        //            break;
        //        case Keys.Down:
        //            buttonDown_Click(sender, e);
        //            break;

        //    }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Program.CoreAnalysis.MonitorList[currentControl + _market].Width += 1;
            draw();
        }

        private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            
            Program.CoreAnalysis.MonitorList[currentControl + _market].Height= int.Parse(numericUpDownHeight.Value.ToString());
            draw();
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            Program.CoreAnalysis.MonitorList[currentControl + _market].Width = int.Parse(numericUpDownWidth.Value.ToString());
            draw();
        }
    }
}

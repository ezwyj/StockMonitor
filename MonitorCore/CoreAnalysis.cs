using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonitorCore.Entity;
using Tesseract;

namespace MonitorCore
{
    public class CoreAnalysis
    {

        public Market MarketList { get; set; }
        private List<Entity.MonitorLocation> _monitorList;
        public static List<TesseractEngine> OcrEngine { get; set; }
        
        private IntPtr AppMainHandler = IntPtr.Zero;
        
        public List<Entity.MonitorLocation> MonitorList
        {
            get { return _monitorList; }
            set { _monitorList = value; }
        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        public CoreAnalysis()
        {
            OcrEngine = new List<TesseractEngine>(3);
            _monitorList = new List<MonitorLocation>();
            for (int i = 0; i < 3; i++)
            {
                OcrEngine.Add(new TesseractEngine("./tessdata", "eng", EngineMode.CubeOnly));
                OcrEngine[i].SetVariable("tessedit_char_whitelist", "0123456789");
                var one = new MonitorLocation();
                one.Width = 40;
                one.Height = 20;
                one.Name = "买一价";
                _monitorList.Add(one);
                var two = new MonitorLocation();
                two.Width = 40;
                two.Height = 20;
                two.Name = "买一量";
                _monitorList.Add(two);
                var three = new MonitorLocation();
                three.Width = 30;
                three.Height = 20;
                three.Name = "买一笔";
                _monitorList.Add(three);
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(System.IO.File.ReadAllText(Application.StartupPath + @"\stocklist.json", System.Text.Encoding.Default));
            MarketList = Newtonsoft.Json.JsonConvert.DeserializeObject<Market>(sb.ToString());
        }

        

        public void SetAppHandler(string progrmaName)
        {
            #region 定位进程
            var ProcessList = Process.GetProcesses();
           
            foreach (var item in ProcessList)
            {
                if (item.ProcessName == progrmaName)
                {
                    appProcessId = item.Id;
                    AppMainHandler = item.MainWindowHandle;
                    break;
                }
            }
            if (AppMainHandler == IntPtr.Zero)
            {
                throw new Exception("未找到监控程序");
            }
            #endregion
            Win32API.SetForegroundWindow(AppMainHandler);
            hScrDc = Win32API.GetWindowDC(AppMainHandler);
            Win32API.RECT windowRect = new Win32API.RECT();
            Win32API.GetWindowRect(AppMainHandler, ref windowRect);
            int width = Convert.ToInt16(Math.Abs(windowRect.Right - windowRect.Left) );
            int height = Convert.ToInt32(Math.Abs(windowRect.Bottom - windowRect.Top) ); ;
            hBitmap = Win32API.CreateCompatibleBitmap(hScrDc, width, height);
            hMemDc = Win32API.CreateCompatibleDC(hScrDc);
            if (width < 300)
            {
                throw new Exception("未显示窗口，请在屏幕最大化监控程序窗口");
            }
           
        }
        private IntPtr hBitmap, hMemDc, hScrDc;
        private int appProcessId = 0;

        public void SetMonitorLocation(List<MonitorLocation> monitorList)
        {
            _monitorList = monitorList;
        }

        /// <summary>
        /// 图
        /// </summary>
        public Bitmap Screenshot(out string stockName)
        {

            hMemDc = Win32API.CreateCompatibleDC(hScrDc);
            Win32API.SetForegroundWindow(AppMainHandler);
            Win32API.SelectObject(hMemDc, hBitmap);
            Win32API.PrintWindow(AppMainHandler, hMemDc, 0);
            Bitmap bmp = Image.FromHbitmap(hBitmap);
            Win32API.DeleteDC(hMemDc);
            //bmp.Save(@"d:\a_" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".png");
            stockName = Process.GetProcessById(appProcessId).MainWindowTitle.Replace("大智慧","").Replace("[","").Replace("]","").Replace(" ","").Replace("-","");
            return bmp;
        }

        /// <summary>
        /// 版面分析
        /// </summary>
        private void LayoutAnalysis(string pathName)
        {
            var client = new Baidu.Aip.Ocr.Ocr(BaiduHelper.API_KEY, BaiduHelper.SECRET_KEY);

            //导入定位图片
            var image = File.ReadAllBytes(pathName);

            //定位“委买队列”
            // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
            var options = new Dictionary<string, object>{
                    {"recognize_granularity", "big"},
                    {"language_type", "CHN_ENG"},
                    {"detect_direction", "true"},
                    {"detect_language", "true"},
                    {"vertexes_location", "true"},
                    {"probability", "true"}
                };
            try
            {

            
            // 带参数调用通用文字识别（含位置信息版）, 图片参数为本地图片
                var result = client.General(image, options);
                var show = result["words_result"].ToList();
                var basic = show.Where(a => a["words"].ToString().StartsWith("委买队列")).FirstOrDefault();
                if (basic == null)
                {
                    //二次高精度
                    var AccurateOptions = new Dictionary<string, object>{
                        {"recognize_granularity", "big"},
                        {"detect_direction", "true"},
                        {"vertexes_location", "true"},
                        {"probability", "true"}
                    };
                    result = client.Accurate(image, AccurateOptions);
                    show = result["words_result"].ToList();
                    basic = show.Where(a => a["words"].ToString().StartsWith("委买队列")).FirstOrDefault();
                }
                if (basic == null)
                {
                    throw new Exception("分析失败");
                }
                int width = Convert.ToUInt16(basic["location"]["width"].ToString());
                int height = Convert.ToUInt16(basic["location"]["height"].ToString());
                int x = Convert.ToUInt16(basic["location"]["left"].ToString());
                int y = Convert.ToUInt16(basic["location"]["top"].ToString());

            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public string CatImageAnalysis(Bitmap bmp, int i)
        {
            System.Drawing.Rectangle rectangle = new Rectangle();
            rectangle.X = Convert.ToInt16(_monitorList[i].X );
            rectangle.Y = Convert.ToInt16(_monitorList[i].Y );
            rectangle.Width = _monitorList[i].Width;
            rectangle.Height = _monitorList[i].Height;
            Bitmap catedImage = bmp.Clone(rectangle, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            catedImage.Save(Application.StartupPath +"\\"+i.ToString() +"_" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".png");
            var page = OcrEngine[i].Process(catedImage);
            
            var text = page.GetText().Replace("\n","").Replace("T","7").Replace("S","9").Replace("I","1");
            page.Dispose();
            catedImage.Dispose();
            return text;
        }

        

        
    }
}

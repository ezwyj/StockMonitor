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
using static MonitorCore.Win32API;

namespace MonitorCore
{
    public class CoreAnalysis
    {
        //158 14702966  6Lc2ZkIFFQA5DOMQlw6jIjLU  YEV4EbT6q7Zz79Ivk20vbE8XYrebF4o1
        public Market MarketList { get; set; }
        private List<Entity.MonitorLocation> _monitorList;
        public static List<TesseractEngine> OcrEngine { get; set; }
        public static List<Baidu.Aip.Ocr.Ocr> OcrEngine2 { get; set; }

        public IntPtr AppMainHandler = IntPtr.Zero;
        
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
            OcrEngine2 = new List<Baidu.Aip.Ocr.Ocr>(3);
            _monitorList = new List<MonitorLocation>();

            var te0 = new TesseractEngine("./tessdata", "eng", EngineMode.CubeOnly);
            te0.SetVariable("tessedit_char_whitelist", "0123456789");
            OcrEngine.Add(te0);

            var te1 = new TesseractEngine("./tessdata", "eng", EngineMode.TesseractOnly);
            te1.SetVariable("tessedit_char_whitelist", "0123456789");
            OcrEngine.Add(te1);

            var te2 = new TesseractEngine("./tessdata", "eng", EngineMode.CubeOnly);
            te2.SetVariable("tessedit_char_whitelist", "0123456789");
            OcrEngine.Add(te2);

            var client0 = new Baidu.Aip.Ocr.Ocr(BaiduHelper.API_KEY, BaiduHelper.SECRET_KEY);
            OcrEngine2.Add(client0);
            var client1 = new Baidu.Aip.Ocr.Ocr(BaiduHelper.API_KEY2, BaiduHelper.SECRET_KEY2);
            OcrEngine2.Add(client1);
            var client2= new Baidu.Aip.Ocr.Ocr(BaiduHelper.API_KEY, BaiduHelper.SECRET_KEY);
            OcrEngine2.Add(client2);

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
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(System.IO.File.ReadAllText(Application.StartupPath + @"\stocklist.json", System.Text.Encoding.Default));
            MarketList = Newtonsoft.Json.JsonConvert.DeserializeObject<Market>(sb.ToString());
        }


        //寻找系统的全部窗口
        private WindowInfo[] GetAllDesktopWindows()
        {
            List<WindowInfo> wndList = new List<WindowInfo>();
            Win32API.EnumWindows(delegate (IntPtr hWnd, int lParam)
            {
                WindowInfo wnd = new WindowInfo();
                StringBuilder sb = new StringBuilder(256);
                //get hwnd
                wnd.hWnd = hWnd;
                //get window name
                Win32API.GetWindowTextW(hWnd, sb, sb.Capacity);
                wnd.szWindowName = sb.ToString();
                //get window class
                Win32API.GetClassNameW(hWnd, sb, sb.Capacity);
                wnd.szClassName = sb.ToString();
                Console.WriteLine("Window handle=" + wnd.hWnd.ToString().PadRight(20) + " szClassName=" + wnd.szClassName.PadRight(20) + " szWindowName=" + wnd.szWindowName);
                //add it into list
                wndList.Add(wnd);
                return true;
            }, 0);
            return wndList.ToArray();
        }
        public void SetAppHandler(string progrmaName)
        {
            var ProcessList = Process.GetProcesses();

            foreach (var item in ProcessList)
            {
                if (item.ProcessName == progrmaName)
                {
                    appProcessId = item.Id;
                    
                    break;
                }
            }
            #region 定位进程
            Win32API.WindowInfo[] a = GetAllDesktopWindows();
            int i = 0;
            int index = 0;
            for (i = 0; i < a.Length; i++)
            {
                
                // MessageBox.Show(a[i].szWindowName.ToString());
                if (a[i].szWindowName.ToString().Contains("大智慧") && a[i].szClassName.Contains("Afx"))
                {
                    Debug.WriteLine(a[i].szWindowName.ToString());
                    index = i;
                }
            }
            AppMainHandler = a[index].hWnd;
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
            int height = Convert.ToInt16(Math.Abs(windowRect.Bottom - windowRect.Top) ); 
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
            StringBuilder title = new StringBuilder(256);
            GetWindowText(AppMainHandler, title, title.Capacity);//得到窗口的标题 
            Win32API.GetWindowTextW(AppMainHandler, title, title.Capacity);
            stockName =  title.ToString().Replace("大智慧","").Replace("[","").Replace("]","").Replace(" ","").Replace("-","");
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


        public string CatImageAnalysis(Bitmap bmp, int i,out Bitmap catedImage)
        {
            System.Drawing.Rectangle rectangle = new Rectangle();
            rectangle.X = Convert.ToInt16(_monitorList[i].X );
            rectangle.Y = Convert.ToInt16(_monitorList[i].Y );
            rectangle.Width = Convert.ToInt16(  _monitorList[i].Width);
            rectangle.Height = Convert.ToInt16( _monitorList[i].Height);
            catedImage = bmp.Clone(rectangle, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //catedImage.Save(Application.StartupPath +"\\save\\"+i.ToString() +"_" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".png");
            var page = OcrEngine[i].Process(catedImage);
            
            var text = page.GetText().Replace("\n","");
            page.Dispose();
            //catedImage.Dispose();
            return text;
        }
        public string CatImageAnalysis2(Bitmap bmp, int i, out Bitmap catedImage)
        {
            
            System.Drawing.Rectangle rectangle = new Rectangle();
            rectangle.X = Convert.ToInt16(_monitorList[i].X);
            rectangle.Y = Convert.ToInt16(_monitorList[i].Y);
            rectangle.Width = Convert.ToInt16(_monitorList[i].Width);
            rectangle.Height = Convert.ToInt16(_monitorList[i].Height);
            catedImage = bmp.Clone(rectangle, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            string text = string.Empty;
            using (MemoryStream stream = new MemoryStream())
            {
                catedImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
                var options = new Dictionary<string, object>{
                       {"language_type", "CHN_ENG"},
                        {"detect_direction", "true"},
                        {"detect_language", "true"},
                        {"probability", "true"}
                };
                // 带参数调用通用文字识别（含位置信息版）, 图片参数为本地图片
                var result = OcrEngine2[i].GeneralBasic (data, options);
                if (result["words_result"].Count()>0)
                {
                    text = result["words_result"][0]["words"].ToString();
                }
                
               
            }
            return text;


        }




    }
}

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
using Tesseract;

namespace MonitorCore
{
    public class CoreAnalysis
    {


        private List<Entity.MonitorLocation> _monitorList;
        public static List<TesseractEngine> OcrEngine { get; set; }
        
        private IntPtr AppMainHandler = IntPtr.Zero;
        

        /// <summary>
        /// 初始化函数
        /// </summary>
        public CoreAnalysis()
        {
            OcrEngine = new List<TesseractEngine>(3);
            for (int i = 0; i < 3; i++)
            {
                OcrEngine.Add(new TesseractEngine("./tessdata", "eng", EngineMode.CubeOnly));
                OcrEngine[i].SetVariable("tessedit_char_whitelist", "0123456789");
            }
            LayoutAnalysis();
        }

        public void SetAppHandler(string progrmaName)
        {
            #region 定位进程
            var ProcessList = Process.GetProcesses();
            var appProcessId = 0;
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
            int width = Convert.ToInt16(Math.Abs(windowRect.Right - windowRect.Left) * 1.25);
            int height = Convert.ToInt32(Math.Abs(windowRect.Bottom - windowRect.Top) * 1.25); ;
            hBitmap = Win32API.CreateCompatibleBitmap(hScrDc, width, height);
            hMemDc = Win32API.CreateCompatibleDC(hScrDc);
           
        }
        private IntPtr hBitmap, hMemDc, hScrDc;
        /// <summary>
        /// 图
        /// </summary>
        public Bitmap Screenshot()
        {

            hMemDc = Win32API.CreateCompatibleDC(hScrDc);
            Win32API.SetForegroundWindow(AppMainHandler);
            Win32API.SelectObject(hMemDc, hBitmap);
            Win32API.PrintWindow(AppMainHandler, hMemDc, 0);
            Bitmap bmp = Image.FromHbitmap(hBitmap);
            Win32API.DeleteDC(hMemDc);
            //bmp.Save(@"d:\a_" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ".png");
            return bmp;
        }

        /// <summary>
        /// 版面分析
        /// </summary>
        public void LayoutAnalysis()
        {
            var client = new Baidu.Aip.Ocr.Ocr(BaiduHelper.API_KEY, BaiduHelper.SECRET_KEY);

            //导入定位图片
            var image = File.ReadAllBytes("");

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
                var basic = show.Where(a => a["words"].ToString().StartsWith("委买队列"));
                if (basic.Count() == 0)
                {
                    throw new Exception("未定位到标识位置,请切换！");
                }

                //System.Reflection.Assembly Asmb = System.Reflection.Assembly.GetExecutingAssembly();
                //string strName = Asmb.GetName().Name + ".cfg.json";
                //System.IO.Stream ManifestStream = Asmb.GetManifestResourceStream(strName);

                //byte[] StreamData = new byte[ManifestStream.Length];
                //ManifestStream.Read(StreamData, 0, (int)ManifestStream.Length);
                //System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(strName);
                //System.IO.StreamReader sr = new System.IO.StreamReader(stream, Encoding.Default);
                //var conString = sr.ReadToEnd();
                //var configList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entity.CfgLocation>>(conString);

                //int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;

                //int iActulaHeight = Screen.PrimaryScreen.Bounds.Height;
                //foreach (var item in configList)
                //{
                    //if (item.Resolution == iActulaWidth.ToString() + iActulaHeight.ToString())
                    //{
                    //    if (!_monitorList.ContainsKey(item.Name))
                    //    {
                    //        Entity.MonitorLocation one = new Entity.MonitorLocation();
                    //        one.Name = item.Name;
                    //        one.X = item.
                    //        _monitorList.Add(item.Resolution, one);
                    //    }
                    //}

               // }
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public string CatImageAnalysis(Bitmap bmp, int i)
        {
            System.Drawing.Rectangle rectangle = new Rectangle();
            rectangle.X = Convert.ToInt16(_monitorList[i].X * 1.25);
            rectangle.Y = Convert.ToInt16(_monitorList[i].Y * 1.25);
            rectangle.Width = _monitorList[i].Width;
            rectangle.Height = _monitorList[i].Height;
            Bitmap catedImage = bmp.Clone(rectangle, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
           
            return Ocr(catedImage, i);
        }

        public string Ocr(Bitmap img, int i)
        {

            var page = OcrEngine[i].Process(img);
            var result = page.GetText();
            return  result;
        }
    }
}

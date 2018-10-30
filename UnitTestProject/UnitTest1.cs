﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonitorCore;
using Tesseract;

namespace UnitTestProject
{
    public static class AccessToken

    {
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例
        public static String TOKEN = "24.adda70c11b9786206253ddb70affdc46.2592000.1493524354.282335-1234567";

        // 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
        private static String clientId = "nN5shGjIsPuVK0dN3cZP56rN";
        // 百度云中开通对应服务应用的 Secret Key
        private static String clientSecret = "pyik6Od7p1kgnL4BdX3MefAqjHM2SNHd";

        public static String getAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", clientId));
            paraList.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            TOKEN = result;
            Console.WriteLine(result);
            return result;
        }
    }
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// 定位委买队列位置
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            var fileList = new string[] { "AA", "BB", "CC", "DD" };
            

            var client = new Baidu.Aip.Ocr.Ocr(BaiduHelper.API_KEY, BaiduHelper.SECRET_KEY);
            foreach (var fItem in fileList)
            {
                //var ocr = new TesseractEngine("./tessdata", "chi_sim", EngineMode.Default);
                //ocr.SetVariable("tessedit_char_whitelist", "委买卖一");

                var img = new Bitmap(@"F:\byj\pic\" + fItem + ".png");
                var image = File.ReadAllBytes(@"F:\byj\pic\" + fItem + ".png");
                // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
                var options = new Dictionary<string, object>{
                    {"recognize_granularity", "big"},
                    {"language_type", "CHN_ENG"},
                    {"detect_direction", "true"},
                    {"detect_language", "true"},
                    {"vertexes_location", "true"},
                    {"probability", "true"}
                };
                // 带参数调用通用文字识别（含位置信息版）, 图片参数为本地图片
                var result = client.General(image, options);
                var show = result["words_result"].ToList();
                var one = show.Where(a => a["words"].ToString().StartsWith("委买队列")).FirstOrDefault();
                Newtonsoft.Json.Linq.JToken buyone = null ;
                if (one== null)
                {
                    var AccurateOptions = new Dictionary<string, object>{
                        {"recognize_granularity", "big"},
                        {"detect_direction", "true"},
                        {"vertexes_location", "true"},
                        {"probability", "true"}
                    };
                    result = client.Accurate(image, AccurateOptions);
                    show = result["words_result"].ToList();
                    one = show.Where(a => a["words"].ToString().StartsWith("委买队列")).FirstOrDefault();
                    buyone = show.Where(a => a["words"].ToString().StartsWith("买一")).FirstOrDefault();
                }
                if(one==null)
                {
                    throw new Exception("分析失败 ");
                }
                Pen myPen = new Pen(System.Drawing.Color.BurlyWood, 3);
                var g = Graphics.FromImage(img);
                int width = Convert.ToUInt16(one["location"]["width"].ToString());
                int height = Convert.ToUInt16(one["location"]["height"].ToString());
                int x = Convert.ToUInt16(one["location"]["left"].ToString());
                int y = Convert.ToUInt16(one["location"]["top"].ToString());

                g.DrawRectangle(myPen, new Rectangle(x, y, width, height));

                 width = Convert.ToUInt16(buyone["location"]["width"].ToString());
                 height = Convert.ToUInt16(buyone["location"]["height"].ToString());
                 x = Convert.ToUInt16(buyone["location"]["left"].ToString());
                 y = Convert.ToUInt16(buyone["location"]["top"].ToString());
                g.DrawRectangle(myPen, new Rectangle(x, y, width, height));

                g.DrawRectangle(myPen, new Rectangle(x - 68, y + height, 55, height));

                //g.DrawRectangle(myPen, new Rectangle(x - 40, y + 37, 80, 31));

                g.DrawRectangle(myPen, new Rectangle(x + 36, y + height, 40, height));

                g.DrawRectangle(myPen, new Rectangle(x + 89, y + height, 30, height));
                Bitmap bmp = new Bitmap(img);
                img.Dispose();
                bmp.Save(@"F:\byj\pic\" + fItem + "_b.png"); //



            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var img = new Bitmap(@"F:\byj\pic\a1.png");
            var OcrEngine = new TesseractEngine("./tessdata", "eng", EngineMode.CubeOnly);
            OcrEngine.SetVariable("tessedit_char_whitelist", "0123456789");
            var page = OcrEngine.Process(img);
            var result = page.GetText();
           
        }
    }
}
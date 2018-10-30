using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonitorCore
{

    public static class BaiduHelper
    {
        public static string APP_ID { get { return "14565074"; } }
        public static string API_KEY
        {
            get
            {
                return "nN5shGjIsPuVK0dN3cZP56rN";
            }
        }
        public static string SECRET_KEY
        {
            get
            {
                return "pyik6Od7p1kgnL4BdX3MefAqjHM2SNHd";
            }
        }
        // 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
        // 返回token示例
        public static String TOKEN = "";

    

        public static String getAccessToken()
        {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            HttpClient client = new HttpClient();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>>();
            paraList.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            paraList.Add(new KeyValuePair<string, string>("client_id", API_KEY));
            paraList.Add(new KeyValuePair<string, string>("client_secret", SECRET_KEY));

            HttpResponseMessage response = client.PostAsync(authHost, new FormUrlEncodedContent(paraList)).Result;
            String result = response.Content.ReadAsStringAsync().Result;
            TOKEN = result;
            Console.WriteLine(result);
            return result;
        }
    }
}

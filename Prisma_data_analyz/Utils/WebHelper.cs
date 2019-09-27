/***************************************************************************************************************************************************
 * *文件名：WebHelper.cs
 * *创建人：Jon
 * *日 期 ：2018年5月25日
 * *描 述 ：实现HTTP协议中的GET、POST请求
 * *MVC使用HttpClient上传文件实例：      
        public IActionResult Upload()
        {

            var url = "http://localhost:57954/API/Default/values";
            var data = new MultipartFormDataContent();
            if (Request.HasFormContentType)
            {
                var request = Request.Form.Files;
                foreach (var item in request)
                {
                    data.Add(new StreamContent(item.OpenReadStream()), item.Name, item.FileName);
                }

                foreach (var item in Request.Form)
                {
                    data.Add(new StringContent(item.Value), item.Key);
                }
            }
            WebHelper.PostByHttpClientFromHttpContent(url, data);
            return Json("OK");
        }
*****************************************************************************************************************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Expansion.Helper
{
    public static class WebHelper
    {
        /// <summary>
        /// 通过WebRequest发起Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns>JSON字符串</returns>
        public static string GetByWebRequest(string url)
        {
            string jsonString = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            var response = (HttpWebResponse)request.GetResponse();
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                jsonString = stream.ReadToEnd();
            }
            return jsonString;
        }

        /// <summary>
        /// 通过WebRequest发起Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns>JSON字符串</returns>
        public static string PostByWebRequest(string url, object data)
        {
            string jsonString = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Timeout = Int32.MaxValue;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            var jsonToSend = JsonConvert.SerializeObject(data, Formatting.None, new IsoDateTimeConverter());
            byte[] btBodys = Encoding.UTF8.GetBytes(jsonToSend);
            request.ContentLength = btBodys.Length;
            request.GetRequestStream().Write(btBodys, 0, btBodys.Length);
            var response = (HttpWebResponse)request.GetResponse();
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                jsonString = stream.ReadToEnd();
            }
            return jsonString;
        }


        /// <summary>
        /// 通过HttpClient发起Get请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns>JSON字符串</returns>
        public static string GetByHttpClient(string url)
        {
            string jsonString = string.Empty;
            using (var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }))
            {
                var taskResponse = client.GetAsync(url);
                taskResponse.Wait();
                if (taskResponse.IsCompleted)
                {
                    var taskStream = taskResponse.Result.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    using (var reader = new StreamReader(taskStream.Result))
                    {
                        jsonString = reader.ReadToEnd();
                    }
                }
            }
            return jsonString;
        }

        /// <summary>
        /// 通过HttpClient发起Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求参数</param>
        /// <returns>JSON字符串</returns>
        public static string PostByHttpClient(string url, object data)
        {
            string jsonString = string.Empty;
            using (var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }))
            {
                var jsonToSend = JsonConvert.SerializeObject(data, Formatting.None, new IsoDateTimeConverter());
                var body = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                var taskResponse = client.PostAsync(url, body);
                taskResponse.Wait();
                if (taskResponse.IsCompleted)
                {
                    var taskStream = taskResponse.Result.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    using (var reader = new StreamReader(taskStream.Result))
                    {
                        jsonString = reader.ReadToEnd();
                    }
                }
            }
            return jsonString;
        }

        /// <summary>
        /// 通过数据来自HttpContent的HttpClient发起Post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="content">请求参数</param>
        /// <returns>JSON字符串</returns>
        public static string PostByHttpClientFromHttpContent(string url, HttpContent content)
        {
            string jsonString = string.Empty;
            using (var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }))
            {
                var taskResponse = client.PostAsync(url, content);
                taskResponse.Wait();
                if (taskResponse.IsCompleted)
                {
                    var taskStream = taskResponse.Result.Content.ReadAsStreamAsync();
                    taskStream.Wait();
                    using (var reader = new StreamReader(taskStream.Result))
                    {
                        jsonString = reader.ReadToEnd();
                    }
                }
            }
            return jsonString;
        }

        /// <summary>
        /// Object转换为StreamContent
        /// </summary>
        /// <param name="data">请求参数</param>
        /// <returns>StreamContent</returns>
        public static HttpContent ToStreamContent(this object data)
        {

            var json = JsonConvert.SerializeObject(data, Formatting.None, new IsoDateTimeConverter());
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            MemoryStream ms = new MemoryStream();
            ms.Write(bytes, 0, bytes.Length);
            ms.Position = 0;
            HttpContent streamContent = new StreamContent(ms);
            return streamContent;
        }

        /// <summary>
        /// Object转换为StringContent
        /// </summary>
        /// <param name="data">请求参数</param>
        /// <returns>StringContent</returns>
        public static HttpContent ToStringContent(this object data)
        {
            HttpContent stringContent = new StringContent(JsonConvert.SerializeObject(data));
            return stringContent;
        }

        /// <summary>
        /// Object转换为MultipartFormDataContent
        /// </summary>
        /// <param name="data"></param>
        /// <returns>MultipartFormDataContent</returns>
        public static HttpContent ToMultipartFormDataContent(this object data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.None, new IsoDateTimeConverter());
            var body = new StringContent(json, Encoding.UTF8, "application/json");
            var multipartFormDataContent = new MultipartFormDataContent();
            multipartFormDataContent.Add(body);
            return multipartFormDataContent;
        }

        /// <summary>
        /// Object转换为FormUrlEncodedContent
        /// </summary>
        /// <param name="data">请求参数</param>
        /// <returns>FormUrlEncodedContent</returns>
        public static HttpContent ToFormUrlEncodedContent(this object data)
        {
            var param = new List<KeyValuePair<string, string>>();
            var values = JObject.FromObject(data);
            foreach (var item in values)
            {
                param.Add(new KeyValuePair<string, string>(item.Key, item.Value.ToString()));
            }
            HttpContent formUrlEncodedContent = new FormUrlEncodedContent(param);
            return formUrlEncodedContent;
        }


        /// <summary>
        /// Object转换为ByteArrayContent
        /// </summary>
        /// <param name="data">请求参数</param>
        /// <returns>FormUrlEncodedContent</returns>
        public static HttpContent ToByteArrayContent(this object data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.None, new IsoDateTimeConverter());
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            HttpContent byteArrayContent = new ByteArrayContent(bytes);
            return byteArrayContent;
        }

        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="encode">编码类型</param>
        /// <returns></returns>
        private static string Encode(string content, Encoding encode = null)
        {
            if (encode == null) return content;

            return System.Web.HttpUtility.UrlEncode(content, Encoding.UTF8);

        }


        /// <summary>
        /// Url解码
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="encode">编码类型</param>
        /// <returns></returns>
        private static string Decode(string content, Encoding encode = null)
        {
            if (encode == null) return content;
            
            return System.Web.HttpUtility.UrlDecode(content, Encoding.UTF8);

        }

        /// <summary>
        /// 将键值对参数集合拼接为Url字符串
        /// </summary>
        /// <param name="paramArray">键值对集合</param>
        /// <param name="encode">转码类型</param>
        /// <returns></returns>
        private static string BuildParam(List<KeyValuePair<string, string>> paramArray, Encoding encode = null)
        {
            string url = "";

            if (encode == null) encode = Encoding.UTF8;

            if (paramArray != null && paramArray.Count > 0)
            {
                var parms = "";
                foreach (var item in paramArray)
                {
                    parms += string.Format("{0}={1}&", Encode(item.Key, encode), Encode(item.Value, encode));
                }
                if (parms != "")
                {
                    parms = parms.TrimEnd('&');
                }
                url += parms;

            }
            return url;
        }
    }
}
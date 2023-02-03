using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace purinawrapper.apis.Models
{
    public class CallAltaRewardApi
    {
        private Logger logger;
        public CallAltaRewardApi() 
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public string AltaRewardAPI(string api, string postParameters, string token)
        {
            var requestId = string.Empty;
            string response = string.Empty;
            logger.Info(api);
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12
                       | SecurityProtocolType.Ssl3;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(api);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                httpWebRequest.Timeout = 3000000;
                httpWebRequest.KeepAlive = false;
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.Headers.Add(HttpRequestHeader.AcceptCharset, "application/json");
                httpWebRequest.Headers.Add("X-AR-CompanyCode", "051");
                httpWebRequest.Headers.Add("X-AR-StoreCode", "PUR");
                httpWebRequest.Headers.Add("X-AR-Source", "Web");
                httpWebRequest.Headers.Add("X-AR-UserID", "PostMan_051");
                httpWebRequest.Headers.Add("X-AR-CountryCode", "US");
                httpWebRequest.Headers.Add("X-AR-Authorization", string.Format("Bearer {0}", token));

                //httpWebRequest.Headers.Add("Content-Type", "application/json");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.MediaType = "httpWebRequest";
                httpWebRequest.Method = "POST";
                ServicePointManager.Expect100Continue = false;
                if (!string.IsNullOrEmpty(postParameters))
                {
                    logger.Debug(postParameters);
                    byte[] bytes2 = Encoding.UTF8.GetBytes(postParameters);
                    httpWebRequest.ContentLength = (long)bytes2.Length;
                    Stream requestStream = httpWebRequest.GetRequestStream();
                    try
                    {
                        requestStream.Write(bytes2, 0, bytes2.Length);
                        requestStream.Close();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        response = ex.Message.ToString();
                    }
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                response = streamReader.ReadToEnd();
                logger.Info(response);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                response = ex.Message.ToString();
            }
            return response;
        }

        public string AltaRewardTokenAPI(string api, string postParameters)
        {
            var requestId = string.Empty;
            string response = string.Empty;
            logger.Info(api);
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12
                       | SecurityProtocolType.Ssl3;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(api);
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                httpWebRequest.Timeout = 3000000;
                httpWebRequest.KeepAlive = false;
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.Headers.Add(HttpRequestHeader.AcceptCharset, "application/json");
                httpWebRequest.Headers.Add("X-AR-grant_type", "051");
                httpWebRequest.Headers.Add("X-AR-StoreCode", "PUR");
                httpWebRequest.Headers.Add("X-AR-Source", "Web");
                httpWebRequest.Headers.Add("X-AR-UserID", "PostMan_051");
                httpWebRequest.Headers.Add("X-AR-CountryCode", "US");
                

                //httpWebRequest.Headers.Add("Content-Type", "application/json");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.MediaType = "httpWebRequest";
                httpWebRequest.Method = "POST";
                ServicePointManager.Expect100Continue = false;
                if (!string.IsNullOrEmpty(postParameters))
                {
                    logger.Debug(postParameters);
                    byte[] bytes2 = Encoding.UTF8.GetBytes(postParameters);
                    httpWebRequest.ContentLength = (long)bytes2.Length;
                    Stream requestStream = httpWebRequest.GetRequestStream();
                    try
                    {
                        requestStream.Write(bytes2, 0, bytes2.Length);
                        requestStream.Close();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        response = ex.Message.ToString();
                    }
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                response = streamReader.ReadToEnd();
                logger.Info(response);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                response = ex.Message.ToString();
            }
            return response;
        }
    }
}
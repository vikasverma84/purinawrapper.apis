using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace purinawrapper.apis.Models
{
    public class AugeoRewardsIssue
    {
        private Logger logger;
        public AugeoRewardsIssue()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public string IssueAugeoRewards(string api, string token, string itemCode, string ItemName, Request req)
        {
            string rs = string.Empty;
            try 
            {
                Random rnd = new Random();
                int myRandomNo = rnd.Next(10000000, 99999999);
                AugeoRewards augeoRewards = new AugeoRewards();
                AddressAugeo addressAugeo = new AddressAugeo();
                List<AddressAugeo> addressAugeos = new List<AddressAugeo>();
                OrderItem orderItem = new OrderItem();
                List<OrderItem> orderedItems = new List<OrderItem>();
                augeoRewards.clientUserId = req.id;
                augeoRewards.clientOrderId= myRandomNo.ToString();
                augeoRewards.email = req.email_address;
                augeoRewards.firstName = req.fname;
                augeoRewards.lastName = req.lname;
                orderItem.productId = itemCode;
                orderItem.qty = "1";
                orderedItems.Add(orderItem);
                addressAugeo.city = req.City;
                addressAugeo.zip = req.PostalCode;
                addressAugeo.countryISOCode = "US";
                addressAugeo.stateISOCode = req.StateProvince;
                addressAugeo.line1 = req.streetAddress;
                addressAugeo.type = req.Address_Type;
                addressAugeo.line2 = req.streetAddress2;
                addressAugeos.Add(addressAugeo);
                augeoRewards.Address = addressAugeos;
                augeoRewards.orderItems = orderedItems;
                var json1 = JsonConvert.SerializeObject(augeoRewards, Formatting.Indented);
                rs = AugeoRewardAPI(api, json1, token);
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
            }

            return rs;
        }
        public string AugeoRewardAPI(string api, string postParameters, string token)
        {
            var requestId = string.Empty;
            string response = string.Empty;
            logger.Info(api);
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(api);
                httpWebRequest.Timeout = 3000000;
                httpWebRequest.KeepAlive = false;
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.Headers.Add("authorization", string.Format("Bearer {0}", token));
                httpWebRequest.ContentType = "application/json";
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
            catch (WebException ex)
            {
                Stream stream = ex.Response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string strMessage = sr.ReadToEnd();
                logger.Error(ex.ToString());
                response = strMessage;
            }
            return response;
        }
    }
}
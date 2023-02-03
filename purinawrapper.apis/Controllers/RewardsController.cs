using Newtonsoft.Json;
using NLog;
using purinawrapper.apis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace purinawrapper.apis.Controllers
{
    public class RewardsController : ApiController
    {
        private Logger logger;
        AltaIssueDigitalVoucher altaIssueDigitalVoucher = new AltaIssueDigitalVoucher();
        AugeoRewardsIssue augeoRewardsIssue = new AugeoRewardsIssue();
        public RewardsController()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        string IssueCapillaryCoupon(string id, string seriesId)
        {
            RootCRM rootCRM = new RootCRM();
            CapillaryCouponRequest capillaryCouponRequest = new CapillaryCouponRequest();
            CouponCRM couponCRM = new CouponCRM();
            List<CouponCRM> couponCRMs = new List<CouponCRM>();
            CustomerCRM customerCRM = new CustomerCRM();
            customerCRM.external_id = id;
            couponCRM.customer = customerCRM;
            couponCRM.series_id = seriesId;
            couponCRMs.Add(couponCRM);
            capillaryCouponRequest.coupon = couponCRMs;
            rootCRM.root = capillaryCouponRequest;
            var json2 = JsonConvert.SerializeObject(rootCRM, Formatting.Indented);
            var intouchResponse = IntouchMakeRequest(@"https://apac.api.capillarytech.com/v1.1/coupon/issue?format=json", json2, "POST");
            return intouchResponse;
        }
        string IntouchMakeRequest(string api, string postParameters, string Method)
        {
            var requestId = string.Empty;
            string response = string.Empty;
            try
            {
                logger.Info(api);
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(api);
                WebRequest.DefaultWebProxy = (IWebProxy)null;
                httpWebRequest.Timeout = 3000000;
                httpWebRequest.KeepAlive = false;
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.Headers.Add(HttpRequestHeader.AcceptCharset, "utf-8");
                httpWebRequest.Headers.Add("Authorization", "Basic ZGVtby5wdXJpbmEuc29sLjE6NjJjYzJkOGI0YmYyZDg3MjgxMjBkMDUyMTYzYTc3ZGY=");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = Method;
                httpWebRequest.UserAgent = "Capillary WebClient";
                ServicePointManager.Expect100Continue = false;
                if (!string.IsNullOrEmpty(postParameters))
                {
                    logger.Info(postParameters);
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
                if (httpWebResponse.Headers != null && !string.IsNullOrEmpty(Convert.ToString(httpWebResponse.Headers["X-Cap-RequestID"])))
                {
                    requestId = Convert.ToString(httpWebResponse.Headers["X-Cap-RequestID"]);
                }
                Stream stream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                response = streamReader.ReadToEnd();
                logger.Info(response);
                logger.Info("CRM request ID: " + requestId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                response = ex.Message.ToString();
            }
            return response;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("reward/add")]
        public HttpResponseMessage RewardAdd([FromBody] Request req)
        {
            var json1 = JsonConvert.SerializeObject(req, Formatting.Indented);
            logger.Info(json1);
            #region var
            var couponCode = req.pid;
            var couponDesc = req.pname;
            var tk = "";
            IntouchCouponResponse intouchCouponResponse = new IntouchCouponResponse();
            Coupon coupon = new Coupon();
            CustomerIntouch customer = new CustomerIntouch();
            ItemStatus itemStatus = new ItemStatus();
            Response intouchResponse = new Response();
            Status status = new Status();
            intouchResponse.coupon = coupon;
            coupon.item_status = itemStatus;
            intouchResponse.status = status;
            intouchCouponResponse.response = intouchResponse;
            status.success = "false";
            status.code = 500;
            status.message = "FAILURE";
            coupon.code = couponCode;
            coupon.description = couponDesc;
            customer.external_id = req.id;
            itemStatus.code = 500;
            itemStatus.success = "false";
            #endregion

            #region Validate Fields
            if (string.IsNullOrEmpty(req.fname))
            {
                itemStatus.message = "First Name can't be empty";
                var j1 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
                logger.Info(j1);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
            }
            if (string.IsNullOrEmpty(req.pid))
            {
                itemStatus.message = "Product Id can't be empty";
                var j1 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
                logger.Info(j1);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
            }
            if (string.IsNullOrEmpty(req.lname))
            {
                itemStatus.message = "last Name can't be empty";
                var j1 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
                logger.Info(j1);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
            }
            if (string.IsNullOrEmpty(req.id))
            {
                itemStatus.message = "Customer ID can't be empty";
                var j1 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
                logger.Info(j1);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
            }
            if (string.IsNullOrEmpty(req.intouch_series_id))
            {
                itemStatus.message = "Intouch Id can't be empty can't be empty";
                var j1 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
                logger.Info(j1);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
            }
            if (req.orderType != "Digital" && req.orderType != "Physical")
            {
                itemStatus.message = "Invalid Order Type";
                var j1 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
                logger.Info(j1);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
            }
            var api = string.Empty;
            if (req.orderType == "Digital")
            {
                api = @"https://tgw.stage.arsecure.com/AR/v1/OrderProcessing/DigitalOrderCreate";
            }
            else if (req.orderType == "Physical")
            {
                #region Validate Address Fields
                if (string.IsNullOrEmpty(req.streetAddress))
                {
                    itemStatus.message = "streetAddress can't be empty";
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
                }
                if (string.IsNullOrEmpty(req.City))
                {
                    itemStatus.message = "City can't be empty";
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
                }
                if (string.IsNullOrEmpty(req.StateProvince))
                {
                    itemStatus.message = "StateProvince can't be empty";
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
                }
                if (string.IsNullOrEmpty(req.PostalCode))
                {
                    itemStatus.message = "PostalCode can't be empty";
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
                }
                if (req.Country != "US")
                {
                    itemStatus.message = "Invaid Country";
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
                }
                api = @"https://tgw.stage.arsecure.com/AR/v1/OrderProcessing/OrderCreate";
                #endregion
            }
            #endregion

            if (req.Vendor == @"AUGEO")
            {
                #region Agueo Token Generate
                try
                {
                    bool isEmail = Regex.IsMatch(req.email_address, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                    if (!isEmail)
                    {
                        itemStatus.message = "Invalid Email Id";
                        var j1 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
                        logger.Info(j1);
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
                    }
                    if (req.Address_Type!= "Shipping" && req.Address_Type!= "Billing")
                    {
                        itemStatus.message = "Invalid Address Type";
                        var j1 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
                        logger.Info(j1);
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
                    }

                    using (var client = new HttpClient())
                    {
                        var postData = new List<KeyValuePair<string, string>>();
                        postData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                        //UAT
                        postData.Add(new KeyValuePair<string, string>("client_id", "ad0e295cb0034e93a3a7d4e54ad8567e"));
                        postData.Add(new KeyValuePair<string, string>("client_secret", "1b6269f4046c46f4aac14835ba198c1e"));

                        //prod
                        //postData.Add(new KeyValuePair<string, string>("client_id", "f328a41e75f64041ac03372e6248ae5c"));
                        //postData.Add(new KeyValuePair<string, string>("client_secret", "b77428ddab734d488b83cbeaf0a8a655"));

                        postData.Add(new KeyValuePair<string, string>("programId", "1961"));
                        postData.Add(new KeyValuePair<string, string>("catalogId", "purina_us"));
                        HttpContent content = new FormUrlEncodedContent(postData);
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                        var responseResult = client.PostAsync("https://aws-api.uat.augeocms.com/oauth/augeoperks/oauth/token", content).Result;
                        string resultString = responseResult.Content.ReadAsStringAsync().Result;
                        tk = resultString;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                    return Request.CreateResponse(HttpStatusCode.OK, intouchCouponResponse);
                }
                #endregion
            }
            else
            {
                #region Alta Token Generate
                try
                {

                    using (var client = new HttpClient())
                    {
                        var postData = new List<KeyValuePair<string, string>>();
                        postData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
                        postData.Add(new KeyValuePair<string, string>("client_id", "69aa60ee-a9fe-4d55-a077-ed72e9b25819"));
                        postData.Add(new KeyValuePair<string, string>("client_secret", "~6jk~KKlZ~Vx7h96t3H17R_gIQVB39TKKO"));
                        postData.Add(new KeyValuePair<string, string>("scope", "api://0ddfa8b1-c107-493c-a918-4dde90007316/.default"));
                        HttpContent content = new FormUrlEncodedContent(postData);
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                        var responseResult = client.PostAsync("https://login.microsoftonline.com/1b191c89-1809-46f1-86d3-4debfb667449/oauth2/v2.0/token", content).Result;
                        string resultString = responseResult.Content.ReadAsStringAsync().Result;
                        tk = resultString;
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                    return Request.CreateResponse(HttpStatusCode.OK, intouchCouponResponse);
                }
                #endregion
            }
           
            #region Issue Coupon
            //try
            //{
            //    var crmCoupons = IssueCapillaryCoupon(req.id, req.intouch_series_id);
            //    DataContractJsonSerializer couoons = new DataContractJsonSerializer(typeof(IntouchCouponResponse));
            //    using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(crmCoupons)))
            //    {
            //        var cr = (IntouchCouponResponse)couoons.ReadObject(ms);
            //        couponCode = cr.response.coupon.code.ToString();
            //        couponDesc = cr.response.coupon.description.ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    itemStatus.message = ex.Message;
            //    var j1 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
            //    logger.Info(j1);
            //    logger.Error(ex.ToString());
            //    return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
            //}
            #endregion


            #region Alta Rewards
            try
            {
                if (string.IsNullOrEmpty(couponCode))
                {
                    itemStatus.message = "Something went wrong while issue Capillary Coupon";
                    var j1 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
                    logger.Info(j1);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
                }
                DataContractJsonSerializer custdes = new DataContractJsonSerializer(typeof(token));
                using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(tk)))
                {
                    logger.Info(tk);
                    token rcust = (token)custdes.ReadObject(ms);
                    var d12 = string.Empty;

                    if (req.Vendor == "AUGEO")
                    {
                        d12 = augeoRewardsIssue.IssueAugeoRewards(@"https://aws-api.uat.augeocms.com/r/v1/orders", rcust.access_token, couponCode, couponDesc, req);
                        //d12 = augeoRewardsIssue.IssueAugeoRewards(@"https://aws-api.augeocms.com/r/v1/orders", rcust.access_token, couponCode, couponDesc, req);
                    }
                    else
                    {
                        d12 = altaIssueDigitalVoucher.IssueRewards(api, rcust.access_token, couponCode, couponDesc, req);
                    }
                    logger.Debug("d12 " + d12);
                    bool flag = false;
                    if (req.Vendor=="AUGEO")
                    {
                        try
                        {
                            if (req.Vendor == "AUGEO")
                            {
                                status.message = d12;
                                itemStatus.message = d12;
                            }
                            DataContractJsonSerializer re = new DataContractJsonSerializer(typeof(RootAugeoResponse));
                            using (MemoryStream msrr = new MemoryStream(Encoding.Unicode.GetBytes(d12)))
                            {
                                RootAugeoResponse rr = (RootAugeoResponse)re.ReadObject(msrr);
                                if (rr.AugeoOrderId > 0)
                                {
                                    if (!string.IsNullOrEmpty(rr.AugeoOrderId.ToString()))
                                    {
                                        itemStatus.message = string.Format(@"User {1}'s Order Id {0}", rr.AugeoOrderId, rr.AugeoUserId);
                                        flag = true;
                                    }
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            logger.Error(ex.ToString());
                        }
                    }
                    else
                    {
                        DataContractJsonSerializer re = new DataContractJsonSerializer(typeof(RootResponse));
                        using (MemoryStream msrr = new MemoryStream(Encoding.Unicode.GetBytes(d12)))
                        {
                            RootResponse rr = (RootResponse)re.ReadObject(msrr);

                            if (!string.IsNullOrEmpty(rr.DeliveryReplyMessage))
                            {
                                itemStatus.message = rr.DeliveryReplyMessage;
                                flag = true;
                            }
                        }
                    }
                    if (flag)
                    {
                        status.success = "true";
                        status.code = 200;
                        status.message = "SUCCESS";
                        if (req.Vendor == "AUGEO")
                        {
                            status.message = d12;
                        }
                        coupon.code = couponCode;
                        coupon.description = couponDesc;
                        customer.external_id = req.id;
                        itemStatus.code = 700;
                        itemStatus.success = "true";
                        var json2 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
                        logger.Info(json2);
                        return Request.CreateResponse(HttpStatusCode.OK, intouchCouponResponse);
                    }
                    else
                    {
                        var json2 = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
                        logger.Info(json2);
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
                    }

                    return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, intouchCouponResponse);
            }
            #endregion

            var json = JsonConvert.SerializeObject(intouchCouponResponse, Formatting.Indented);
            logger.Info(json);
            return Request.CreateResponse(HttpStatusCode.OK, intouchCouponResponse);
        }
    }
}

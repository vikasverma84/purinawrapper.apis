using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace purinawrapper.apis.Models
{
    public class IntouchCouponResponse
    {
        public Response response { get; set; }
    }
    public class Coupon
    {
        public string code { get; set; }
        public string description { get; set; }
        public string valid_till { get; set; }
        public SeriesInfo series_info { get; set; }
        public CustomerIntouch customer { get; set; }
        public ItemStatus item_status { get; set; }
    }
    public class CustomerIntouch
    {
        public string external_id { get; set; }
        public string user_id { get; set; }
    }

    public class ItemStatus
    {
        public string success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }

    public class Response
    {
        public Status status { get; set; }
        public Coupon coupon { get; set; }
    }

    public class SeriesInfo
    {
        public string id { get; set; }
        public string description { get; set; }
        public string discount_code { get; set; }
        public string valid_till { get; set; }
        public string discount_type { get; set; }
        public string discount_value { get; set; }
        public string detailed_info { get; set; }
    }

    public class Status
    {
        public string success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }

}
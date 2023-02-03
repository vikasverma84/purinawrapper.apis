using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace purinawrapper.apis.Models
{
    public class CustomerGet
    {
    }
    public class CouponCust
    {
        public string id { get; set; }
        public string series_id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string created_date { get; set; }
        public string valid_till { get; set; }
        public string redeemed { get; set; }
        public string same_user_multiple_redeem { get; set; }
    }

    public class CustomerCust
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public object mobile { get; set; }
        public object email { get; set; }
        public string external_id { get; set; }
        public int lifetime_points { get; set; }
        public int lifetime_purchases { get; set; }
        public int loyalty_points { get; set; }
        public string current_slab { get; set; }
        public string registered_on { get; set; }
        public string updated_on { get; set; }
        public string type { get; set; }
    }

    public class Customers
    {
        public List<CustomerCust> customer { get; set; }
    }


    public class ItemStatusCust
    {
        public string success { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public Warnings warnings { get; set; }
    }

    public class PointsSummaries
    {
        public List<object> points_summary { get; set; }
    }

    public class RegisteredStore
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class RegisteredTill
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class ResponseCust
    {
        public StatusCust status { get; set; }
        public Customers customers { get; set; }
    }

    public class RootCust
    {
        public ResponseCust response { get; set; }
    }

    public class StatusCust
    {
        public string success { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string total { get; set; }
        public string success_count { get; set; }
    }
    public class Warnings
    {
        public List<object> warning { get; set; }
    }


}
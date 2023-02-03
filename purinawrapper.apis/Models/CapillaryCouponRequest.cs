using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace purinawrapper.apis.Models
{
    public class CapillaryCouponRequest
    {
        public List<CouponCRM> coupon { get; set; }
    }
    public class CouponCRM
    {
        public string series_id { get; set; }
        public CustomerCRM customer { get; set; }
    }

    public class CustomerCRM
    {
        public string external_id { get; set; }
    }

    public class RootCRM
    {
        public CapillaryCouponRequest root { get; set; }
    }

}
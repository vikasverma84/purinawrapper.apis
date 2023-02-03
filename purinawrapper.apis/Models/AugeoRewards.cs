using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace purinawrapper.apis.Models
{
    public class AugeoRewards
    {
        public string clientUserId { get; set; }
        public string clientOrderId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public List<AddressAugeo> Address { get; set; }
        public List<OrderItem> orderItems { get; set; }
    }
    public class AddressAugeo
    {
        public string type { get; set; }
        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string stateISOCode { get; set; }
        public string countryISOCode { get; set; }
        public string zip { get; set; }
    }
    public class OrderItem
    {
        public string productId { get; set; }
        public string qty { get; set; }
    }

    public class RootAugeoResponse
    {
        public int AugeoOrderId { get; set; }
        public int AugeoUserId { get; set; }

    }

}
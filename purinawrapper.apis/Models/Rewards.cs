using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace purinawrapper.apis.Models
{
    public class Rewards
    {
    }// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Contract
    {
        public ShipToCustomer ShipToCustomer { get; set; }
        public string LanguageCode { get; set; }
    }

    public class Customer
    {
        public string CustomerId { get; set; }
        public Name Name { get; set; }
    }

    public class Name
    {
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class Email
    {
        public string Type { get; set; }
        public string Address { get; set; }
    }

    public class Order
    {
        public string OrderId { get; set; }
        public string OrderDate { get; set; }
        public string OrderTime { get; set; }
        public Overrides Overrides { get; set; }
        public List<OrderedItem> OrderedItem { get; set; }
    }

    public class OrderedItem
    {
        public string ProductId { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }

    public class Overrides
    {
        public bool Price { get; set; }
        public bool Tax { get; set; }
    }

    public class Root
    {
        public Contract Contract { get; set; }
        public Order Order { get; set; }
    }
    public class Address
    {
        public List<string> streetAddress { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class ShipToCustomer
    {
        public Customer Customer { get; set; }
        public List<Email> Email { get; set; }
        public Address address { get; set; }
    }

    public class Request
    {
        public string id { get; set; }
        public string intouch_series_id { get; set; }
        public string fname { get; set; }
        public string pid { get; set; }
        public string pname { get; set; }
        public string lname { get; set; }
        public string email_address { get; set; }
        public string streetAddress { get; set; }
        public string streetAddress2 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string orderType { get; set; }
        public string Vendor { get; set; }
        public string Address_Type { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Severity { get; set; }
        public string Source { get; set; }
    }

    public class RootResponse
    {
        public string DeliveryReplyMessage { get; set; }
        //public List<Error> Errors { get; set; }
    }
}
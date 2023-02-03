using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using NLog;
using purinawrapper.apis.Models;

namespace purinawrapper.apis.Models
{
    public class AltaIssueDigitalVoucher
    {
        private Logger logger;
        CallAltaRewardApi callAltaRewardApi=new CallAltaRewardApi();
        public AltaIssueDigitalVoucher()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public string IssueRewards(string api, string token, string itemCode, string ItemName, Request req)
        {
            string rs = string.Empty;
            try
            {
                Root root = new Root();
                Contract contract = new Contract();
                Email email1 = new Email();
                List<Email> emails = new List<Email>();
                Order order = new Order();
                ShipToCustomer shipToCustomer = new ShipToCustomer();
                Customer customer = new Customer();
                Name name = new Name();
                Overrides overrides = new Overrides();
                OrderedItem orderedItem = new OrderedItem();
                List<OrderedItem> orderedItems = new List<OrderedItem>();
                
                if (req.orderType == "Physical")
                {
                    Address address = new Address();
                    address.Country = string.IsNullOrEmpty(req.Country)?"null": req.Country;
                    address.PostalCode = string.IsNullOrEmpty(req.PostalCode) ? "null" : req.PostalCode;
                    address.StateProvince = string.IsNullOrEmpty(req.StateProvince)?"null":req.StateProvince;
                    address.City = string.IsNullOrEmpty(req.City) ? "null" : req.City;
                    List<string> adds = new List<string>();
                    adds.Add(string.IsNullOrEmpty(req.streetAddress)?"null":req.streetAddress);
                    adds.Add(string.IsNullOrEmpty(req.streetAddress2) ? "null" : req.streetAddress2);
                    address.streetAddress = adds;
                    shipToCustomer.address = address;
                }
                orderedItem.ProductId = itemCode;
                orderedItem.Name = ItemName;
                orderedItem.Quantity = 1;
                orderedItem.Sequence = 1;
                orderedItem.Price = 0;
                name.First = req.fname;
                name.Last = req.lname;
                Random rnd = new Random();
                email1.Type = "Primary";
                email1.Address = string.IsNullOrEmpty(req.email_address) ? "null" : req.email_address;
                int myRandomNo = rnd.Next(10000000, 99999999);
                order.OrderId = myRandomNo.ToString();
                order.OrderDate = DateTime.Now.ToString("yyyy-MM-dd");
                order.OrderTime = DateTime.Now.ToString("HH:mm:ss-4:00");
                order.Overrides = overrides;
                overrides.Price = false;
                overrides.Tax = false;
                emails.Add(email1);
                customer.CustomerId = req.id;
                customer.Name = name;
                shipToCustomer.Customer = customer;
                shipToCustomer.Email = emails;
                contract.LanguageCode = "eng";
                contract.ShipToCustomer = shipToCustomer;
                orderedItems.Add(orderedItem);
                order.OrderedItem = orderedItems;
                root.Order = order;
                root.Contract = contract;

                var json = JsonConvert.SerializeObject(root, Formatting.Indented);
                rs = callAltaRewardApi.AltaRewardAPI(api, json, token);

            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
            return rs;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace purinawrapper.apis.Models
{
    public class token
    {
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public int ext_expires_in { get; set; }
        public string access_token { get; set; }
    }
}
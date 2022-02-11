using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models
{
    public class ErrorMessageModel
    {
        public string type { get; set; }

        public string message { get; set; }

        public int statusCode { get; set; }

        public string clientRequest { get; set; }

        public string action { get; set; }

        public dynamic errors { get; set; }
    }
}

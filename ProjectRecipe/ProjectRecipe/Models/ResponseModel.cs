using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Models
{
    public class ResponseModel
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public IEnumerable<string> errors { get; set; }
        public DateTime? expireDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Content
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int totalprice { get; set; }
        public bool depositpaid { get; set; }
        public Dates bookingdates { get; set; }
        public string additionalneeds { get; set; }
    }
}

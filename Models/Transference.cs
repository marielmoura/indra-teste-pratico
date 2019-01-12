using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntraTransactionAPI.Models
{
    public class Transference
    {        
        public float value { get; set; }
        public string description { get; set; }
        public string originAccountId { get; set; }
        public string destinyAccountId { get; set; }
    }
}

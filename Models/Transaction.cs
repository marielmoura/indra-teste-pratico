using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntraTransactionAPI.Models
{
    public class Transaction
    {        
        public float value { get; set; }
        public string description { get; set; }
        public string accountId { get; set; }
        public Account account { get; set; }
    }
}

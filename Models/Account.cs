using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntraTransactionAPI.Models
{
    public class Account
    {        
        public int agency { get; set; }
        public int currentAccount { get; set; }
        public int digit { get; set; }
        public string bankName { get; set; }
        public string bankCode { get; set; }
    }
}

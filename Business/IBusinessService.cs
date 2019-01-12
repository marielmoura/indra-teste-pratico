using IntraTransactionAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntraTransactionAPI.Repository
{
    public interface IBusinessService
    {
        string postTransaction(Transaction transaction);
        string postAccount(Account account);
        string postTransference(Transference transference);
        JObject getTransactions();
        JObject getAccounts();
        Account getAccountById(string id);
    }
}

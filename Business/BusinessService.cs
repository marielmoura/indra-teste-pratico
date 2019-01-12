using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using IntraTransactionAPI.Models;
using IntraTransactionAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntraTransactionAPI.Business
{
    public class BusinessService : IBusinessService
    {        
        IFirebaseConfig config;

        public BusinessService(IConfiguration configuration)
        {
            config = new FirebaseConfig
            {
                AuthSecret = configuration["FirebaseAuthSecret"],
                BasePath = configuration["FirebaseBasePath"]
            };
        }

        public string postTransference(Transference transference)
        {

            try
            {
                if (transference.destinyAccountId.Equals(transference.originAccountId))
                    return "Destiny and origin accounts can't be equals. Transference failed";

                if (transference.value.Equals(0))
                    return "Transference value can't be zero. Transference failed";

                if (transference.destinyAccountId == null)
                    return "Destiny account id can`t be null. Transference failed";

                if (transference.originAccountId == null)
                    return "Origin account id can`t be null. Transference failed";

                //ORIGIN TRANSACTION

                Transaction originAccountTransaction = new Transaction();

                originAccountTransaction.value = transference.value * -1;
                originAccountTransaction.description = transference.description;
                originAccountTransaction.accountId = transference.originAccountId;

                postTransaction(originAccountTransaction);

                //DESTINY TRANSACTION

                Transaction destinyAccountTransaction = new Transaction();

                destinyAccountTransaction.value = transference.value;
                destinyAccountTransaction.description = transference.description;
                destinyAccountTransaction.accountId = transference.destinyAccountId;

                postTransaction(destinyAccountTransaction);

                return "Transference successfully recorded.";

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string postTransaction(Transaction transaction)
        {
            try
            {
                if (transaction.accountId == null)
                    return "Account id can`t be null. Transference failed";

                IFirebaseClient client = new FirebaseClient(config);
                transaction.account = getAccountById(transaction.accountId);

                if (transaction.account == null)
                    return "Account not found. Transference failed";

                PushResponse response = client.Push("transactions", transaction);
                Transaction result = response.ResultAs<Transaction>();
                return "Transaction successfully recorded.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string postAccount(Account account)
        {
            try
            {
                IFirebaseClient client = new FirebaseClient(config);
                PushResponse response = client.Push("accounts", account);
                Account result = response.ResultAs<Account>(); //The response will contain the data written
                return "Account successfully recorded.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject getTransactions()
        {
            try
            {
                IFirebaseClient client = new FirebaseClient(config);
                FirebaseResponse response = client.Get("transactions");
                JObject transactions = JsonConvert.DeserializeObject<JObject>(response.Body);
                return transactions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject getAccounts()
        {
            try
            {
                IFirebaseClient client = new FirebaseClient(config);
                FirebaseResponse response = client.Get("accounts");
                JObject accounts = JsonConvert.DeserializeObject<JObject>(response.Body);
                return accounts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Account getAccountById(string id)
        {
            try
            {
                IFirebaseClient client = new FirebaseClient(config);
                FirebaseResponse accountResponse = client.Get("accounts/" + id);
                Account accountResult = accountResponse.ResultAs<Account>();
                return accountResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

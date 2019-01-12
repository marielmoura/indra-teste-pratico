using IntraTransactionAPI.Business;
using IntraTransactionAPI.Models;
using IntraTransactionAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace IntraTransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IBusinessService businessService;        

        public TransactionsController(IConfiguration _configuration)
        {
            businessService = new BusinessService(_configuration);
        }

        // GET api/transactions
        [HttpGet]
        public ActionResult<JObject> Get()
        {
            try
            {
                JObject transactions = businessService.getTransactions();

                if (transactions == null)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }

                return transactions;
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                throw ex;
            }
        }

        // POST api/transactions
        [HttpPost]
        public ActionResult<string> Post([FromBody] Transaction transaction)
        {
            try
            {
                return businessService.postTransaction(transaction);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                throw ex;
            }
        }

    }
}

using System;
using System.Net;
using IntraTransactionAPI.Business;
using IntraTransactionAPI.Models;
using IntraTransactionAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace IntraTransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IBusinessService businessService;

        public AccountsController(IConfiguration _configuration)
        {
            businessService = new BusinessService(_configuration);
        }

        // GET api/accounts
        [HttpGet]
        public ActionResult<JObject> Get()
        {
            try
            {
                JObject accounts = businessService.getAccounts();

                if (accounts == null)
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }

                return accounts;
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                throw ex;
            }
           
        }

        // POST api/accounts
        [HttpPost]
        public ActionResult<string> Post([FromBody] Account account)
        {
            try
            {
                return businessService.postAccount(account);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                throw ex;
            }           
        }
    }
}

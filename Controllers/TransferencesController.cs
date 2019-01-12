using System;
using System.Net;
using IntraTransactionAPI.Business;
using IntraTransactionAPI.Models;
using IntraTransactionAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace IntraTransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferencesController : ControllerBase
    {
        private readonly IBusinessService businessService;

        public TransferencesController(IConfiguration _configuration)
        {
            businessService = new BusinessService(_configuration);
        }

        // POST api/transferences     
        [HttpPost]
        public ActionResult<string> Post([FromBody] Transference transference)
        {
            try
            {
                return businessService.postTransference(transference);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                throw ex;
            }
        }
    }
}

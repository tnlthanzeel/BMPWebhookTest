using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace testcors.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpPost, Route("success")]
        public ActionResult Success([FromBody] TransactionSettledWebhookModel requestBody)
        {

            var doesxrequestidexist = HttpContext.Request.Headers.TryGetValue("X-Request-Id", out var XRequestId);

            var nonce = new NonceObject()
            {
                Nonce = requestBody.Nonce
            };

            return Ok(nonce);
        }

        [HttpPost, Route("rejected")]
        public ActionResult Rejected([FromBody] TransactionSettledWebhookModel requestBody)
        {

            var doesxrequestidexist = HttpContext.Request.Headers.TryGetValue("X-Request-Id", out var XRequestId);

            var nonce = new NonceObject()
            {
                Nonce = requestBody.Nonce
            };

            return Ok(nonce);
        }
    }

    public class NonceObject
    {
        [JsonProperty("Nonce")]
        public string Nonce { get; set; }
    }

    public class TransactionSettledWebhookModel
    {
        public long AgentUserId { get; set; }
        public Payload Payload { get; set; }
        public string Nonce { get; set; }
        [StringLength(200)]
        public string XRequestId { get; set; }
    }

    public class Payload
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public string Scheme { get; set; }
        public string EndToEndTransactionId { get; set; }
        public double Amount { get; set; }
        public string TimestampModified { get; set; }
        public string CurrencyCode { get; set; }
        public string DebitCreditCode { get; set; }
        public string Reference { get; set; }
        public bool IsReturn { get; set; }
        public Account Account { get; set; }
        public Account CounterpartAccount { get; set; }
        public string ActualEndToEndTransactionId { get; set; }
    }

    public class Account
    {
        public string IBAN { get; set; }
        public string BBAN { get; set; }
        public string AccountName { get; set; }
        public string InstitutionName { get; set; }
    }

}

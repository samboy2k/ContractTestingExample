using Microsoft.AspNetCore.Mvc;
using ProviderWebApi;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditDecisionController : ControllerBase
    {
        private static readonly string[] Decisions = new[]
        {
        "Approve", "Refer", "Decline"
    };

        private readonly ILogger<CreditDecisionController> _logger;

        public CreditDecisionController(ILogger<CreditDecisionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetCreditDecision")]
        public IEnumerable<CreditDecision> Get()
        {
            return Enumerable.Range(1, 1).Select(index => new CreditDecision
            {
                Date = DateTime.Now.AddDays(index),
                ApplicationScore = Random.Shared.Next(-20, 350),
                Decision = Decisions[Random.Shared.Next(Decisions.Length)]
            })
            .ToArray();
        }

     
    }
}
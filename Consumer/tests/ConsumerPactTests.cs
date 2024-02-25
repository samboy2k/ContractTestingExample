using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using Consumer;
using System.Collections.Generic;
using PactNet;
using PactNet.Matchers;
using PactNet.Infrastructure.Outputters;
using PactNet.Output.Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Text;

namespace tests
{
    public class ConsumerPactTests
    {
        private IPactBuilderV3 pact;

        private readonly List<object> decisionResponse;

        public ConsumerPactTests(ITestOutputHelper output)
        {

            decisionResponse = new List<object>()
            {
                new { date = "2024-02-25T08:44:54.7438282+11:00", applicationScore = 320, decision = "Approve" }
            };

            var Config = new PactConfig
            {
                PactDir = Path.Join("..", "..", "..", "..", "..", "pacts"),
                Outputters = new[] { new XunitOutput(output) },
                LogLevel = PactLogLevel.Debug
            };

            pact = Pact.V3("Consumer", "Provider", Config).WithHttpInteractions();
        }

        [Fact]
        public async Task ItHandlesCreditDecision()
        {
            // Arrange
            pact.UponReceiving("A GET Request for CreditDecision")
             //       .Given("There is data")
                    .WithRequest(HttpMethod.Get, "/CreditDecision")
                .WillRespond()
                    .WithStatus(HttpStatusCode.OK)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithJsonBody((Match.Type(decisionResponse)));
                  
            // .WithJsonBody(decisions);

            // Act
            await pact.VerifyAsync(async ctx =>
            {
                var result = await ConsumerApiClient.GetCreditDecisionUsingProviderApi(ctx.MockServerUri.ToString());      
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
             
            });
        }

     



    }
}

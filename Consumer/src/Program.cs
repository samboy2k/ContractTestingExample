using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
     
            string baseUri = "https://localhost:44377";

        

            Console.WriteLine("Getting Credit Decision...");
            var result = ConsumerApiClient.GetCreditDecisionUsingProviderApi( baseUri).GetAwaiter().GetResult();
            var resultContentText = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine(resultContentText);
            Console.WriteLine("Get Credit Decision Completed");
        }

      
    }
}

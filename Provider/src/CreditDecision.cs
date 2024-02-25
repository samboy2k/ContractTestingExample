namespace ProviderWebApi
{
    public class CreditDecision
    {
        public DateTime Date { get; set; }

        public int ApplicationScore { get; set; }


        public string? Decision { get; set; }
    }
}
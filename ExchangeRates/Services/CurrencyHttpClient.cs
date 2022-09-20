using ExchangeRates.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExchangeRates.Services
{
    public class CurrencyHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions _options;

        //injecting httpclient provided by httpclient factory 
        //injecting IConfiguration to get the appropriate address for the request
        public CurrencyHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
               JsonNumberHandling.WriteAsString
            };
        }

        //implement platform to command synchronous call

        public async Task<Rate> getLatest()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.apilayer.com/fixer/latest?symbols=NOK&base=GBP"),
                Headers =
                 {
                     {"apikey", "A07tgsV3H2vGivSzkUzxZT8kQ8Kum8RJ"},
                 },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                //if(response.IsSuccessStatusCode?)


                FixerResponse body = await JsonSerializer.DeserializeAsync<FixerResponse>(await response.Content.ReadAsStreamAsync(), _options) ?? throw new ArgumentException();
                Rate responseItem = new Rate()
                {
                    Value = body.rates.NOK,
                    Date = DateTime.UtcNow,
                };
                Console.WriteLine(body);
                return responseItem;
            }
        }

    }


    //{
    //  "base": "GBP",
    //  "date": "2022-09-19",
    //  "rates": {
    //    "NOK": 11.703089
    //  },
    //  "success": true,
    //  "timestamp": 1663607316
    //} 
    public class Rates
    {
        public double NOK { get; set; }
    }

    public class FixerResponse
    {
        public string @base { get; set; }
        public string date { get; set; }
        public Rates rates { get; set; }
        public bool success { get; set; }
        public int timestamp { get; set; }
    }


}

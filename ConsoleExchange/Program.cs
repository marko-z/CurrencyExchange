// See https://aka.ms/new-console-template for more information
using System.Dynamic;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

//{
//  "base": "GBP",
//  "date": "2022-09-19",
//  "rates": {
//    "NOK": 11.703089
//  },
//  "success": true,
//  "timestamp": 1663607316
//} 



public class FixerResponse
{
    public string @base { get; set; }
    public string date { get; set; }

    public Dictionary<string, Double> rates { get; set; }
    public bool success { get; set; }
    public int timestamp { get; set; }

}
internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        using var client = new HttpClient();
        while (true)
        {
            Console.WriteLine("From: ");
            String[] input = Console.ReadLine().Split();
            Double inputAmount = Convert.ToDouble(input[0]);
            String inputCurrency = input[1];
            Console.WriteLine("To: ");
            String outputCurrency = Console.ReadLine();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.apilayer.com/fixer/latest?symbols={inputCurrency}&base={outputCurrency}"),
                Headers =
                 {
                     {"apikey", "A07tgsV3H2vGivSzkUzxZT8kQ8Kum8RJ"},
                 },
            };
            using var response = await client.SendAsync(request);
            
            if(response.IsSuccessStatusCode)
            {
                FixerResponse result = await JsonSerializer.DeserializeAsync<FixerResponse>(await response.Content.ReadAsStreamAsync()) ?? throw new ArgumentException();
                var conversionRates = result.rates.Values.ToArray();
                Double conversionRate = (Double)conversionRates[0];
                Console.WriteLine($"Conversion rate: {conversionRate}");
                Console.WriteLine($"That will be {inputAmount / conversionRate} in {outputCurrency}");

            } else
            {
                Console.WriteLine("Something went wrong");
            }
        }
    }
}
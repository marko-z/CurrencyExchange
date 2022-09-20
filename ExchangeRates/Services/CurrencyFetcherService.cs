using ExchangeRates.Data;
using ExchangeRates.Models;
using System.Diagnostics;

namespace ExchangeRates.Services
{

    public sealed class CurrencyFetcherService : BackgroundService
    {
        private readonly CurrencyHttpClient _currencyClient;
        private readonly IServiceProvider _serviceProvider;

        private static int SecondsUntilMidnight()
        {
            return (int)(DateTime.Today.AddDays(1.0) - DateTime.Now).TotalSeconds;
        }

        public CurrencyFetcherService(IServiceProvider serviceProvider, CurrencyHttpClient currencyClient)
        {
            _currencyClient = currencyClient;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var countdown = SecondsUntilMidnight();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (countdown-- <= 0)
                {
                    try
                    {
                        await OnTimerFiredAsync(stoppingToken);
                    }
                    catch (System.Exception)
                    {
                        // todo: log exceeption
                        throw;
                    }
                    finally
                    {
                        countdown = SecondsUntilMidnight();
                    }
                }
                Debug.WriteLine($"Time is {DateTime.Now}, {countdown} seconds until midnight / next request");
                await Task.Delay(60000, stoppingToken);
            }
        }

        private async Task OnTimerFiredAsync(CancellationToken stoppingToken)
        {
            Debug.WriteLine("Performing request to fixer.io");
            Rate response = await _currencyClient.getLatest();
            Debug.WriteLine("Writing to dabase....");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Rates.Add(response);
                db.SaveChanges();
            }
            Debug.WriteLine("Done.");
        }
    }
}
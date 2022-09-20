using ExchangeRates.Data;
using ExchangeRates.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace ExchangeRates.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ExchangeRatesController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet("latest")]
        public ActionResult<Rate> Latest(int id)
        {
            return _db.Rates.First();
        }
        [HttpGet("range")]
        public ActionResult<IEnumerable<Rate>> Range([FromQuery] string from, [FromQuery] string to)
        {
            //19.09.2022 14:18:52
          
            DateTime fromDate;
            DateTime toDate;
            if (DateTime.TryParseExact(from, "dd-MM-yyyy", null, DateTimeStyles.None, out fromDate) &&
                 DateTime.TryParseExact(to, "dd-MM-yyyy", null, DateTimeStyles.None, out toDate))
            {
                return _db.Rates.Where(item => (item.Date >= fromDate && item.Date <= toDate)).ToList();

            } else
            {
                throw new Exception("Invalid date");
            }

        }
        //[HttpGet]
        //public ActionResult<Item> GetLatest() {
        //    return _db.Items.FirstOrDefault();
        //}
    }
}
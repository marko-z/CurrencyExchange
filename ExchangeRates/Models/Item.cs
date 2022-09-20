using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExchangeRates.Models
{
    public class Rate
    {
        public int Id { get; set; }
        // {
        //   "base": "GBP",
        //   "date": "2022-09-19",
        //   "rates": {
        //     "NOK": 11.702652
        //   },
        //   "success": true,
        //   timestamp": 1663606475
        // }

        [Required]
        public DateTime Date { get; set; }

        //[Required]
        //public string Text { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(8,6)")]
        public double Value { get; set; }
    }
}
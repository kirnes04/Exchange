using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Models;

[Table("rates")]
public class Rates
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("first_currency")]
    public string FirstCurrency { get; set; }
    
    [Column("second_currency")]
    public string SecondCurrency { get; set; }
    
    [Column("currency_rate")]
    public double CurrencyRate { get; set; }
}
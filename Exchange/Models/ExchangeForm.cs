using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Exchange.Models;

[Table("exchange_form")]
public class ExchangeForm
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("initial_currency")]
    public string InitialCurrency { get; set; }
    
    [Column("required_currency")]
    public string RequiredCurrency { get; set; }
    
    [Column("amount")]
    public double Amount { get; set; }
}
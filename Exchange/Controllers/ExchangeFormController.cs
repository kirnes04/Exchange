using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exchange.Models;
using Exchange.context;

namespace Exchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeFormController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExchangeFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ExchangeForm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExchangeForm>>> GetExchangeForm()
        {
            if (_context.ExchangeForm == null)
            {
                return NotFound();
            }

            return await _context.ExchangeForm.ToListAsync();
        }

        // GET: api/ExchangeForm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExchangeForm>> GetExchangeForm(int id)
        {
            if (_context.ExchangeForm == null)
            {
                return NotFound();
            }

            var exchangeForm = await _context.ExchangeForm.FindAsync(id);

            if (exchangeForm == null)
            {
                return NotFound();
            }

            return exchangeForm;
        }

        // POST: api/ExchangeForm
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExchangeForm>> PostExchangeForm(ExchangeForm exchangeForm)
        {
            if (_context.ExchangeForm == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ExchangeForm'  is null.");
            }

            if (exchangeForm.Amount <= 0)
            {
                return BadRequest("You can't exchange less than zero amount");
            }

            double amount = -1;
            foreach (var rate in _context.Rates)
            {
                if (rate.FirstCurrency == exchangeForm.InitialCurrency.ToLower() &&
                    rate.SecondCurrency == exchangeForm.RequiredCurrency.ToLower())
                {
                    amount = exchangeForm.Amount * rate.CurrencyRate;
                    break;
                } 
                else if (rate.FirstCurrency == exchangeForm.RequiredCurrency.ToLower() &&
                          rate.SecondCurrency == exchangeForm.InitialCurrency.ToLower())
                {
                    amount = exchangeForm.Amount / rate.CurrencyRate;
                    break;
                }
            }

            if (amount < 0)
            {
                return BadRequest("Currently we can't exchange these currencies");
            }
            _context.ExchangeForm.Add(exchangeForm);
            await _context.SaveChangesAsync();
            
            return Ok($"Exchange successful: you got {Math.Round(amount, 2)} {exchangeForm.RequiredCurrency}");
        }

        private bool ExchangeFormExists(int id)
        {
            return (_context.ExchangeForm?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
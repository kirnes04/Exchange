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
    public class RatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Rates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rates>>> GetRates()
        {
            if (_context.Rates == null)
            {
                return NotFound();
            }

            return await _context.Rates.ToListAsync();
        }
        
        // POST: api/Rates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rates>> PostRates(Rates rates)
        {
            if (_context.Rates == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rates'  is null.");
            }

            if (rates.FirstCurrency.All(char.IsDigit) || rates.SecondCurrency.All(char.IsDigit))
            {
                return BadRequest("Currency name can't contain digits");
            }
            if (rates.CurrencyRate <= 0)
            {
                return BadRequest("Incorrect currency rate");
            }

            rates.FirstCurrency = rates.FirstCurrency.ToLower();
            rates.SecondCurrency = rates.SecondCurrency.ToLower();
            _context.Rates.Add(rates);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRates", new { id = rates.Id }, rates);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhoWasHere.Server.Data.Customer;
using WhoWasHere.Shared.Customer;

namespace WhoWasHere.Server.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomerController(CustomerContext context)
        {
            _context = context;
        }

        #region GET...
        // GET: api/Calendar
        [HttpGet]
        public async Task<ActionResult> GetCustomers()
        {
            var test = new List<CustomerModel>();
            try
            {
                test = _context.CustomerModel.ToList();
            }
            catch (Exception e3x)
            {
                Console.WriteLine("Error: "+e3x);                
            }
            return new JsonResult(test);
            //return await _context.Day.ToListAsync();

        }

        // GET: api/Calendar/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerModel>> GetCustomerById(int id)
        {
            var customer = await _context.CustomerModel.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }            
            return new JsonResult(customer);
        }


        //// GET: api/Calendar/startDate/endDate
        //[HttpGet("{startDate:DateTime}/{endDate:DateTime}", Name = "GetDayBetweenDate")]
        //public async Task<JsonResult> GetDayBetweenDateAsync(DateTime startDate, DateTime endDate)
        //{

        //    var dayList = _context.Day.Where(d => d.Date >= startDate && d.Date <= endDate).ToList();

        //    if (dayList.Count() >= 0)
        //    {
        //        foreach (var day in dayList)
        //        {
        //            day.DayName = day.Date.ToString("dddd");
        //        }
        //    }
        //    return new JsonResult(dayList);
        //}

        #endregion

        #region PUT...

        // PUT: api/Calendar/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCustomer(int id, [FromBody] CustomerModel customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                    throw;

                }
            }

            return NoContent();
        }

        #endregion

        #region POST...


        // POST: api/Calendar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> PostCustomer(CustomerModel customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            _context.CustomerModel.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customer);
        }

        #endregion

        #region DELETE...




        // DELETE: api/Calendar/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGetCustomer(int id)
        {
            var day = await _context.CustomerModel.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }

            _context.CustomerModel.Remove(day);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DayExists(int id)
        {
            return _context.CustomerModel.Any(e => e.Id == id);
        }
        #endregion
    }
}

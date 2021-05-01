using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WhoWasHere.Server.Data.Customer;
using WhoWasHere.Shared.Customer;
using WhoWasHere.Shared.Round;

namespace WhoWasHere.Server.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController  : ControllerBase
    {
        private readonly CustomerContext _context;



        public AppointmentController (CustomerContext context)
        {
            _context = context;
        }

        #region GET...
        // GET: api/Calendar
        [HttpGet]
        public async Task<ActionResult> GetAppointments()
        {
            var test = new List<Appointment>();
            try
            {
                test = _context.Appointment.ToList();
            }
            catch (Exception e3x)
            {
                Console.WriteLine("Error: "+e3x);                
            }
            return new JsonResult(test);            

        }

        // GET: api/Calendar/id
        [HttpGet("{day:DateTime}")]
        public async Task<ActionResult> GetAppointmentsByDay(DateTime day)
        {

            var appointments =  _context.Appointment.Where(a => a.StartAppointment.Day == day.Day 
                && a.StartAppointment.Month == day.Month && a.StartAppointment.Year == day.Year
            ).OrderBy(a=>a.Id).ToList();

            if (appointments == null)
            {
                return NotFound();
            }            
            return new JsonResult(appointments);
        }

    


        #endregion

        #region PUT...

        // PUT: api/Calendar/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAppointment(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                return BadRequest();
            }
            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAppointments), new { id = appointment.Id }, appointment);
        }

        #endregion

        #region DELETE...




        // DELETE: api/Calendar/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGetAppointment(int id)
        {
            
            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            if (appointment.StartAppointment< DateTime.Now)//non posso eliminare un appuntamento gia passato da piu di un giorno
            {
                return BadRequest();
            }

            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.Id == id);
        }
        #endregion
    }
}

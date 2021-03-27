using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhoWasHere.Server.Data.Calendar;
using WhoWasHere.Shared.Calendar;

namespace WhoWasHere.Server.Controllers.Calendar
{
    [Route("api/[controller]")]
    [ApiController]    
    public class CalendarController : ControllerBase
    {
        private readonly CalendarContext _context;

        public CalendarController(CalendarContext context)
        {
            _context = context;
        }

        #region GET...
        // GET: api/Calendar
        [HttpGet]
        public async Task<JsonResult> GetDay()
        {
            var test = await _context.Day.ToListAsync();
            return new JsonResult(test);
             //return await _context.Day.ToListAsync();

        }

        // GET: api/Calendar/id
        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<IDay>> GetDayById(int id)
        {
            var day = await _context.Day.FindAsync(id);

            if (day == null)
            {
                return NotFound();
            }
            day.DayName = day.Date.ToString("dddd");
            return new JsonResult(day);
        }


        // GET: api/Calendar/startDate/endDate
        [HttpGet("{startDate:DateTime}/{endDate:DateTime}", Name = "GetDayBetweenDate")]
        public async Task<JsonResult> GetDayBetweenDateAsync(DateTime startDate, DateTime endDate)
        {

            var dayList = _context.Day.Where(d => d.Date >= startDate && d.Date <= endDate).ToList();

            if (dayList.Count() >= 0)
            {
                foreach (var day in dayList)
                {
                    day.DayName = day.Date.ToString("dddd");
                }
            }
            return new JsonResult(dayList);
        }

        #endregion

        #region PUT...

        // PUT: api/Calendar/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutDay(int id,[FromBody] Day day)
        {
            if (id != day.Id)
            {
                return BadRequest();
            }

            _context.Entry(day).State = EntityState.Modified;

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
        public async Task<ActionResult<IDay>> PostDay(Day day)
        {
            if (day == null)
            {
                return BadRequest();
            }
            _context.Day.Add(day);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDay), new { id = day.Id }, day);
        }

        #endregion




        //TODO Non posso elimniare il giorno quando farò un associazione con l'id, posso eliminarlo solo se non ho appuntamenti
        // DELETE: api/Calendar/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDay(int id)
        {
            var day = await _context.Day.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }

            _context.Day.Remove(day);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DayExists(int id)
        {
            return _context.Day.Any(e => e.Id == id);
        }
    }
}

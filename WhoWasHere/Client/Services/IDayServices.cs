using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoWasHere.Shared.Calendar;

namespace WhoWasHere.Client.Services
{
    public interface IDayServices
    {
        Task<IEnumerable<IDay>> GetDaysAsync();        
        Task<IDay> GetDayFromIdAsync();                
        Task<IDay> PostDayAsync(IDay day);
        Task<IEnumerable<Day>> GetDaysOnDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IDay> PutDayAsync(int id,Day day);

        
        Task<IDay> CreateOrUpdate(Day day);
        
    }
}

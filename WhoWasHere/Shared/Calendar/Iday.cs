using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWasHere.Shared.Calendar
{
    public interface IDay
    {
        int Id { get; set; }
        string Note { get; set; }        
        int DayNumber { get; set; }
        string DayName { get; set; }
        

    }
}

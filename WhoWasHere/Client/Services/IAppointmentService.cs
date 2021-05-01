using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoWasHere.Shared.Customer;
using WhoWasHere.Shared.Round;

namespace WhoWasHere.Client.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
        Task<Appointment> GetAppointmentFromIdAsync(int id);
        Task<IEnumerable<Appointment>> GetAppointmentsOfDayAsync(DateTime day);
        Task<Appointment> PostAppointmentAsync(Appointment appointment);        
        Task<Appointment> PutAppointmentAsync(int id, Appointment appointment);
        Task<Appointment> DeleteAppointmentAsync(Appointment appointment);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using WhoWasHere.Shared.Round;

namespace WhoWasHere.Client.Services
{
    public class AppointmentService : IAppointmentService
    {

        private readonly HttpClient httpClient;
        private readonly string baseURI = "api/Appointment/";

        public AppointmentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Appointment> DeleteAppointmentAsync(Appointment Appointment)
        {
            var response = await httpClient.DeleteAsync($"{baseURI}{Appointment.Id}");
            if (response.IsSuccessStatusCode)
            {
                return new Appointment()
                {
                    Id=0
                };
            }
            else
            {
                return Appointment;

            }
        }

        public async Task<Appointment> GetAppointmentFromIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<Appointment>($"{baseURI}{id}");
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Appointment>>($"{baseURI}");
        }


        public async Task<IEnumerable<Appointment>> GetAppointmentsOfDayAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Appointment>>($"{baseURI}{id}");
        }





        public async Task<Appointment> PostAppointmentAsync(Appointment Appointment)
        {
            var response = await httpClient.PostAsJsonAsync<Appointment>($"{baseURI}", Appointment);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Appointment>(jsonString);
            }
            else
            {
                return Appointment;
            }
        }

        public async Task<Appointment> PutAppointmentAsync(int id, Appointment Appointment)
        {
            var response = await httpClient.PutAsJsonAsync<Appointment>($"{baseURI}{id}", Appointment);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Appointment>(jsonString);
            }
            else
            {
                return Appointment;
            }
        }
    }
}

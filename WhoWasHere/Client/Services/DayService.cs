using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WhoWasHere.Shared.Calendar;

namespace WhoWasHere.Client.Services
{
    /// <summary>
    /// Manage the Day api call
    /// </summary>
    public class DayService : IDayServices
    {
        private readonly HttpClient httpClient;

        public DayService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<IEnumerable<IDay>> GetDaysAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Day>>("api/Calendar");
        }


        public async Task<IDay> PostDayAsync(IDay day)
        {
            var response = await httpClient.PostAsJsonAsync<IDay>("api/Calendar", day);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Day>(jsonString);
            }
            else
            {
                return day;
            }
        }

        public async Task<IDay> GetDayFromIdAsync()
        {
            return await httpClient.GetFromJsonAsync<IDay>("api/Calendar/Id");
        }

        public async Task<IEnumerable<Day>> GetDaysOnDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Day>>("api/Calendar/" + startDate.ToString("yyyy-MM-dd") + "/" + endDate.ToString("yyyy-MM-dd"));
        }

        public async Task<IDay> PutDayAsync(int id, Day day)
        {
            var response = await httpClient.PutAsJsonAsync<IDay>($"api/Calendar/{id}", day);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Day>(jsonString);
            }
            else
            {
                return day;
            }
        }
        public async Task<IDay> DeleteDayAsync(Day day)
        {
            var response = await httpClient.DeleteAsync($"api/Calendar/{day.Id}");

            if (response.IsSuccessStatusCode)
            {
                return new Day()
                {
                    Id = 0,
                    Date = day.Date,
                    DayName = day.Date.ToString("dddd"),
                    Note = ""
                };
            }
            else
            {
                return day;

            }
        }

        public async Task<IDay> CreateOrUpdate(Day day)
        {
            // TODO: Riportare questa logica nel chiamante, in moda da chiamre le singole chiamate e distinguere il resul e action 
            if (day.Id == 0 && String.IsNullOrEmpty(day.Note.Trim()))
                return day;
            IDay result = day;


            if (day.Id == 0 && !String.IsNullOrEmpty(day.Note.Trim()))
            {
                result = await PostDayAsync(day);
            }
            else if (String.IsNullOrEmpty(day.Note.Trim()) && day.Id > 0)
            {
                result = await DeleteDayAsync(day);
            }
            else
            {
                result = await PutDayAsync(day.Id, day);
            }


            return result;
        }


    }
}

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
            return  await httpClient.GetFromJsonAsync<IEnumerable<Day>>("api/Calendar/" + startDate.ToString("yyyy-MM-dd")+"/"+ endDate.ToString("yyyy-MM-dd"));
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

        public async Task<IDay> CreateOrUpdate(Day day)
        {

            IDay result = day;
            if (day.Id == 0)
            {
                 result = await PostDayAsync(day);
            }
            else
            {
                result = await PutDayAsync(day.Id, day);                
            }


            return result;
        }


    }
}

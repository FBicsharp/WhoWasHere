using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhoWasHere.Client.Services;
using WhoWasHere.Shared;
using WhoWasHere.Shared.Calendar;
using Microsoft.Extensions.DependencyInjection;

namespace WhoWasHere.Client.Pages
{
    public partial class Calendar  : ComponentBase
    {
       [Inject] 
        public IDayServices DayServices { get; set; }
             
        
        public List<Week> weeks { get; set; }        


        private DateTime startDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);//primo di questo mese
        private DateTime endDate { get; set; } = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(1).AddDays(-1);//ultimo del mese

        protected DateTime selectedMonth { get; set; } = DateTime.Now;

        public List<string> daysName { get; set; }
        public List<string> MonthsName { get; set; } = new List<string>();

        public IDay DaySelected { get; set; }




        protected override async Task OnInitializedAsync()
        {
            MonthsName = CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames.ToList();
            MonthsName.Remove("");
            try
            {
                GenerateCalendarHead();
                var a = await GenerateCalendarBody();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error" + ex);                
            }           
        }



        /// <summary>
        /// Get all days datails from tabale body
        /// </summary>
        private async Task<bool> GenerateCalendarBody()
        {
            //leggo solo le date che mi intaressano dall api
            weeks = new List<Week>();            
            Week week = new Week();
            List<IDay> days = new List<IDay>();
            Day day;
            Day existingDay;
            var daysRegistred = await DayServices.GetDaysOnDateRangeAsync(startDate, endDate);


            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                day = new Day(){              
                    Id = 0,
                    Date= dt,
                    DayName = dt.ToString("dddd"),
                    Note = ""
                };
                
                existingDay  = (Day) daysRegistred.ToList().Find(x => x.Date.ToString("dd-MM-yyyy") == dt.ToString("dd-MM-yyyy"));

                if (existingDay != null)
                {
                    days.Add(existingDay);
                }
                else
                {
                    days.Add(day);
                }

                if (DateTime.Now.Day == dt.Day && DateTime.Now.Month == dt.Month) 
                {                    
                    DaySelected = days.Last();
                }



                if (dt.Day%7 == 0)//1settimana
                {
                    week = new Week();
                    week.Days = days;
                    weeks.Add(week);
                    days = new List<IDay>();                    
                }
                if (dt >= endDate)
                {
                    week = new Week();
                    week.Days = days;
                    weeks.Add(week);
                    days = new List<IDay>();
                    break;
                }                
                
            }
            return true;
        }

        /// <summary>
        /// Get all days Name for table header
        /// </summary>
        private void GenerateCalendarHead()
        {
            daysName = new List<string>();
            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                daysName.Add(dt.ToString("dddd"));
            }
            daysName = daysName.Distinct().ToList();          
        }


        public async Task OnMonthChange(ChangeEventArgs e) 
        {
            var monthNameSelected = e.Value.ToString();
            var monthindex = MonthsName.FindIndex((monthname) => monthname == monthNameSelected)+1 ;
            startDate = new DateTime(DateTime.Now.Year, monthindex, 1);
            endDate = (new DateTime(DateTime.Now.Year, monthindex, 1)).AddMonths(1).AddDays(-1);
            GenerateCalendarHead();
            var a = await GenerateCalendarBody(); 


        }

        #region Editing day info


        protected void OnModifyDayInfo(IDay day)=> DaySelected = day;

        protected async void OnSaveDayInfo(IDay day)
        {
            Console.WriteLine(day.DayName + day.Note);
            var newday = await DayServices.CreateOrUpdate(day as Day);
            Console.WriteLine(day.DayName + day.Note);
            DaySelected = newday;
            //chiama l'api e fa post dello specifico id            
        }



        #endregion









        //public WeatherForecast[] forecasts ;

        //protected override async Task OnInitializedAsync()
        //{            
        //    forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");            
        //}

    }



}


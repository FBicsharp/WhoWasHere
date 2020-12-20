using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhoWasHere.Shared;
using WhoWasHere.Shared.Calendar;

namespace WhoWasHere.Client.Pages
{
    public partial class Calendar  :ComponentBase
    {


        
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
                GenerateCalendarBody();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("error" +ex);
                throw;
            }
        }

        /// <summary>
        /// Get all days datails from tabale body
        /// </summary>
        private void GenerateCalendarBody()
        {
            weeks = new List<Week>();            
            Week week = new Week();
            List<IDay> days = new List<IDay>();            

            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                days.Add(new Day()
                {
                    Id = dt.Day,
                    DayNumber = dt.Day,
                    DayName = dt.ToString("dddd"),
                    Note = "bla"
                });

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


        public void OnMonthChange(ChangeEventArgs e) 
        {
            var monthNameSelected = e.Value.ToString();
            var monthindex = MonthsName.FindIndex((monthname) => monthname == monthNameSelected)+1 ;
            startDate = new DateTime(DateTime.Now.Year, monthindex, 1);
            endDate = (new DateTime(DateTime.Now.Year, monthindex, 1)).AddMonths(1).AddDays(-1);
            GenerateCalendarHead();
            GenerateCalendarBody();

        }

        #region Editing day info


        protected void OnModifyDayInfo(IDay day)=> DaySelected = day;

        protected void OnSaveDayInfo(IDay day)=> Console.WriteLine(day.DayName+day.DayNumber+day.Note);//chiama l'api e fa post dello specifico id



        #endregion









        //public WeatherForecast[] forecasts ;

        //protected override async Task OnInitializedAsync()
        //{            
        //    forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");            
        //}

    }



}


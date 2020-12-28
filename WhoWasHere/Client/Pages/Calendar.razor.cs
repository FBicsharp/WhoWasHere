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
        private DateTime dayfromdatapicker { get; set; }

        public IDay DaySelected { get; set; }
        //|§FB001 gestione controllo select
        //public List<string> MonthsName { get; set; } = new List<string>();
        //|çFB001 




        protected override async Task OnInitializedAsync()
        {

            //|§FB001 gestione controllo select
            //MonthsName = CultureInfo.CurrentCulture.DateTimeFormat.MonthGenitiveNames.ToList();
            //MonthsName.Remove("");

            //|çFB001 
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
            DaySelected = null;

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

                if (dayfromdatapicker.ToString("dd-MM-YYYY") == dt.ToString("dd-MM-YYYY")) 
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
                    if (DaySelected == null )
                    {
                        DaySelected = days.Last();
                    }
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


        public async void OnMonthChange(ChangeEventArgs e) 
        {
            //|§FB001 gestione controllo select
            //var monthNameSelected = e.Value.ToString();
            //var monthindex = MonthsName.FindIndex((monthname) => monthname == monthNameSelected) + 1;
            //|çFB001
            DateTime datesel ;
            try
            {
                datesel = DateTime.Parse(e.Value.ToString());
            }
            catch (Exception)
            {
                Console.WriteLine("Formato errato della data");
                return;
            }
            dayfromdatapicker = datesel;
            startDate = new DateTime(dayfromdatapicker.Year, dayfromdatapicker.Month, 1);
            endDate = (new DateTime(dayfromdatapicker.Year, dayfromdatapicker.Month, 1)).AddMonths(1).AddDays(-1);
             
            try
            {
                
                GenerateCalendarHead();
               var a = await GenerateCalendarBody();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error"+ex);                
            }
            
        }

        

        #region Editing day info


        protected void OnModifyDayInfo(IDay day)=> DaySelected = day;

        protected async void OnSaveDayInfo(IDay day)
        {            
            var newday = await DayServices.CreateOrUpdate(day as Day);            
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


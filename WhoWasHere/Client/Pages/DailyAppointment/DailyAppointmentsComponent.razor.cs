using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhoWasHere.Client.Services;
using WhoWasHere.Shared.Round;

namespace WhoWasHere.Client.Pages.DailyAppointment
{
    public partial class DailyAppointmentsComponent : ComponentBase
    {

        [Inject]
        private IAppointmentService _appointmentService { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private ICustomerService _customerService { get; set; }

        public IEnumerable<Appointment> _appointments { get; set; }


        private int _dayid;
        public int DayId { 
            get { return _dayid; } 
            set {
                _dayid = value;
                ReloadData();
            } 
        }

        private async Task ReloadData() 
        {
            await LoadList();
            StateHasChanged();
        }

         


        protected async override Task OnInitializedAsync()
        {
            if (DayId ==0)
            {
                _appointments = new List<Appointment>();
                return ;
            }
            await LoadList();
        }


        public async Task<bool> LoadList()
        {
            _appointments = await _appointmentService.GetAppointmentsOfDayAsync(DayId);
            foreach (var appointment in _appointments)
            {
                appointment.customer = await _customerService.GetCustomerFromIdAsync(appointment.IdCustomer);
            }
            return true;
        }

        public async void Delete(Appointment appointment)
        {
            try
            {

                var response = await _appointmentService.DeleteAppointmentAsync(appointment);

                if (response.Id == 0)
                {
                    _toastService.ShowSuccess("appointment deleted!");
                }
                else
                {
                    _toastService.ShowError("Unable to delete this appointment !");
                }
                await LoadList();
                StateHasChanged();

            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR: " + ex.StackTrace);
            }

        }



        public EventCallback<bool> ChangeDayEventCallback
        {
            get;
            set;
        }

    }
}

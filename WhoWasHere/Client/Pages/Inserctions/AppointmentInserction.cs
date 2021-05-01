using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using WhoWasHere.Client.Services;
using WhoWasHere.Shared.Customer;
using WhoWasHere.Shared.Round;

namespace WhoWasHere.Client.Pages.Inserctions
{
    public partial class AppointmentInserction : ComponentBase
    {
        public Appointment appointment
        {
            get;
            set;
        } = new Appointment { };
        public IEnumerable<CustomerModel> _customer { get; private set; }
        public IEnumerable<Appointment> _appointmentList { get; set; }

        public DateTime StartAppointment { get; set; }
        public DateTime DayAppointment { get; set; }
        public DateTime EndAppointment { get; set; }


        [Inject]
        public IAppointmentService AppointmentService { get; set; }

        [Inject]
        public ICustomerService CustomerService { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        public bool ShowDialog
        {
            get;
            set;
        }
        [Parameter]
        public EventCallback<bool> CloseEventCallback
        {
            get;
            set;
        }
        private DateTime _Day { get; set; }

        public async Task Show(DateTime Day)
        {
            _Day = Day;
            DayAppointment = _Day;//(DateTime.Now.ToString("yyyy/MM/dd hh:mm"));
            StartAppointment = DateTime.Now.AddMinutes(5);//(DateTime.Now.ToString("yyyy/MM/dd hh:mm"));
            EndAppointment = StartAppointment.AddMinutes(15);

            var loadcomplate = await ResetDialog();
            if (loadcomplate)
            {
                ShowDialog = true;
                StateHasChanged();
            }
        }
        public void Close()
        {
            StateHasChanged();
            ShowDialog = false;
        }
        private async Task<bool> ResetDialog()
        {
            appointment = new Appointment { };
            _customer = await CustomerService.GetCustomersAsync();
            //_appointmentList = await AppointmentService.GetAppointmentsOfDayAsync(_Day);
            return true;
        }
        protected async Task HandleValidSubmit()
        {
            appointment.StartAppointment = new DateTime(DayAppointment.Year, DayAppointment.Month, DayAppointment.Day, StartAppointment.Hour, StartAppointment.Minute, 0);
            appointment.EndAppointment = new DateTime(DayAppointment.Year, DayAppointment.Month, DayAppointment.Day, EndAppointment.Hour, EndAppointment.Minute, 0);
            if (appointment.IdCustomer == 0 || appointment.StartAppointment > appointment.EndAppointment)
            {
                _toastService.ShowError("Appointment is incorrect!");

            }
            else
            {


                appointment = await AppointmentService.PostAppointmentAsync(appointment);
                if (appointment.Id > 0)
                {
                    _toastService.ShowSuccess("Appointment inserted!");
                }
                else
                {
                    _toastService.ShowError("Unable to insert this appointment !");
                }
            }

            ShowDialog = false;
            StateHasChanged();
            await CloseEventCallback.InvokeAsync(true);
        }
    }
}
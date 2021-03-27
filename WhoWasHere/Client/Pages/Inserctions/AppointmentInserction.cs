using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IEnumerable<CustomerModel> customerList { get; set; }


        [Inject]
        public IAppointmentService AppointmentService {get;set;}

        [Inject]
        public ICustomerService CustomerService { get; set; }

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
        private int _DayId { get; set; }

        public async Task Show(int DayId)
        {
            _DayId = DayId;

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
            customerList =  await CustomerService.GetCustomersAsync();
            return true;
        }
        protected async Task HandleValidSubmit()
        {
            appointment.IdDay = _DayId;
            await AppointmentService.PostAppointmentAsync(appointment);
            ShowDialog = false;
            StateHasChanged();
            await CloseEventCallback.InvokeAsync(true);
        }
    }
}
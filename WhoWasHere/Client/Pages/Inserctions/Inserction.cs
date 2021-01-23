using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WhoWasHere.Client.Services;
using WhoWasHere.Shared.Customer;

namespace WhoWasHere.Client.Pages.Inserctions
{
    public partial class Inserction : ComponentBase
    {
        public CustomerModel Customers
        {
            get;
            set;
        } = new CustomerModel { };
        [Inject]
        public ICustomerService CustomerService
        {
            get;
            set;
        }
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
        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }
        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }
        private void ResetDialog()
        {
            Customers = new CustomerModel { };
        }
        protected async Task HandleValidSubmit()
        {
            await CustomerService.PostCustomerAsync(Customers);
            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
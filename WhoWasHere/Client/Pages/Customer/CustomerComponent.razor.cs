using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoWasHere.Client.Services;
using WhoWasHere.Shared.Customer;
using WhoWasHere.Client.Pages;

namespace WhoWasHere.Client.Pages.Customer
{
    public partial class CustomerComponent : ComponentBase
    {
    
        

        [Inject]
        private ICustomerService CustomerService { get; set; }
        
        public IEnumerable<CustomerModel> Customerlist { get; set; }

       
        protected async  override Task OnInitializedAsync()
        {
            Customerlist = await CustomerService.GetCustomersAsync();
            
        }


        public async void ReloadList()
        {
             Customerlist = await CustomerService.GetCustomersAsync();
            StateHasChanged();
        }





    }
}

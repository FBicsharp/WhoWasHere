using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoWasHere.Client.Services;
using WhoWasHere.Shared.Customer;
using WhoWasHere.Client.Pages;
using Blazored.Toast.Services;

namespace WhoWasHere.Client.Pages.Customer
{
    public partial class CustomerComponent : ComponentBase
    {
    
        

        [Inject]
        private ICustomerService CustomerService { get; set; }
        [Inject]
        private IToastService toastService { get; set; }
        
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

        public async void Delete(CustomerModel customer)
        {
            try
            {

                var response = await CustomerService.DeleteCustomerAsync(customer);

                if (response.Id ==0)
                {
                    toastService.ShowSuccess("Customer deleted!");
                }
                else
                {
                    toastService.ShowError("Unable to delete this customer !");
                }
                ReloadList();
            }
            catch (Exception ex)
            {

                Console.WriteLine("ERROR: " +ex.StackTrace);
            }

        }



    }
}

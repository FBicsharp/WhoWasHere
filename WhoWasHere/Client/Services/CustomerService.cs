using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoWasHere.Shared.Customer;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace WhoWasHere.Client.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly HttpClient httpClient;
        private readonly string baseURI = "api/Customer/";

        public CustomerService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CustomerModel> DeleteCustomerAsync(CustomerModel customer)
        {
            var response = await httpClient.DeleteAsync($"{baseURI}{customer.Id}");
            if (response.IsSuccessStatusCode)
            {
                return new CustomerModel()
                {
                    Id=0
                };
            }
            else
            {
                return customer;

            }
        }

        public async Task<CustomerModel> GetCustomerFromIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<CustomerModel>($"{baseURI}{id}");
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomersAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<CustomerModel>>($"{baseURI}");
        }

        public async Task<CustomerModel> PostCustomerAsync(CustomerModel customer)
        {
            var response = await httpClient.PostAsJsonAsync<CustomerModel>($"{baseURI}", customer);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CustomerModel>(jsonString);
            }
            else
            {
                return customer;
            }
        }

        public async Task<CustomerModel> PutCustomerAsync(int id, CustomerModel customer)
        {
            var response = await httpClient.PutAsJsonAsync<CustomerModel>($"{baseURI}{id}", customer);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CustomerModel>(jsonString);
            }
            else
            {
                return customer;
            }
        }
    }
}

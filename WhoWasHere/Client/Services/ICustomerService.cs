using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhoWasHere.Shared.Customer;

namespace WhoWasHere.Client.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerModel>> GetCustomersAsync();
        Task<CustomerModel> GetCustomerFromIdAsync(int id);
        Task<CustomerModel> PostCustomerAsync(CustomerModel day);        
        Task<CustomerModel> PutCustomerAsync(int id, CustomerModel customer);
        Task<CustomerModel> DeleteCustomerAsync(CustomerModel customer);
    }
}

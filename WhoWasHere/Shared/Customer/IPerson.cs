using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWasHere.Shared.Customer
{
    public interface IPersonData
    {
        int Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }
        string PhoneNumber { get; set; }
        string Address { get; set; }
        string Notes { get; set; }
        string Mail { get; set; }
        string GetFullName();

    }
}

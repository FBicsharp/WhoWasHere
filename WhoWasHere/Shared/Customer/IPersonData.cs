using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWasHere.Shared.Customer
{
    interface IPersonData
    {
        long PhoneNumber { get; set; }
        string Address { get; set; }
        string Notes { get; set; }
    }
}

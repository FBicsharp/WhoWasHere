using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWasHere.Shared.Customer
{
    interface IPerson
    {
        int Id { get; set; }
        string Name { get; set; }
        string Surname { get; set; }        
        
    }
}

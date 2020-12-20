using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhoWasHere.Client.Pages.Customer
{
    public partial class CustomerComponent : ComponentBase
    {
        public string FullName { get; set; } 
        public string Address { get; set; } 
        public long Number { get; set; } 
        private DateTime LastVisit { get; set; } 
        public string LastVisitDay { get;  } 



    }
}

using DevExpress.Xpo;
using System;

namespace WhoWasHere.Shared.Round
{
    public interface IAppointment 
    {
        
        public int Id { get; set; }
        
        public int IdDay { get; set; }

        public int IdCustomer { get; set; }

        public DateTime StartAppointment { get; set; }

        public DateTime EndAppointment { get; set; }        


    }

}
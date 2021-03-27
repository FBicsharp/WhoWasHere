using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhoWasHere.Shared.Customer;

namespace WhoWasHere.Shared.Round
{
    public class Appointment : IAppointment
    {

        [Key]
        [Column(Order = 1)]
        [Required]
        public int Id { get; set; }

        [Required]
        [Column(Order = 2)]
        public int IdDay { get; set; }


        [Required]
        [Column(Order = 3)]
        public int IdCustomer { get; set; }


        [Required]
        [Column(Order = 4)]
        public DateTime StartAppointment { get; set; }


        [Required]
        [Column(Order = 5)]
        public DateTime EndAppointment { get; set; }
        [NotMapped]
        public IPersonData customer { get; set; }
    }
}

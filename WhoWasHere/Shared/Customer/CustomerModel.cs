using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWasHere.Shared.Customer
{
    public class CustomerModel : IPersonData
    {
        public string GetFullName()
        {
            return this.Name + " " + this.Surname;
        }
        [Key]
        public int Id { get; set; }

        [Required]        
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [MinLength(5)]
        [MaxLength(15)]
        [Required]
        public string PhoneNumber { get; set; }        
        public string Address { get; set; }
        public string Mail { get; set; }
        public string Notes { get; set; }
    }
}

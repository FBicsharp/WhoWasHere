using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoWasHere.Shared.Calendar
{
    public class Day : IDay
    {
        [Key]
        [Column(Order = 1)]
        [Required]
        public int Id { get ; set ; }
                
        [Required]
        [Column(Order = 2)]
        public DateTime Date { get  ; set ; }        
        
        [Required]
        [Column(Order = 3)]
        public string Note { get ; set ; }

        [NotMapped]
        public string DayName { get ; set ; }
    }
}

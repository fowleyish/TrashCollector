using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Models
{
    public class Day
    {
        [Key]
        [Display(Name = "Day ID")]
        public int DayId { get; set; }
        [Display(Name = "Day of the week")]
        public string DayOfWeek { get; set; }
    }
}

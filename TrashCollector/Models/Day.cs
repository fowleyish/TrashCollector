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
        public int DayId { get; set; }
        public string DayOfWeek { get; set; }
    }
}

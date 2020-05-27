using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Models
{
    public class TodayPickup
    {
        public TodayPickup()
        {
            Completed = false;
        }

        [Key]
        public int PickupId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime Date { get; set; }
        public bool Completed { get; set; }
    }
}

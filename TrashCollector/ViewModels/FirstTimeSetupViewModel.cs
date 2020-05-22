using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TrashCollector.Models;

namespace TrashCollector.ViewModels
{
    public class FirstTimeSetupViewModel
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Display(Name = "Preferred pickup day of week")]
        public string DayOfWeek { get; set; }
        public SelectList DaysOfWeek { get; set; }
        [Display(Name = "Street address")]
        public string Street { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "2-letter state code")]
        public string State { get; set; } 
        [Display(Name = "Zip code")]
        public string Zip { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashCollector.Models;

namespace TrashCollector.ViewModels
{
    public class CustomerDashboardViewModel
    {
        public Customer Customer { get; set; }
        public string DayOfWeek { get; set; }
        public SelectList DaysOfWeek { get; set; }
        public string SelectedDay { get; set; }
    }
}

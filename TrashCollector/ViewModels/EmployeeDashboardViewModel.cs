﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashCollector.Models;

namespace TrashCollector.ViewModels
{
    public class EmployeeDashboardViewModel
    {
        public Employee Employee { get; set; }
        public string Zip { get; set; }
        public List<Address> Stops { get; set; }
        public DateTime FocusDay { get; set; }
    }
}

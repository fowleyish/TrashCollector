using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.ViewModels
{
    public class FirstTimeSetupViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DayOfWeek { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}

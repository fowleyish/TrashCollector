using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [ForeignKey("Day")]
        [Display(Name = "Day")]
        public int DayId { get; set; }
        public Day Day { get; set; }
        [ForeignKey("Account")]
        [Display(Name = "Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        [ForeignKey("Address")]
        [Display(Name = "Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        [ForeignKey("User")]
        [Display(Name = "User ID")]
        public string Id { get; set; }
        public IdentityUser User { get; set; }
        public DateTime SuspendStart { get; set; }
        public DateTime SuspendEnd { get; set; }
        public DateTime SpecialPickup { get; set; }
    }
}

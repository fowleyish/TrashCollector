using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Models
{
    public class Account
    {
        [Key]
        [Display(Name = "Account ID")]
        public int AccountId { get; set; }
        [Display(Name = "Balance")]
        public double Balance { get; set; }
    }
}

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
        public int AccountId { get; set; }
        public double Balance
        {
            get => Math.Round(Balance, 2);
            set => Balance = value;
        }
    }
}

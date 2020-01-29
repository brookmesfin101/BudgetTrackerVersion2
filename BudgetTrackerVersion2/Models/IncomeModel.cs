using BudgetTrackerVersion2.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetTrackerVersion2.Models
{
    public class IncomeModel
    {
        [Key]
        public int IncomeId { get; set; }
        public IncomeType IncomeType { get; set; } 
        public string IncomeDescription { get; set; }
        public int IncomeAmount { get; set; }
        public DateTime IncomeDate { get; set; }
    }
}
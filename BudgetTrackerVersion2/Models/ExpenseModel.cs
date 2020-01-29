using BudgetTrackerVersion2.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BudgetTrackerVersion2.Models
{
    public class ExpenseModel
    {
        [Key]
        public int ExpenseId { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public string ExpenseDescription { get; set; }
        public int ExpenseAmount { get; set; }
        public DateTime ExpenseDate { get; set; }
    }
}
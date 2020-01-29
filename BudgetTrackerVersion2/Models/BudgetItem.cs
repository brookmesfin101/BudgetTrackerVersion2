using BudgetTrackerVersion2.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetTrackerVersion2.Models
{
    public class BudgetItem
    {
        public string BudgetItemType { get; set; }
        public string BudgetItemDescription { get; set; }
        public int BudgetItemAmount { get; set; }
        public DateTime BudgetItemDate { get; set; }
        public BudgetGroup BudgetItemGroup { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BudgetTrackerVersion2.Models
{
    public class BalanceModel
    {
        [Key]
        public int BalanceID { get; set; }
        public int Balance { get; set; }
        public DateTime BalanceDate { get; set; }

        [Required]
        public virtual UserModel User { get; set; }
    }
}
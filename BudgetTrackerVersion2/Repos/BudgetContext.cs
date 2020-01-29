using BudgetTrackerVersion2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BudgetTrackerVersion2.Repos
{
    public class BudgetContext : DbContext
    {
        public BudgetContext() : base("BudgetContext")
        {
        }

        public DbSet<IncomeModel> IncomeItems { get; set; }
        public DbSet<ExpenseModel> ExpenseItems { get; set; }
        public DbSet<BalanceModel> BalanceInfo { get; set; }
        public DbSet<UserModel> UserInfo { get; set; }
    }
}
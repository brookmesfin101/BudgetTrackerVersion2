using BudgetTrackerVersion2.Models;
using BudgetTrackerVersion2.Models.Enums;
using BudgetTrackerVersion2.Repos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetTrackerVersion2.Controllers
{
    public class MainController : Controller
    {
        BudgetContext budgetContext = new BudgetContext();

        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddIncomeItem(IncomeModel incomeItem)
        {
            budgetContext.IncomeItems.Add(incomeItem);
            budgetContext.SaveChanges();
            return Json(new { Message = "SUCCESS", JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult AddExpenseItem(ExpenseModel expenseItem)
        {
            budgetContext.ExpenseItems.Add(expenseItem);
            budgetContext.SaveChanges();
            return Json(new { Message = "SUCCESS", JsonRequestBehavior.AllowGet });
        }

        [HttpGet]
        public string GetIncomeItems()
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                }
            };                               

            return JsonConvert.SerializeObject(budgetContext.IncomeItems, settings);
        }

        [HttpGet]
        public string GetExpenseItems()
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                }
            };

            return JsonConvert.SerializeObject(budgetContext.ExpenseItems, settings);
        }

        [HttpGet]
        public string GetBudgetItems()
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (send, args) =>
                {
                    args.ErrorContext.Handled = true;
                }
            };

            var incomeItems = budgetContext.IncomeItems.ToList();
            var expenseItems = budgetContext.ExpenseItems.ToList();

            var budgetItems = incomeItems.Select(x => new BudgetItem()
            {
                BudgetItemAmount = x.IncomeAmount,
                BudgetItemDescription = x.IncomeDescription,
                BudgetItemDate = x.IncomeDate,
                BudgetItemType = x.IncomeType.ToString(),
                BudgetItemGroup = BudgetGroup.Income
            });

            budgetItems = budgetItems.Concat(expenseItems.Select(x => new BudgetItem()
            {
                BudgetItemAmount = x.ExpenseAmount,
                BudgetItemDescription = x.ExpenseDescription,
                BudgetItemDate = x.ExpenseDate,
                BudgetItemType = x.ExpenseType.ToString(),
                BudgetItemGroup = BudgetGroup.Expense
            }));                       

            return JsonConvert.SerializeObject(budgetItems, settings);
        }        

        [HttpDelete]
        public void DeleteIncomeItems()
        {
            var incomeItems = budgetContext.IncomeItems;

            foreach(IncomeModel incomeItem in incomeItems)
            {
                budgetContext.IncomeItems.Remove(incomeItem);
            }
            budgetContext.SaveChanges();
        }

        [HttpDelete]
        public void DeleteExpenseItems()
        {
            var expenseItems = budgetContext.ExpenseItems;

            foreach (ExpenseModel expenseItem in expenseItems)
            {
                budgetContext.ExpenseItems.Remove(expenseItem);
            }
            budgetContext.SaveChanges();
        }

        [HttpDelete]
        public void DeleteBudgetItems()
        {
            DeleteIncomeItems();
            DeleteExpenseItems();
        }

        
    }
}
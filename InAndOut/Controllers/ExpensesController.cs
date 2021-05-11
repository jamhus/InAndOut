using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly DataContext _context;
        public ExpensesController(DataContext context)
        {
            _context = context;
        }

        // GET: ExpensesController
        public ActionResult Index()
        {
            IEnumerable<Expense> items = _context.Expenses.ToList();
            foreach (var item in items)
            {
                item.ExpenseType = _context.ExpenseTypes.FirstOrDefault(u => u.Id == item.ExpenseTypeId);
            }

            return View(items);
        }
        public IActionResult Create()
        {
            ExpenseVM expenseVm = new ExpenseVM()
            {
                Expense = new Expense(),
                ExpenseTypes = _context.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };
            return View(expenseVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM model)
        {

            if (ModelState.IsValid)
            {
                _context.Add(model.Expense);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var expense = _context.Expenses.Find(id);
            if (expense == null) return NotFound();
            return View(expense);

        }

        // POST DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null) return NotFound();
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET UPDATE
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var expense = _context.Expenses.Find(id);
            if (expense == null) return NotFound();

            ExpenseVM expenseVm = new ExpenseVM()
            {
                Expense = expense,
                ExpenseTypes = _context.ExpenseTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };
            return View(expenseVm);

        }

        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(ExpenseVM model)
        {
            if (ModelState.IsValid)
            {
                _context.Expenses.Update(model.Expense);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}

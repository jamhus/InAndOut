using InAndOut.Data;
using InAndOut.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpenseTypesController : Controller
    {
        private readonly DataContext _context;
        public ExpenseTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: ExpensesTypeController
        public ActionResult Index()
        {
            IEnumerable<ExpenseType> items = _context.ExpenseTypes.ToList();
            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseType model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
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
            var expense = _context.ExpenseTypes.Find(id);
            if (expense == null) return NotFound();
            return View(expense);

        }

        // POST DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var type = _context.ExpenseTypes.Find(id);
            if (type == null) return NotFound();
            _context.ExpenseTypes.Remove(type);
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
            var type = _context.ExpenseTypes.Find(id);
            if (type == null) return NotFound();
            return View(type);

        }

        // POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePost(ExpenseType model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}

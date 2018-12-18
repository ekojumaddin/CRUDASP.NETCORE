using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core1.Data;
using Core1.Models;

namespace Core1.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuppliersController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public JsonResult GetData()
        //{
        //    var k = _context.Supplier.ToList();
        //    return Json(k);
        //    //var a = context.Suppliers.Where(x => x.IsDelete == false).ToList();
        //    //return a;
        //}

        //public IActionResult Create()
        //{
        //    return View();
        //}
        [HttpGet]
        // GET: Suppliers
        public IActionResult Index()
        {
            return View(_context.Supplier.ToList());
        }

        // GET: Suppliers/Details/5
        public IActionResult Details(int? id)
        {
            var supplier = _context.Supplier.SingleOrDefault(m => m.Id == id);
            return View(supplier);
        }

        #region Create
        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.CreateDate = DateTimeOffset.Now.LocalDateTime;
                _context.Add(supplier);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }
        #endregion
        #region Edit
        // GET: Suppliers/Edit/5
        public IActionResult Edit(int? id)
        {
            var supplier = _context.Supplier.Find(id);
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Supplier supplier, DateTime CreateDates)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                //    supplier.UpdateDate = DateTimeOffset.Now.ToLocalTime();
                //    _context.Update(supplier);
                //    _context.SaveChanges();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!SupplierExists(supplier.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                supplier.CreateDate = CreateDates;
                supplier.UpdateDate = DateTimeOffset.Now.LocalDateTime;
                _context.Entry(supplier).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }
        #endregion
        #region Delete
        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _context.Supplier
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _context.Supplier.FindAsync(id);
            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        //private bool SupplierExists(int id)
        //{
        //    return _context.Supplier.Any(e => e.Id == id);
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core1.Data;
using Core1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Core1.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: Items
        public IActionResult Index()
        {
            var getItem = _context.Item.Include(i => i.Supplier);
            ViewBag.items = _context.Item.ToList();
            return View(getItem.ToList());
        }

        // GET: Items/Details/5
        public IActionResult Details(int? id)
        {
            var item = _context.Item.Include(i => i.Supplier).SingleOrDefault(m => m.Id == id);
            return View(item);
        }

        #region Create
        // GET: Items/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item item, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                item.Image = "no image";
            }
            else
            {
                string path = "F:/MII/CRUDASP.NETCORE/Core1/wwwroot/images/"+image.FileName;
                image.CopyTo(new FileStream(path, FileMode.Create));
                item.Image = image.FileName;
            }

            if (ModelState.IsValid)
            {
                item.CreateDate = DateTimeOffset.Now.LocalDateTime;
                _context.Add(item);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name", item.SupplierId);
            return View(item);
        }
        #endregion
        #region Edit
        // GET: Items/Edit/5
        public IActionResult Edit(int? id)
        {
            var item = _context.Item.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name", item.SupplierId);
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item, IFormFile image, string imagee, DateTime CreateDates)
        {
            if (image == null || image.Length == 0)
            {
                item.Image = imagee;
                item.CreateDate = CreateDates;
                //item.Image = _context.Item.Find(id).Image;
            }
            else
            {
                string path = "F:/MII/CRUDASP.NETCORE/Core1/wwwroot/images/" + image.FileName;
                image.CopyTo(new FileStream(path, FileMode.Create));
                //var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images",
                //    image.FileName);
                //var stream = new FileStream(path, FileMode.Create);
                item.Image = image.FileName;
                item.CreateDate = CreateDates;
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(item);
                //    _context.SaveChanges();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!ItemExists(item.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                
                item.UpdateDate = DateTimeOffset.Now.LocalDateTime;
                _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name", item.SupplierId);
            return View(item);
        }
        #endregion
        #region Delete
        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            item.DeleteDate = DateTimeOffset.Now.LocalDateTime;
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        //private bool ItemExists(int id)
        //{
        //    return _context.Item.Any(e => e.Id == id);
        //}
    }
}

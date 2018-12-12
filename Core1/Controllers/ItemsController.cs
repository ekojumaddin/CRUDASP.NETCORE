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
        private readonly IHostingEnvironment _appEnvironment;

        public ItemsController(ApplicationDbContext context, IHostingEnvironment appEnvirontment)
        {
            _context = context;
            _appEnvironment = appEnvirontment;
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
            var item = _context.Item.Include(i => i.Supplier).FirstOrDefault(m => m.Id == id);
            return View(item);
        }

        // GET: Items/Create
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
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images",
                    image.FileName);
                var stream = new FileStream(path, FileMode.Create);
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

        //public IActionResult Create(IList<IFormFile> files)
        //{
        //    IFormFile upload = files.FirstOrDefault();
        //    if(upload == null || upload.ContentType.ToLower().StartsWith("wwwroot/images"))
        //    {
        //        using (ApplicationDbContext dbContext = new ApplicationDbContext())
        //        {
        //            MemoryStream ms = new MemoryStream();
        //            upload.OpenReadStream().CopyTo(ms);

        //            System.Drawing.Image image
        //        }
        //    }
        //}
        // GET: Items/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = _context.Item.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name", item.SupplierId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Price,Stock,Image,SupplierId,Id,CreateDate,UpdateDate,DeleteDate,IsDelete")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Name", item.SupplierId);
            return View(item);
        }

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
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
    }
}

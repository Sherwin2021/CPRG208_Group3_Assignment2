using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Group3_Assignment2.MVC.Data;
using Group3_Assignment2.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Web;

namespace Group3_Assignment2.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ApplicationDbContext context,
            ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Products
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.Products.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Products->Index");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Products with search term
        public async Task<IActionResult> Index(string id)
        {
            // ASP.NET Razor page filters any variables you insert in it
            // This protects from XSS injections
            // However, client side sanitizing will also be helpful
            // https://docs.microsoft.com/en-us/aspnet/core/security/cross-site-scripting?view=aspnetcore-3.1'
            try
            {
                // Front-end input sanitization for XSS attack
                id = id.Replace("script", "");
                id = id.Replace("<", "");
                id = id.Replace(">", "");
                // Back-end SQL sanitization for Injection
                id = id.Replace("select", "");
                id = id.Replace("insert", "");
                id = id.Replace("update", "");

                // Encode the text using HTML encoder
                // Not required as Razor page will do it automatically, This is for demo
                // https://docs.microsoft.com/en-us/dotnet/api/system.web.httputility.urlencode?view=netcore-3.1
                ViewBag.searchTerm = HttpUtility.HtmlEncode(id);

                if (string.IsNullOrEmpty(id))
                    return View(await _context.Products.ToListAsync());
                else
                {
                    var plist = await _context.Products.Where(p => p.ProductName.Contains(id)).ToListAsync();
                    return View(plist);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Products->Index(id)");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Products/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Products->Details");
                return RedirectToAction("Error", "Home");
            }
        }
        /*
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Description,Price")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        */
    }
}

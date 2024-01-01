using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketCounterWebApp.Models;

namespace TicketCounterWebApp.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly TicketDbContext _context;
        private readonly IWebHostEnvironment _enc;

        public InvoicesController(TicketDbContext context, IWebHostEnvironment enc)
        {
            _context = context;
            _enc = enc;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            
            var data = await _context.Invoices.Include(i => i.TicketInfos).ToListAsync();


            ViewBag.Count = data.Count;
            ViewBag.GrandTotal = data.Sum(i => i.TicketInfos.Sum(l => l.TotalPrice));

            ViewBag.Average = data.Count > 0 ? data.Average(i => i.TicketInfos.Sum(l => l.TotalPrice)) : 0;
            return View(data);
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.Include(x => x.TicketInfos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            return View(new Invoice());
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,PassengerName,Address,ContactNo,ImagePath, ImageFile,TicketInfos")] Invoice invoice, string command = "")
        {
            if (invoice.ImageFile != null)
            {

                invoice.ImagePath = "\\Image\\" + invoice.ImageFile.FileName;


                string serverPath = _enc.WebRootPath + invoice.ImagePath;


                using FileStream stream = new FileStream(serverPath, FileMode.Create);

                await invoice.ImageFile.CopyToAsync(stream);


            }

            if (command == "Add")
            {
                invoice.TicketInfos.Add(new());

                return View(invoice);
            }
            else if (command.Contains("delete"))
            {
                int idx = int.Parse(command.Split('-')[1]);
                invoice.TicketInfos.RemoveAt(idx);
                ModelState.Clear();
                return View(invoice);
            }

            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.Include(x => x.TicketInfos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedDate,PassengerName,Address,ContactNo,TicketInfos")] Invoice invoice, string command = "")
        {
            if (command == "Add")
            {
                invoice.TicketInfos.Add(new());

                return View(invoice);
            }
            else if (command.Contains("delete"))
            {
                int idx = int.Parse(command.Split('-')[1]);
                invoice.TicketInfos.RemoveAt(idx);
                ModelState.Clear();
                return View(invoice);
            }
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
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
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.Include(x => x.TicketInfos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _context.Database.ExecuteSqlAsync($"exec spDeleteInvoice {id}");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}

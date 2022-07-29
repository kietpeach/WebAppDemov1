using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using static WebApplication1.Helper;

namespace WebApplication1.Controllers
{
    public class BankController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bank
        [HttpGet]
        public async Task<IActionResult> Index(int numRecord)
        {
            try
            {
                if (numRecord == 0)
                {
                    var customerData = (from tempcustomer in _context.Bank select tempcustomer).Take(10);
                    return View(await customerData.ToListAsync());
                    //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index", await customerData.ToListAsync()) });
                }
                else
                {
                    var customerData = (from tempcustomer in _context.Bank select tempcustomer).Take(numRecord);
                    int b = customerData.Count();
                    return View(await customerData.ToListAsync());
                    //return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index", await customerData.ToListAsync()) });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            

        }
        [HttpPost]
        public async Task<IActionResult> Index(string record)
        {
            try
            {
                int numRecord = Convert.ToInt32(record);
                return RedirectToAction("Index",new { numRecord = numRecord });
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // GET: Bank/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _context.Bank
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // GET: Bank/Create
        [NoDirectAccess]
        public IActionResult Create()
        {
            return View(new Bank());
        }

        // POST: Bank/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BankID,BankName,BankInfo,BankAccount,BankNumber,Status,Channel")] Bank bank)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(bank);
                    await _context.SaveChangesAsync();
                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Index", _context.Bank.ToList()) });
                }
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", bank) });
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // GET: Bank/Edit/5
        [NoDirectAccess]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var bank = await _context.Bank.FindAsync(id);
                if (bank == null)
                {
                    return NotFound();
                }
                return View(bank);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // POST: Bank/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BankID,BankName,BankInfo,BankAccount,BankNumber,Status,Channel")] Bank bank)
        {
            try
            {
                if (id != bank.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(bank);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BankExists(bank.ID))
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
                return View(bank);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // GET: Bank/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var bank = await _context.Bank
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (bank == null)
                {
                    return NotFound();
                }

                return View(bank);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        // POST: Bank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var bank = await _context.Bank.FindAsync(id);
                _context.Bank.Remove(bank);
                await _context.SaveChangesAsync();
                return Json(new { html = Helper.RenderRazorViewToString(this, "Index", _context.Bank.ToList()) });
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private bool BankExists(int id)
        {
            try
            {
                return _context.Bank.Any(e => e.ID == id);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

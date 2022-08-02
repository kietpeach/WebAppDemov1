using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> GetFilteredItems()
        {
            try
            {
                //System.Threading.Thread.Sleep(2000);
                // ajax GET -> Request.Query, ajax POST -> Request.Form
                var draw = Request.Form["draw"].FirstOrDefault(); // số lần gọi vào api
                var start = Request.Form["start"].FirstOrDefault(); // số record bỏ qua
                var length = Request.Form["length"].FirstOrDefault(); // số record trong 1 trang
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(); // cột sắp xếp
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault(); // sắp xếp tăng dần hay giảm dần
                var searchValue = Request.Form["search[value]"].FirstOrDefault(); // giá trị ô tìm kiếm
                int pageSize = length != null ? Convert.ToInt32(length) : 0; // số record trong 1 trang
                int skip = start != null ? Convert.ToInt32(start) : 0; // số record bỏ qua
                int recordsTotal = 0; // tổng số record trong 1 bảng CSDL
                var customerData = (from tempcustomer in _context.Bank select tempcustomer);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.BankAccount.Contains(searchValue)
                                                || m.BankName.Contains(searchValue)
                                                || m.BankNumber.Contains(searchValue)
                                                || m.BankInfo.Contains(searchValue));
                }
                recordsTotal = customerData.Count();
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bank/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BankID,BankName,BankInfo,BankAccount,BankNumber,Status,Channel")] Bank bank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bank);
        }

        // GET: Bank/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: Bank/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BankID,BankName,BankInfo,BankAccount,BankNumber,Status,Channel")] Bank bank)
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

        // GET: Bank/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var bank = await _context.Bank
            //    .FirstOrDefaultAsync(m => m.ID == id);
            //if (bank == null)
            //{
            //    return NotFound();
            //}

            //return View(bank);
            var bank = await _context.Bank.FindAsync(id);
            _context.Bank.Remove(bank);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //// POST: Bank/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var bank = await _context.Bank.FindAsync(id);
        //    _context.Bank.Remove(bank);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool BankExists(int id)
        {
            return _context.Bank.Any(e => e.ID == id);
        }
    }
}

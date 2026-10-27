using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using renad.net.Data;
using renad.net.Models;

namespace renad.net.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _db;

        public EmployeeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string? search, int? departmentId)
        {
            var query = _db.Employees.Include(e => e.Department).Include(e => e.Gender).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.Employee_Name.Contains(search) || e.Employee_Email.Contains(search));
            }
            if (departmentId != null)
            {
                query = query.Where(e => e.Department_Id == departmentId);
            }

            ViewBag.Search = search;
            ViewBag.Departments = new SelectList(await _db.Department.ToListAsync(), "Department_Id", "Department_Name", departmentId);
            return View(await query.OrderBy(e => e.Employee_Name).ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _db.Employees.Include(e => e.Department).Include(e => e.Gender)
                .FirstOrDefaultAsync(e => e.Employee_Id == id);
            return employee == null ? NotFound() : View(employee);
        }

        public async Task<IActionResult> Create()
        {
            await LoadLookups();
            return View(new Employee());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            await CheckUniqueEmail(employee);
            if (!ModelState.IsValid)
            {
                await LoadLookups();
                return View(employee);
            }

            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            TempData["Message"] = "Employee added.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            await LoadLookups();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Employee_Id) return BadRequest();

            await CheckUniqueEmail(employee);
            if (!ModelState.IsValid)
            {
                await LoadLookups();
                return View(employee);
            }

            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
            TempData["Message"] = "Employee updated.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Employee_Id == id);
            return employee == null ? NotFound() : View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _db.Employees.FindAsync(id);
            if (employee != null)
            {
                _db.Employees.Remove(employee);
                await _db.SaveChangesAsync();
                TempData["Message"] = "Employee deleted.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadLookups()
        {
            ViewBag.Departments = new SelectList(await _db.Department.ToListAsync(), "Department_Id", "Department_Name");
            ViewBag.Genders = new SelectList(await _db.Genders.ToListAsync(), "Gender_Id", "Gender_Name");
        }

        private async Task CheckUniqueEmail(Employee employee)
        {
            if (await _db.Employees.AnyAsync(e => e.Employee_Email == employee.Employee_Email && e.Employee_Id != employee.Employee_Id))
            {
                ModelState.AddModelError(nameof(employee.Employee_Email), "Another employee already uses this email.");
            }
        }
    }
}

using CFirstApproach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CFirstApproach.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeeDBContext employeeDB;

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(EmployeeDBContext employeeDB) 
        {
            this.employeeDB = employeeDB;
        }
        public async Task<IActionResult> Index()
        {
            var empData = await employeeDB.Employees.ToListAsync();
            return View(empData);
        }

        //Create method for creating data 
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            if (ModelState.IsValid)
            { 
                await employeeDB.Employees.AddAsync(emp);
                await employeeDB.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View(emp);
        }
        //edit method
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            { 
                return NotFound();
            }
            var empData = await employeeDB.Employees.FindAsync(id);

            if(empData == null) 
            {
                return NotFound();
            }
            await employeeDB.Employees.FindAsync(id);
            return View(empData);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee editEmployee)
        {
            if(id != editEmployee.Id) 
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            { 
                employeeDB.Entry(editEmployee).State =EntityState.Modified;
                await employeeDB.SaveChangesAsync();

                //Redirect to details action
                return RedirectToAction("Index","Home");
            }
            
            return View(editEmployee);
        }

        //Details

        public async Task<IActionResult> Details(int? id)
        { 
            if(id==null || employeeDB.Employees == null) 
            {
                return NotFound();
            }
            var empData =  await employeeDB.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (empData == null) 
            {
                return NotFound();
            }
            return View(empData);
        }

        //Delete


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || employeeDB.Employees == null)

            {
                return NotFound();
            }

            var empData = await employeeDB.Employees.FindAsync(id);
            if (empData == null)
            {
                return NotFound();
            }
            return View(empData);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, Employee editEmployee)
        {
            var empData = await employeeDB.Employees.FindAsync(id);
            if (empData == null) 
            {
                return NotFound();
            }
            employeeDB.Employees.Remove(empData);
            await employeeDB.SaveChangesAsync();
            return RedirectToAction("Index");

           
        }








        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
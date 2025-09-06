using Microsoft.AspNetCore.Mvc;
using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private static readonly List<Employee> employees = new()
        {
            new Employee { EmployeeId = 1, FullName = "John Manager", Email = "john@company.com", Role = "Manager" },
            new Employee { EmployeeId = 2, FullName = "Alice Employee", Email = "alice@company.com", Role = "Employee" },
            new Employee { EmployeeId = 3, FullName = "Bob Employee", Email = "bob@company.com", Role = "Employee" }
        };

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.EmployeeList = employees;
            return View();
        }


        [HttpPost]
        public IActionResult Index(int employeeId)
        {
            var emp = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (emp == null)
                return View("Error");

            HttpContext.Session.SetInt32("EmployeeId", emp.EmployeeId);
            HttpContext.Session.SetString("Role", emp.Role);
            HttpContext.Session.SetString("Name", emp.FullName);
            HttpContext.Session.SetString("Email", emp.Email);

            return RedirectToAction(emp.Role == "Manager" ? "ManagerHome" : "EmployeeHome", "LeaveRequests");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}


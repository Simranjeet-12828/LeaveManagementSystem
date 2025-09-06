using LeaveManagementSystem.Models;
using LeaveManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Controllers
{
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveService _leaveService;

        public LeaveRequestsController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        // GET: /LeaveRequests/ManagerHome
        public IActionResult ManagerHome()
        {
            if (HttpContext.Session.GetString("Role") != "Manager")
                return Unauthorized();

            return View();
        }

        // GET: /LeaveRequests/EmployeeHome
        public IActionResult EmployeeHome()
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return Unauthorized();

            return View();
        }

        // GET: /LeaveRequests/History
        public async Task<IActionResult> History()
        {
            var role = HttpContext.Session.GetString("Role");
            var employeeId = HttpContext.Session.GetInt32("EmployeeId") ?? 0;

            var leaves = await _leaveService.GetLeavesAsync();

            if (role == "Manager")
            {
                return View(leaves); // Manager sees all requests
            }
            else if (role == "Employee")
            {
                var employeeLeaves = leaves.Where(l => l.EmployeeId == employeeId).ToList();
                return View(employeeLeaves);
            }

            return Unauthorized();
        }

        // GET: /LeaveRequests/Apply
        public IActionResult Apply()
        {
            if (HttpContext.Session.GetString("Role") != "Employee")
                return Unauthorized();

            var employeeId = HttpContext.Session.GetInt32("EmployeeId") ?? 0;
            return View(new LeaveRequest { EmployeeId = employeeId });
        }
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Manager")
                return Unauthorized();

            await _leaveService.UpdateLeaveStatusAsync(id, "Approved");
            return RedirectToAction("History");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            if (HttpContext.Session.GetString("Role") != "Manager")
                return Unauthorized();

            await _leaveService.UpdateLeaveStatusAsync(id, "Rejected");
            return RedirectToAction("History");
        }



        [HttpPost]
        public async Task<IActionResult> Apply(LeaveRequest leave)
        {
            leave.EmployeeId = HttpContext.Session.GetInt32("EmployeeId") ?? 0;
            leave.Status = "Pending";
            leave.DateRequested = DateTime.Now;

            var result = await _leaveService.SubmitLeaveAsync(leave);
            if (!result)
            {
                ModelState.AddModelError("", "Failed to submit leave request. Please check the API or try again.");
                return View(leave);
            }

            return RedirectToAction("EmployeeHome"); 
        }



        // GET: /LeaveRequests/Pending
        public async Task<IActionResult> Pending()
        {
            if (HttpContext.Session.GetString("Role") != "Manager")
                return Unauthorized();

            var allLeaves = await _leaveService.GetLeavesAsync();
            var pending = allLeaves.Where(l => l.Status == "Pending").ToList();

            return View(pending);
        }

        // POST: /LeaveRequests/UpdateStatus
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            if (HttpContext.Session.GetString("Role") != "Manager")
                return Unauthorized();

            var allLeaves = await _leaveService.GetLeavesAsync();
            var leave = allLeaves.FirstOrDefault(l => l.LeaveRequestId == id);

            if (leave == null)
                return NotFound();

            leave.Status = status;

            await _leaveService.SubmitLeaveAsync(leave);

            return RedirectToAction("Pending");
        }
    }
}

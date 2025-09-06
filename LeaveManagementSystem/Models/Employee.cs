namespace LeaveManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // "Manager" or "Employee"

        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}



































































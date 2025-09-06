namespace LeaveManagementSystemAPI.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}

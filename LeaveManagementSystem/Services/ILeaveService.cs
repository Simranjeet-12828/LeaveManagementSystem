using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Services
{
    public interface ILeaveService
    {
        Task<List<LeaveRequest>> GetLeavesAsync();
        Task<bool> SubmitLeaveAsync(LeaveRequest leave);
        Task<bool> UpdateLeaveStatusAsync(int leaveId, string newStatus);


    }
}

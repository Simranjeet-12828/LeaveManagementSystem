using LeaveManagementSystem.Models;
using System.Net.Http.Json;

namespace LeaveManagementSystem.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly HttpClient _httpClient;

        public LeaveService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<LeaveRequest>> GetLeavesAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<LeaveRequest>>("LeaveApi");
                return result ?? new List<LeaveRequest>();
            }
            catch
            {
                // Handle error and (log if needed)
                return new List<LeaveRequest>();
            }
        }

        public async Task<bool> SubmitLeaveAsync(LeaveRequest leave)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("LeaveApi", leave);
                var msg = await response.Content.ReadAsStringAsync(); // log this
                Console.WriteLine("API Response: " + msg);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error submitting leave: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateLeaveStatusAsync(int leaveId, string newStatus)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"LeaveApi/{leaveId}/status", new { Status = newStatus });
                Console.WriteLine($"Calling API: LeaveApi/{leaveId}/status with status = {newStatus}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating leave status: {ex.Message}");
                return false;
            }
        }


    }
}


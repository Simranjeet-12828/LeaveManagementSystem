using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using LeaveManagementSystem.Models;
using LeaveManagementSystem.Services;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Services.Tests
{
    [TestClass]
    public class LeaveServiceTests
    {
        private LeaveService _leaveService;
        private Mock<HttpMessageHandler> _mockHandler;

        [TestInitialize]
        public void Setup()
        {
            _mockHandler = new Mock<HttpMessageHandler>();

            var client = new HttpClient(_mockHandler.Object)
            {
                BaseAddress = new System.Uri("https://localhost:7036/api/")
            };

            _leaveService = new LeaveService(client);
        }

        [TestMethod]
        public async Task SubmitLeaveAsync_ReturnsTrue_WhenApiAcceptsRequest()
        {
            // Arrange
            var leave = new LeaveRequest
            {
                EmployeeId = 1,
                LeaveType = "Sick Leave",
                FromDate = System.DateTime.Today,
                ToDate = System.DateTime.Today.AddDays(1),
                Reason = "Testing",
                Status = "Pending"
            };

            var mockResponse = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent("Created")
            };

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            // Act
            var result = await _leaveService.SubmitLeaveAsync(leave);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetLeavesAsync_ReturnsList_WhenApiIsSuccessful()
        {
            // Arrange
            var mockLeaves = new List<LeaveRequest>
            {
                new LeaveRequest
                {
                    LeaveRequestId = 1,
                    EmployeeId = 123,
                    LeaveType = "Sick Leave",
                    Status = "Approved"
                }
            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(mockLeaves))
            };
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            // Act
            var result = await _leaveService.GetLeavesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Sick Leave", result[0].LeaveType);
        }

        [TestMethod]
        public async Task UpdateLeaveStatusAsync_ReturnsTrue_WhenStatusUpdated()
        {
            // Arrange
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK);
            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);

            // Act
            var result = await _leaveService.UpdateLeaveStatusAsync(1, "Approved");

            // Assert
            Assert.IsTrue(result);
        }
    }
}

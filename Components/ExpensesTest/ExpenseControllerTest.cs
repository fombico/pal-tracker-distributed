using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Expenses;
using Xunit;

namespace ExpensesTest
{
    public class ExpenseControllerTest
    {
        private readonly Mock<IExpenseDataGateway> _gateway;
        private readonly Mock<IProjectClient> _client;
        private readonly ExpenseController _controller;
        
        public ExpenseControllerTest()
        {
            _gateway = new Mock<IExpenseDataGateway>();
            _client = new Mock<IProjectClient>();
            _controller = new ExpenseController(_gateway.Object, _client.Object);
        }

        [Fact]
        public void TestPost()
        {
            _gateway.Setup(g => g.Create(1000, 2000, ExpenseType.EQUIPMENT, 100.00, new DateTime(2019, 2, 28, 14, 30, 0)))
                .Returns(new ExpenseRecord(100, 1000, 2000, ExpenseType.EQUIPMENT, 100.00, new DateTime(2019, 2, 28, 14, 30, 0)));

            _client.Setup(c => c.Get(2000)).Returns(Task.FromResult(new ProjectInfo(true)));

            var response = _controller.Post(new ExpenseInfo(-1, 1000, 2000, ExpenseType.EQUIPMENT, 100.00, new DateTime(2019, 2, 28, 14, 30, 0)));
            Assert.IsType<CreatedResult>(response);

            var body = (ExpenseInfo)((ObjectResult)response).Value;
            Assert.Equal(100, body.Id);
            Assert.Equal(1000, body.UserId);
            Assert.Equal(2000, body.ProjectId);
            Assert.Equal(ExpenseType.EQUIPMENT, body.ExpenseType);
            Assert.Equal(100.00, body.TotalAmountSpent);
            Assert.Equal(28, body.Date.Day);
            Assert.Equal(2, body.Date.Month);
            Assert.Equal(2019, body.Date.Year);
            Assert.Equal(14, body.Date.Hour);
            Assert.Equal(30, body.Date.Minute);
            Assert.Equal(0, body.Date.Second);
        }

        [Fact]
        public void TestPost_InactiveProject()
        {
            _gateway.Setup(g => g.Create(1000, 2000, ExpenseType.EQUIPMENT, 100.00, new DateTime(2019, 2, 28, 14, 30, 0)))
                .Returns(new ExpenseRecord(100, 1000, 2000, ExpenseType.EQUIPMENT, 100.00, new DateTime(2019, 2, 28, 14, 30, 0)));

            _client.Setup(c => c.Get(55432)).Returns(Task.FromResult(new ProjectInfo(false)));

            var response = _controller.Post(new ExpenseInfo(-1, 1000, 2000, ExpenseType.EQUIPMENT, 100.00, new DateTime(2019, 2, 28, 14, 30, 0)));

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(304, ((StatusCodeResult)response).StatusCode);
        }

        [Fact]
        public void TestGet()
        {
            _gateway.Setup(g => g.FindByExpenseId(100))
                .Returns(new ExpenseRecord(100, 1000, 2000, ExpenseType.EQUIPMENT, 100.00, new DateTime(2019, 2, 28, 14, 30, 0)));

            var response = _controller.Get(100);
            Assert.IsType<OkObjectResult>(response);

            var body = (ExpenseInfo)((ObjectResult)response).Value;
            Assert.Equal(100, body.Id);
            Assert.Equal(1000, body.UserId);
            Assert.Equal(2000, body.ProjectId);
            Assert.Equal(ExpenseType.EQUIPMENT, body.ExpenseType);
            Assert.Equal(100.00, body.TotalAmountSpent);
            Assert.Equal(28, body.Date.Day);
            Assert.Equal(2, body.Date.Month);
            Assert.Equal(2019, body.Date.Year);
            Assert.Equal(14, body.Date.Hour);
            Assert.Equal(30, body.Date.Minute);
            Assert.Equal(0, body.Date.Second);
        }
    }
}

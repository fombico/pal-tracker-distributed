using System;
using Microsoft.EntityFrameworkCore;
using TestSupport;
using Expenses;
using Xunit;


namespace ExpensesTest
{
    public class ExpenseDataGatewayTest
    {
        private static readonly TestDatabaseSupport Support =
            new TestDatabaseSupport(TestDatabaseSupport.ExpensesConnectionString);

        private static readonly DbContextOptions<ExpenseContext> DbContextOptions =
            new DbContextOptionsBuilder<ExpenseContext>().UseMySql(TestDatabaseSupport.ExpensesConnectionString)
                .Options;

        public ExpenseDataGatewayTest()
        {
            Support.TruncateAllTables();
        }

        [Fact]
        public void TestCreate()
        {
            var gateway = new ExpenseDataGateway(new ExpenseContext(DbContextOptions));
            gateway.Create(1000, 2000, ExpenseType.EQUIPMENT, 100.00, new DateTime(2019, 2, 28, 14, 30, 0));

            // todo...
            var projectIds = Support.QuerySql("select project_id from expenses");

            Assert.Equal(2000L, projectIds[0]["project_id"]);
        }

        [Fact]
        public void TestFind()
        {
            Support.ExecSql(@"insert into expenses (id, user_id, project_id, expense_type, total_amount_spent, date) 
values (150, 1500, 2500, 'EQUIPMENT', 150.00, now());");

            var gateway = new ExpenseDataGateway(new ExpenseContext(DbContextOptions));
            var actual = gateway.FindByExpenseId(150);

            Assert.Equal(150, actual.Id);
            Assert.Equal(1500, actual.UserId);
            Assert.Equal(2500, actual.ProjectId);
            Assert.Equal(ExpenseType.EQUIPMENT.Value, actual.ExpenseType);
            Assert.Equal(150.00, actual.TotalAmountSpent);
            Assert.NotNull(actual.Date);
        }
    }
}

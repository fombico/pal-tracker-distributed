using System;
using Expenses;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTest
{
    public class ExpenseDataGateway : IExpenseDataGateway
    {
        private ExpenseContext _expenseContext;

        public ExpenseDataGateway(ExpenseContext expenseContext)
        {
            _expenseContext = expenseContext;
        }

        public ExpenseRecord Create(long userId, long projectId, ExpenseType expenseType, double totalAmountSpent, DateTime date)
        {
            var recordToCreate = new ExpenseRecord(userId, projectId, expenseType, totalAmountSpent, date);

            _expenseContext.ExpenseRecords.Add(recordToCreate);
            _expenseContext.SaveChanges();

            return recordToCreate;
        }

        public ExpenseRecord FindByExpenseId(long expenseId) => _expenseContext.ExpenseRecords
            .AsNoTracking()
            .Where(e => e.Id == expenseId)
            .First();
    }
}
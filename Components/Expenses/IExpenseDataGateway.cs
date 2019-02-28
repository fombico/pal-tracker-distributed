using System;

namespace Expenses
{
    public interface IExpenseDataGateway
    {
        ExpenseRecord Create(long userId, long projectId, ExpenseType expenseType, double totalAmountSpent, DateTime date);
        ExpenseRecord FindByExpenseId(long expenseId);
    }
}
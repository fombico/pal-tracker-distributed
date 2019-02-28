using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses
{
    public class ExpenseInfo
    {
        public long Id { get; private set; }
        public long UserId { get; private set; }
        public long ProjectId { get; private set; }
        public ExpenseType ExpenseType { get; private set; }
        public double TotalAmountSpent { get; private set; }
        public DateTime Date { get; private set; }

        public ExpenseInfo(long id, long userId, long projectId, ExpenseType expenseType, double totalAmountSpent, DateTime date)
        {
            Id = id;
            UserId = userId;
            ProjectId = projectId;
            ExpenseType = expenseType;
            TotalAmountSpent = totalAmountSpent;
            Date = date;
        }
    }
}

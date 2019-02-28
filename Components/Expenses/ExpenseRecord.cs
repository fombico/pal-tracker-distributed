using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expenses
{
    [Table("expenses")]
    public class ExpenseRecord
    {
        [Column("id")] public long Id { get; private set; }
        [Column("user_id")] public long UserId { get; private set; }
        [Column("project_id")] public long ProjectId { get; private set; }
        [Column("expense_type")] public string ExpenseType { get; private set; }
        [Column("total_amount_spent")] public double TotalAmountSpent { get; private set; }
        [Column("date")] public DateTime Date { get; private set; }

        private ExpenseRecord()
        {
        }

        public ExpenseRecord(long userId, long projectId, ExpenseType expenseType, double totalAmountSpent, DateTime date) : 
            this(default(long), userId, projectId, expenseType, totalAmountSpent, date)
        {
        }

        public ExpenseRecord(long id, long userId, long projectId, ExpenseType expenseType, double totalAmountSpent, DateTime date)
        {
            Id = id;
            UserId = userId;
            ProjectId = projectId;
            ExpenseType = expenseType.Value;
            TotalAmountSpent = totalAmountSpent;
            Date = date;
        }
    }

}
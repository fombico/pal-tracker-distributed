using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses
{
    public class ExpenseType
    {
        private ExpenseType(string value) { Value = value; }

        public string Value { get; set; }

        public static ExpenseType FOOD { get { return new ExpenseType("FOOD"); } }
        public static ExpenseType LODGING { get { return new ExpenseType("LODGING"); } }
        public static ExpenseType TRAVEL { get { return new ExpenseType("TRAVEL"); } }
        public static ExpenseType EQUIPMENT { get { return new ExpenseType("EQUIPMENT"); } }
        public static ExpenseType OTHER { get { return new ExpenseType("OTHER"); } }
    }
}

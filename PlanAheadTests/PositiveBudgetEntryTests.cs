using NUnit.Framework;
using System;

namespace PlanAhead {
    [TestFixture]
    public class PositiveBudgetEntryTests: BudgetCategoryTestsBase {

        public PositiveBudgetEntryTests(): base(1) {
        }

        protected override FinancialTransaction CreateTransaction(DateTime date, Money value) {
            return new DepositTransaction(date, value);
        }
    }
}


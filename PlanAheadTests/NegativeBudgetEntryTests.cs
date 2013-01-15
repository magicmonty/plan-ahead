using NUnit.Framework;
using System;

namespace PlanAhead {
    [TestFixture]
    public class NegativeBudgetEntryTests: BudgetCategoryTestsBase {

        public NegativeBudgetEntryTests(): base(-1) {
        }

        protected override FinancialTransaction CreateTransaction(DateTime date, Money value) {
            return new WithdrawTransaction(date, value);
        }
    }
}


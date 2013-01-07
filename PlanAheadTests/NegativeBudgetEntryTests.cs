using NUnit.Framework;
using System;

namespace PlanAhead {
    [TestFixture]
    public class NegativeBudgetEntryTests: BudgetCategoryTestsBase {

        public NegativeBudgetEntryTests(): base(-1) {
        }

        protected override void CreateBudgetEntry() {
            budgetEntry = new NegativeBudgetEntry(name, budget);
        }

        protected override FinancialTransaction CreateTransaction(DateTime date, Money value) {
            return new WithdrawTransaction(date, value);
        }

        protected override void AssertType(BudgetCategory entry) {
            Assert.IsInstanceOf(typeof(NegativeBudgetEntry), entry);
        }
    }
}


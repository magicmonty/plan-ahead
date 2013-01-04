using NUnit.Framework;

namespace PlanAhead {
    [TestFixture]
    public class NegativeBudgetEntryTests: BudgetEntryTestsBase {

        public NegativeBudgetEntryTests(): base(-1) {
        }

        protected override void CreateBudgetEntry() {
            budgetEntry = new NegativeBudgetEntry(name, month, year, budget);
        }

        protected override Transaction CreateTransaction(int day, Money value) {
            return new WithdrawTransaction(day, value);
        }

        protected override void AssertType(BudgetEntry entry) {
            Assert.IsInstanceOf(typeof(NegativeBudgetEntry), entry);
        }
    }
}


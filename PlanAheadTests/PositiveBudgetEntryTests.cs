using NUnit.Framework;

namespace PlanAhead {
    [TestFixture]
    public class PositiveBudgetEntryTests: BudgetEntryTestsBase {

        public PositiveBudgetEntryTests(): base(1) {
        }

        protected override void CreateBudgetEntry() {
            budgetEntry = new PositiveBudgetEntry(name, month, year, budget);
        }

        protected override BudgetEntryTransaction CreateTransaction(int day, Money value) {
            return new DepositTransaction(day, value);
        }

        protected override void AssertType(BudgetEntry entry) {
            Assert.IsInstanceOf(typeof(PositiveBudgetEntry), entry);
        }
    }
}


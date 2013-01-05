using NUnit.Framework;

namespace PlanAhead {

    [TestFixture]
    public class MonthlyBudgetEntriesTests {

        private MonthlyBudgetEntries monthlyBudgetEntries;
        private BudgetEntry addedEntry;

        private static readonly string TEST_ENTRY_NAME = "TestEntry";
        private static readonly Money TEST_BUDGET = new Money(42);
        private static readonly int TEST_YEAR = 2012;
        private static readonly Month TEST_MONTH = Month.April;

        [SetUp]
        public void SetUp() {
            monthlyBudgetEntries = new MonthlyBudgetEntries(TEST_YEAR, TEST_MONTH);
        }

        private void AssertYearAndMonth(BudgetEntry entry) {
            Assert.AreEqual(TEST_YEAR, entry.Year);
            Assert.AreEqual(TEST_MONTH, entry.Month);
        }

        private void AddPositiveBudgetEntry() {
            addedEntry = monthlyBudgetEntries.AddPositive(TEST_ENTRY_NAME, TEST_BUDGET);
        }

        private void AddNegativeBudgetEntry() {
            addedEntry = monthlyBudgetEntries.AddNegative(TEST_ENTRY_NAME, TEST_BUDGET);
        }

        [Test]
        public void AnAddedPositiveBudgetEntryShouldHaveTheYearAndTheMonthOfTheContainer() {
            AddPositiveBudgetEntry();
            Assert.IsInstanceOf(typeof(PositiveBudgetEntry), addedEntry);
            AssertYearAndMonth(addedEntry);
        }

        [Test]
        public void AnAddedNegativeBudgetEntryShouldHaveTheYearAndTheMonthOfTheContainer() {
            AddNegativeBudgetEntry();
            Assert.IsInstanceOf(typeof(NegativeBudgetEntry), addedEntry);
            AssertYearAndMonth(addedEntry);
        }

        [Test]
        public void APositiveBudgetEntryShouldNotBeAddedTwice() {
            AddPositiveBudgetEntry();
            Assert.Throws<BudgetEntryExistsAlreadyException>(() => AddPositiveBudgetEntry());
        }

        [Test]
        public void ANegativeBudgetEntryShouldNotBeAddedTwice() {
            AddNegativeBudgetEntry();
            Assert.Throws<BudgetEntryExistsAlreadyException>(() => AddNegativeBudgetEntry());
        }

        [Test]
        public void ANegativeBudgetEntryShouldNotBeAddedIfAPositiveOfSameNameExists() {
            AddPositiveBudgetEntry();
            Assert.Throws<BudgetEntryExistsAlreadyException>(() => AddNegativeBudgetEntry());
        }

        [Test]
        public void APositiveBudgetEntryShouldNotBeAddedIfANegativeOfSameNameExists() {
            AddNegativeBudgetEntry();
            Assert.Throws<BudgetEntryExistsAlreadyException>(() => AddPositiveBudgetEntry());
        }
    }
}


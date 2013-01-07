using System;
using NUnit.Framework;

namespace PlanAhead {
    public abstract class BudgetCategoryTestsBase {

        private readonly Money TEST_VALUE_SMALL = new Money(20);
        private readonly Money TEST_VALUE = new Money(42);
        private readonly Money TEST_VALUE_TIMES_3;
        private readonly int factor;

        protected string name;
        protected Money budget;
        protected BudgetCategory budgetEntry;

        public BudgetCategoryTestsBase(int factor) {
            this.factor = factor;
            this.TEST_VALUE_TIMES_3 = TEST_VALUE * factor * 3;
        }

        [SetUp]
        public void SetUp() {
            name = "TestEntry";
            budget = TEST_VALUE * factor;

            CreateBudgetEntry();
        }


        protected abstract void CreateBudgetEntry();
        protected abstract BudgetEntryTransaction CreateTransaction(DateTime date, Money value);
        protected abstract void AssertType(BudgetCategory entry);

        private void AssertNextMonthsValueIs(Money value) {
            Assert.AreEqual(value, budgetEntry.GetNextMonthsValue());
        }

        private BudgetEntryTransaction CreateSmallTestTransaction() {
            return CreateTransaction(DateTime.Today, TEST_VALUE_SMALL);
        }

        private BudgetEntryTransaction CreateTestTransaction() {
            return CreateTransaction(DateTime.Today, TEST_VALUE);
        }

        private void AddTransaction() {
            budgetEntry = budgetEntry.AddTransaction(CreateTestTransaction());
        }

        private void AddSmallTransaction() {
            budgetEntry = budgetEntry.AddTransaction(CreateSmallTestTransaction());
        }

        private void CloseEntry() {
            budgetEntry = budgetEntry.Close();
        }

        private void AddThreeTransactions() {
            AddTransaction();
            AddTransaction();
            AddTransaction();
        }

        [Test]
        public void ABudgetEntryWithoutTransactionsShouldHaveANextMonthsValueOfItsBudgetValue() {
            AssertNextMonthsValueIs(budget);
        }

        [Test]
        public void ABudgetEntryWithTransactionsOfLessThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            AddSmallTransaction();
            AssertNextMonthsValueIs(budget);
        }

        [Test]
        public void ABudgetEntryWithTransactionsOfSameThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            AddTransaction();
            AssertNextMonthsValueIs(TEST_VALUE * factor);
        }

        [Test]
        public void ABudgetEntryWithTransactionsOfMoreThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            AddThreeTransactions();
            AssertNextMonthsValueIs(TEST_VALUE_TIMES_3);
        }

        [Test]
        public void AClosedBudgetEntryWithoutTransactionsShouldHaveANextMonthsValueOf0() {
            CloseEntry();
            AssertNextMonthsValueIs(Money.ZERO);
        }

        [Test]
        public void AClosedBudgetEntryWithTransactionsLessThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            AddSmallTransaction();
            AddSmallTransaction();
            CloseEntry();
            AssertNextMonthsValueIs(factor * 2 * TEST_VALUE_SMALL);
        }

        [Test]
        public void AClosedBudgetEntryWithTransactionsOfSameThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            AddTransaction();
            CloseEntry();
            AssertNextMonthsValueIs(factor * TEST_VALUE);
        }

        [Test]
        public void AClosedBudgetEntryWithTransactionsOfMoreThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            AddThreeTransactions();
            CloseEntry();
            AssertNextMonthsValueIs(TEST_VALUE_TIMES_3);
        }

        [Test]
        public void AClosedBudgetEntryShouldNotAcceptAnyFurtherTransactions() {
            CloseEntry();
            Assert.Throws<BudgetIsClosedException>(() => budgetEntry.AddTransaction(CreateTestTransaction()));
        }
    }
}

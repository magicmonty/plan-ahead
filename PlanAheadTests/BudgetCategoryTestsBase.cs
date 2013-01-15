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
            budgetEntry = new BudgetCategory(name, budget);
        }

        protected abstract FinancialTransaction CreateTransaction(DateTime date, Money value);

        private void AssertValueIs(Money value) {
            Assert.AreEqual(value, budgetEntry.GetValue((Month)DateTime.Today.Month, DateTime.Today.Year));
        }

        private FinancialTransaction CreateSmallTestTransaction() {
            return CreateTransaction(DateTime.Today, TEST_VALUE_SMALL);
        }

        private FinancialTransaction CreateTestTransaction() {
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
            AssertValueIs(budget);
        }

        [Test]
        public void ABudgetEntryWithTransactionsOfLessThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            AddSmallTransaction();
            AssertValueIs(budget);
        }

        [Test]
        public void ABudgetEntryWithTransactionsOfSameThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            AddTransaction();
            AssertValueIs(TEST_VALUE * factor);
        }

        [Test]
        public void ABudgetEntryWithTransactionsOfMoreThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            AddThreeTransactions();
            AssertValueIs(TEST_VALUE_TIMES_3);
        }

        [Test]
        public void AClosedBudgetEntryWithoutTransactionsShouldHaveANextMonthsValueOf0() {
            CloseEntry();
            AssertValueIs(Money.ZERO);
        }

        [Test]
        public void AClosedBudgetEntryWithTransactionsLessThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            AddSmallTransaction();
            AddSmallTransaction();
            CloseEntry();
            AssertValueIs(factor * 2 * TEST_VALUE_SMALL);
        }

        [Test]
        public void AClosedBudgetEntryWithTransactionsOfSameThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            AddTransaction();
            CloseEntry();
            AssertValueIs(factor * TEST_VALUE);
        }

        [Test]
        public void AClosedBudgetEntryWithTransactionsOfMoreThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            AddThreeTransactions();
            CloseEntry();
            AssertValueIs(TEST_VALUE_TIMES_3);
        }

        [Test]
        public void AClosedBudgetEntryShouldNotAcceptAnyFurtherTransactions() {
            CloseEntry();
            Assert.Throws<BudgetIsClosedException>(() => budgetEntry.AddTransaction(CreateTestTransaction()));
        }
    }
}


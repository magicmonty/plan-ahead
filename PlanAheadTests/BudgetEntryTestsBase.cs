using System;
using NUnit.Framework;

namespace PlanAhead {
    public abstract class BudgetEntryTestsBase {

        private readonly Money TEST_VALUE_SMALL = new Money(20);
        private readonly Money TEST_VALUE = new Money(42);
        private readonly Money TEST_VALUE_TIMES_3 = new Money(126);
        private readonly int factor;

        protected string name;
        protected Month month;
        protected int year;
        protected Money budget;
        protected BudgetEntry budgetEntry;

        public BudgetEntryTestsBase(int factor) {
            this.factor = factor;
        }

        [SetUp]
        public void SetUp() {
            name = "TestEntry";
            month = Month.January;
            year = 2012;
            budget = TEST_VALUE * factor;

            CreateBudgetEntry();
        }


        protected abstract void CreateBudgetEntry();
        protected abstract Transaction CreateTransaction(int day, Money value);
        protected abstract void AssertType(BudgetEntry entry);

        private void AssertNextMonthsValueIs(Money value) {
            Assert.AreEqual(value, budgetEntry.GetNextMonthsValue());
        }

        private void AssertNextMonthsValueIsSameAsBudgetValue() {
            AssertNextMonthsValueIs(budget);
        }

        private Transaction CreateSmallTestTransaction() {
            return CreateTransaction(1, TEST_VALUE_SMALL);
        }

        private Transaction CreateTestTransaction() {
            return CreateTransaction(1, TEST_VALUE);
        }

        [Test]
        public void ABudgetEntryWithoutTransactionsShouldHaveANextMonthsValueOfItsBudgetValue() {
            AssertNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void ABudgetEntryWithTransactionsOfLessThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            budgetEntry = budgetEntry.AddTransaction(CreateSmallTestTransaction());
            AssertNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void ABudgetEntryWithTransactionsOfSameThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            budgetEntry = budgetEntry.AddTransaction(CreateTestTransaction());
            AssertNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void ABudgetEntryWithTransactionsOfMoreThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            budgetEntry = budgetEntry
                .AddTransaction(CreateTestTransaction())
                .AddTransaction(CreateTestTransaction())
                .AddTransaction(CreateTestTransaction());

            AssertNextMonthsValueIs(factor * TEST_VALUE_TIMES_3);
        }

        [Test]
        public void AClosedPositiveBudgetEntryWithNoTransactionsShouldHaveANextMonthsValueOf0() {
            budgetEntry = budgetEntry.Close();

            AssertNextMonthsValueIs(Money.ZERO);
        }

        [Test]
        public void AClosedPositiveBudgetEntryWithLessThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            budgetEntry = budgetEntry
                .AddTransaction(CreateSmallTestTransaction())
                .Close();

            AssertNextMonthsValueIs(factor * TEST_VALUE_SMALL);
        }

        [Test]
        public void AClosedPositiveBudgetEntryWithTransactionsOfSameThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            budgetEntry = budgetEntry
                .AddTransaction(CreateTestTransaction())
                .Close();

            AssertNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void AClosedPositiveBudgetEntryWithTransactionsOfMoreThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            budgetEntry = budgetEntry
                .AddTransaction(CreateTestTransaction())
                .AddTransaction(CreateTestTransaction())
                .AddTransaction(CreateTestTransaction())
                .Close();

            AssertNextMonthsValueIs(factor * TEST_VALUE_TIMES_3);
        }

        [Test]
        public void AClosedPositiveBudgetEntryShouldNotAcceptAnyFurtherTransactions() {
            budgetEntry = budgetEntry.Close();
            Assert.Throws<BudgetIsClosedException>(() => budgetEntry.AddTransaction(CreateTestTransaction()));
        }

        [Test]
        public void ABudgetEntryCanBeCopied() {
            BudgetEntry newEntry = budgetEntry.CopyTo(budgetEntry.Month + 1, year);

            AssertType(newEntry);
            Assert.AreEqual(budget, newEntry.Budget);
            Assert.AreEqual(name, newEntry.Name);
            Assert.AreEqual(month + 1, newEntry.Month);
            Assert.AreEqual(year, newEntry.Year);
        }
    }
}


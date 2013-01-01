using NUnit.Framework;

namespace PlanAhead {
    [TestFixture]
    public class BudgetEntryTests {
        string name;
        Month month;
        int year;
        Money budget;
        BudgetEntry positiveBudgetEntry, negativeBudgetEntry;

        private void CreateBudgetEntry() {
            positiveBudgetEntry = new PositiveBudgetEntry(name, month, year, budget);
            negativeBudgetEntry = new NegativeBudgetEntry(name, month, year, budget * -1);
        }

        [SetUp]
        public void SetUp() {
            name = "TestEntry";
            month = Month.January;
            year = 2012;
            budget = new Money(42);

            CreateBudgetEntry();
        }

        private void AssertPositiveNextMonthsValueIsSameAsBudgetValue() {
            Money nextMonthsValue = positiveBudgetEntry.GetNextMonthsValue();
            Assert.AreEqual(budget, nextMonthsValue);
        }

        private void AssertNegativeNextMonthsValueIsSameAsBudgetValue() {
            Money nextMonthsValue = negativeBudgetEntry.GetNextMonthsValue();
            Assert.AreEqual(budget * -1, nextMonthsValue);
        }

        [Test]
        public void APositiveBudgetEntryWithoutTransactionsShouldHaveANextMonthsValueOfItsBudgetValue() {
            AssertPositiveNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void ANegativeBudgetEntryWithoutTransactionsShouldHaveANextMonthsValueOfItsBudgetValue() {
            AssertNegativeNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void APositiveBudgetEntryWithTransactionsOfLessThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            var transaction = new DepositTransaction(1, new Money(20));
            positiveBudgetEntry = positiveBudgetEntry.AddTransaction(transaction);

            AssertPositiveNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void ANegativeBudgetEntryWithTransactionsOfLessThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            var transaction = new WithdrawTransaction(1, new Money(20));
            negativeBudgetEntry = negativeBudgetEntry.AddTransaction(transaction);

            AssertPositiveNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void APositiveBudgetEntryWithTransactionsOfSameThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            var transaction = new DepositTransaction(1, budget);
            positiveBudgetEntry = positiveBudgetEntry.AddTransaction(transaction);

            AssertPositiveNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void ANegativeBudgetEntryWithTransactionsOfSameThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            var transaction = new WithdrawTransaction(1, budget);
            negativeBudgetEntry = negativeBudgetEntry.AddTransaction(transaction);

            AssertNegativeNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void APositiveBudgetEntryWithTransactionsOfMoreThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            var transaction1 = new DepositTransaction(1, budget);
            var transaction2 = new DepositTransaction(2, budget);
            var transaction3 = new DepositTransaction(3, budget);

            positiveBudgetEntry = positiveBudgetEntry
                .AddTransaction(transaction1)
                .AddTransaction(transaction2)
                .AddTransaction(transaction3);

            Money nextMonthsValue = positiveBudgetEntry.GetNextMonthsValue();
            Assert.AreEqual(3 * budget, nextMonthsValue);
            Assert.AreNotSame(budget, nextMonthsValue);
        }

        [Test]
        public void ANegativeBudgetEntryWithTransactionsOfLessThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            var transaction1 = new WithdrawTransaction(1, budget);
            var transaction2 = new WithdrawTransaction(2, budget);
            var transaction3 = new WithdrawTransaction(3, budget);

            negativeBudgetEntry = negativeBudgetEntry
                .AddTransaction(transaction1)
                .AddTransaction(transaction2)
                .AddTransaction(transaction3);

            Money nextMonthsValue = negativeBudgetEntry.GetNextMonthsValue();
            Assert.AreEqual(- 3 * budget, nextMonthsValue);
            Assert.AreNotSame(budget, nextMonthsValue);
        }

        [Test]
        public void AClosedPositiveBudgetEntryWithNoTransactionsShouldHaveANextMonthsValueOf0() {
            positiveBudgetEntry = positiveBudgetEntry.Close();

            Money nextMonthsValue = positiveBudgetEntry.GetNextMonthsValue();
            Assert.AreEqual(Money.ZERO, nextMonthsValue);
        }

        [Test]
        public void AClosedNegativeBudgetEntryWithNoTransactionsShouldHaveANextMonthsValueOf0() {
            negativeBudgetEntry = negativeBudgetEntry.Close();

            Money nextMonthsValue = negativeBudgetEntry.GetNextMonthsValue();
            Assert.AreEqual(Money.ZERO, nextMonthsValue);
        }

        [Test]
        public void AClosedPositiveBudgetEntryWithLessThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            var transaction = new DepositTransaction(1, new Money(20));
            positiveBudgetEntry = positiveBudgetEntry
                .AddTransaction(transaction)
                .Close();

            Money nextMonthsValue = positiveBudgetEntry.GetNextMonthsValue();
            Assert.AreEqual(new Money(20), nextMonthsValue);
        }

        [Test]
        public void AClosedNegativeBudgetEntryWithMoreThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            var transaction = new WithdrawTransaction(1, new Money(20));
            negativeBudgetEntry = negativeBudgetEntry
                .AddTransaction(transaction)
                .Close();

            Money nextMonthsValue = negativeBudgetEntry.GetNextMonthsValue();
            Assert.AreEqual(new Money(-20), nextMonthsValue);
        }

        [Test]
        public void AClosedPositiveBudgetEntryWithTransactionsOfSameThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            var transaction = new DepositTransaction(1, budget);
            positiveBudgetEntry = positiveBudgetEntry
                .AddTransaction(transaction)
                .Close();

            AssertPositiveNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void AClosedNegativeBudgetEntryWithTransactionsOfSameThanBudgetShouldHaveANextMonthsValueOfItsBudgetValue() {
            var transaction = new WithdrawTransaction(1, budget);
            negativeBudgetEntry = negativeBudgetEntry
                .AddTransaction(transaction)
                .Close();

            AssertNegativeNextMonthsValueIsSameAsBudgetValue();
        }

        [Test]
        public void AClosedPositiveBudgetEntryWithTransactionsOfMoreThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            var transaction1 = new DepositTransaction(1, budget);
            var transaction2 = new DepositTransaction(2, budget);
            var transaction3 = new DepositTransaction(3, budget);

            positiveBudgetEntry = positiveBudgetEntry
                .AddTransaction(transaction1)
                .AddTransaction(transaction2)
                .AddTransaction(transaction3)
                .Close();

            Money nextMonthsValue = positiveBudgetEntry.GetNextMonthsValue();
            Assert.AreEqual(3 * budget, nextMonthsValue);
            Assert.AreNotSame(budget, nextMonthsValue);
        }

        [Test]
        public void AClosedNegativeBudgetEntryWithTransactionsOfLessThanBudgetShouldHaveANextMonthsValueOfTheSumOfItsTransactions() {
            var transaction1 = new WithdrawTransaction(1, budget);
            var transaction2 = new WithdrawTransaction(2, budget);
            var transaction3 = new WithdrawTransaction(3, budget);

            negativeBudgetEntry = negativeBudgetEntry
                .AddTransaction(transaction1)
                .AddTransaction(transaction2)
                .AddTransaction(transaction3)
                .Close();

            Money nextMonthsValue = negativeBudgetEntry.GetNextMonthsValue();
            Assert.AreEqual(-3 * budget, nextMonthsValue);
            Assert.AreNotSame(budget, nextMonthsValue);
        }

        [Test]
        public void AClosedPositiveBudgetEntryShouldNotAcceptAnyFurtherTransactions() {
            positiveBudgetEntry = positiveBudgetEntry.Close();
            var transaction = new WithdrawTransaction(1, budget);
            Assert.Throws<BudgetIsClosedException>(() => positiveBudgetEntry.AddTransaction(transaction));
        }

        [Test]
        public void AClosedNegativeBudgetEntryShouldNotAcceptAnyFurtherTransactions() {
            negativeBudgetEntry = negativeBudgetEntry.Close();
            var transaction = new WithdrawTransaction(1, budget);
            Assert.Throws<BudgetIsClosedException>(() => negativeBudgetEntry.AddTransaction(transaction));
        }

        [Test]
        public void ABudgetEntryCanBeCopied() {
            BudgetEntry newEntry = positiveBudgetEntry.CopyTo(positiveBudgetEntry.Month + 1, year);

            Assert.AreEqual(budget, newEntry.Budget);
            Assert.AreEqual(name, newEntry.Name);
            Assert.AreEqual(month + 1, newEntry.Month);
            Assert.AreEqual(year, newEntry.Year);
        }
    }
}


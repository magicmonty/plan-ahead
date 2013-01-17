using NUnit.Framework;
using System;

namespace PlanAhead {

    [TestFixture]
    public class FinancialTransactionsTests {

        private static readonly DateTime TEST_DATE_JANUARY_2012 = new DateTime(2012, 1, 20);
        private static readonly DateTime TEST_DATE_FEBRUARY_2012 = new DateTime(2012, 2, 20);
        private static readonly DateTime TEST_DATE_JANUARY_2013 = new DateTime(2013, 1, 20);

        private FinancialTransactions transactions;

        [SetUp]
        public void SetUp() {
            transactions = new FinancialTransactions();
        }

        private void deposit(int value) {
            transactions.Add(new DepositTransaction(DateTime.Today, new Money(value)));
        }

        private void deposit(DateTime date, int value) {
            transactions.Add(new DepositTransaction(date, new Money(value)));
        }

        private void withdraw(int value) {
            transactions.Add(new WithdrawTransaction(DateTime.Today, new Money(value)));
        }

        private void withdraw(DateTime date, int value) {
            transactions.Add(new WithdrawTransaction(date, new Money(value)));
        }

        [Test]
        public void AnEmptyTransactionsContainerShouldReturnAZeroMoneyValue() {
            Assert.AreEqual(Money.ZERO, transactions.GetTotal());
        }

        [Test]
        public void ATransactionsContainerWithASingleDepositTransactionShouldReturnTheValueOfTheTransaction() {
            deposit(42);
            Assert.AreEqual(new Money(42), transactions.GetTotal());
        }

        [Test]
        public void ATransactionsContainerWithMultipleDepositTransactionShouldReturnTheSumOfTheValuesOfTheTransactions() {
            deposit(42);
            deposit(20);
            deposit(38);

            Assert.AreEqual(new Money(42 + 20 + 38), transactions.GetTotal());
        }

        [Test]
        public void ATransactionsContainerWithASingleWithdrawTransactionsShouldReturnTheValueOfTheNegativeOfTheTransaction() {
            withdraw(42);
            Assert.AreEqual(new Money(-42), transactions.GetTotal());
        }

        [Test]
        public void ATransactionsContainerWithMultipleWithdrawTransactionsShouldReturnTheNegativeOfTheSumOfTheValuesOfTheTransactions() {
            withdraw(42);
            withdraw(20);
            withdraw(38);

            Assert.AreEqual(new Money(-42 - 20 - 38), transactions.GetTotal());
        }

        [Test]
        public void ATransactionsContainerWithDifferentTransactionsShouldReturnTheAppropriateSum() {
            deposit(42);
            deposit(20);
            withdraw(38);

            Assert.AreEqual(new Money(42 + 20 - 38), transactions.GetTotal());
        }

        [Test]
        public void TestGetValueForSpecificMonthAndYear() {
            deposit(TEST_DATE_JANUARY_2012, 42);
            deposit(TEST_DATE_JANUARY_2013, 20);
            deposit(TEST_DATE_FEBRUARY_2012, 38);

            Assert.AreEqual(new Money(42 + 20 + 38), transactions.GetTotal());
            Assert.AreEqual(new Money(42), transactions.GetValueOf(Month.January, 2012));
            Assert.AreEqual(new Money(20), transactions.GetValueOf(Month.January, 2013));
            Assert.AreEqual(new Money(38), transactions.GetValueOf(Month.February, 2012));
        }

        [Test]
        public void TestGetValueForSpecificYear() {
            deposit(TEST_DATE_JANUARY_2012, 42);
            deposit(TEST_DATE_JANUARY_2013, 20);
            deposit(TEST_DATE_FEBRUARY_2012, 38);

            Assert.AreEqual(new Money(42 + 20 + 38), transactions.GetTotal());
            Assert.AreEqual(new Money(42 + 38), transactions.GetValueOf(2012));
            Assert.AreEqual(new Money(20), transactions.GetValueOf(2013));
        }

        [Test]
        public void TestGetValueUntil() {
            deposit(TEST_DATE_JANUARY_2012, 42);
            deposit(TEST_DATE_FEBRUARY_2012, 38);
            deposit(TEST_DATE_JANUARY_2013, 20);

            Assert.AreEqual(new Money(42 + 20 + 38), transactions.GetTotal());
            Assert.AreEqual(new Money(42), transactions.GetValueUntil(Month.January, 2012));
            Assert.AreEqual(new Money(42 + 38), transactions.GetValueUntil(Month.February, 2012));
            Assert.AreEqual(new Money(42 + 38), transactions.GetValueUntil(Month.March, 2012));
            Assert.AreEqual(new Money(42 + 38 + 20), transactions.GetValueUntil(Month.March, 2013));
        }
    }
}


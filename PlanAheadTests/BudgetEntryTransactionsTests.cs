using NUnit.Framework;
using System;

namespace PlanAhead {

    [TestFixture]
    public class BudgetEntryTransactionsTests {

        [Test]
        public void AnEmptyTransactionsContainerShouldReturnAZeroMoneyValue() {
            var transactions = new BudgetEntryTransactions();
            Assert.AreEqual(Money.ZERO, transactions.GetValue());
        }

        [Test]
        public void ATransactionsContainerWithASingleDepositTransactionShouldReturnTheValueOfTheTransaction() {
            var transactions = new BudgetEntryTransactions();
            var transaction = new DepositTransaction(DateTime.Today, new Money(42));
            transactions.Add(transaction);
            Assert.AreEqual(transaction.Value, transactions.GetValue());
        }

        [Test]
        public void ATransactionsContainerWithMultipleDepositTransactionShouldReturnTheSumOfTheValuesOfTheTransactions() {
            var transactions = new BudgetEntryTransactions();
            transactions.Add(new DepositTransaction(DateTime.Today, new Money(42)));
            transactions.Add(new DepositTransaction(DateTime.Today, new Money(20)));
            transactions.Add(new DepositTransaction(DateTime.Today, new Money(38)));

            Assert.AreEqual(new Money(42 + 20 + 38), transactions.GetValue());
        }

        [Test]
        public void ATransactionsContainerWithASingleWithdrawTransactionsShouldReturnTheValueOfTheNegativeOfTheTransaction() {
            var transactions = new BudgetEntryTransactions();
            var transaction = new WithdrawTransaction(DateTime.Today, new Money(42));
            transactions.Add(transaction);
            Assert.AreEqual(transaction.Value * -1, transactions.GetValue());
        }

        [Test]
        public void ATransactionsContainerWithMultipleWithdrawTransactionsShouldReturnTheNegativeOfTheSumOfTheValuesOfTheTransactions() {
            var transactions = new BudgetEntryTransactions();
            transactions.Add(new WithdrawTransaction(DateTime.Today, new Money(42)));
            transactions.Add(new WithdrawTransaction(DateTime.Today, new Money(20)));
            transactions.Add(new WithdrawTransaction(DateTime.Today, new Money(38)));

            Assert.AreEqual(new Money(-42 - 20 - 38), transactions.GetValue());
        }

        [Test]
        public void ATransactionsContainerWithDifferentTransactionsShouldReturnTheAppropriateSum() {
            var transactions = new BudgetEntryTransactions();
            transactions.Add(new DepositTransaction(DateTime.Today, new Money(42)));
            transactions.Add(new DepositTransaction(DateTime.Today, new Money(20)));
            transactions.Add(new WithdrawTransaction(DateTime.Today, new Money(38)));

            Assert.AreEqual(new Money(42 + 20 - 38), transactions.GetValue());
        }

    }
}


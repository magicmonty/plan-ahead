using NUnit.Framework;

namespace PlanAhead {

    [TestFixture]
    public class TransactionsTests {

        [Test]
        public void AnEmptyTransactionsContainerShouldReturnAZeroMoneyValue() {
            var transactions = new Transactions();
            Assert.AreEqual(Money.ZERO, transactions.GetValue());
        }

        [Test]
        public void ATransactionsContainerWithASingleDepositTransactionShouldReturnTheValueOfTheTransaction() {
            var transactions = new Transactions();
            var transaction = new DepositTransaction(1, new Money(42));
            transactions.Add(transaction);
            Assert.AreEqual(transaction.Value, transactions.GetValue());
        }

        [Test]
        public void ATransactionsContainerWithMultipleDepositTransactionShouldReturnTheSumOfTheValuesOfTheTransactions() {
            var transactions = new Transactions();
            transactions.Add(new DepositTransaction(1, new Money(42)));
            transactions.Add(new DepositTransaction(1, new Money(20)));
            transactions.Add(new DepositTransaction(1, new Money(38)));

            Assert.AreEqual(new Money(42 + 20 + 38), transactions.GetValue());
        }

        [Test]
        public void ATransactionsContainerWithASingleWithdrawTransactionsShouldReturnTheValueOfTheNegativeOfTheTransaction() {
            var transactions = new Transactions();
            var transaction = new WithdrawTransaction(1, new Money(42));
            transactions.Add(transaction);
            Assert.AreEqual(transaction.Value * -1, transactions.GetValue());
        }

        [Test]
        public void ATransactionsContainerWithMultipleWithdrawTransactionsShouldReturnTheNegativeOfTheSumOfTheValuesOfTheTransactions() {
            var transactions = new Transactions();
            transactions.Add(new WithdrawTransaction(1, new Money(42)));
            transactions.Add(new WithdrawTransaction(1, new Money(20)));
            transactions.Add(new WithdrawTransaction(1, new Money(38)));

            Assert.AreEqual(new Money(-42 - 20 - 38), transactions.GetValue());
        }

        [Test]
        public void ATransactionsContainerWithDifferentTransactionsShouldReturnTheAppropriateSum() {
            var transactions = new Transactions();
            transactions.Add(new DepositTransaction(1, new Money(42)));
            transactions.Add(new DepositTransaction(1, new Money(20)));
            transactions.Add(new WithdrawTransaction(1, new Money(38)));

            Assert.AreEqual(new Money(42 + 20 - 38), transactions.GetValue());
        }

    }
}


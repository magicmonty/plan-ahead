using NUnit.Framework;
using System;

namespace PlanAhead {

    [TestFixture]
    public class BudgetEntryTransactionTests {

        [Test]
        public void AppliedWithdrawTransactionsShouldSubtractMoney() {
            var transaction = new WithdrawTransaction(DateTime.Today, new Money(20));
            Assert.AreEqual(new Money(10), transaction.apply(new Money(30)));
        }

        [Test]
        public void AppliedDepositTransactionShouldAddMoney() {
            var transaction = new DepositTransaction(DateTime.Today, new Money(20));
            Assert.AreEqual(new Money(50), transaction.apply(new Money(30)));
        }

        [Test]
        public void AWithdrawTransactionShouldBeCloneable() {
            var transaction = new WithdrawTransaction(DateTime.Today, new Money(42));
            var cloned = transaction.Clone();
            Assert.IsInstanceOf(typeof(WithdrawTransaction), cloned, "Should be of type WithdrawTransaction");
            Assert.AreEqual(transaction.Date, cloned.Date, "Day should match");
            Assert.AreEqual(transaction.Value, cloned.Value, "Value should match");
        }

        [Test]
        public void ADepositTransactionShouldBeCloneable() {
            var transaction = new DepositTransaction(DateTime.Today, new Money(42));
            var cloned = transaction.Clone();
            Assert.IsInstanceOf(typeof(DepositTransaction), cloned, "Should be of type WithdrawTransaction");
            Assert.AreEqual(transaction.Date, cloned.Date, "Day should match");
            Assert.AreEqual(transaction.Value, cloned.Value, "Value should match");
        }
    }
}


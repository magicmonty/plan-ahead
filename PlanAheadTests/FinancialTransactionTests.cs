using NUnit.Framework;
using System;

namespace PlanAhead {

    public abstract class FinancialTransactionTestsBase {
        protected abstract FinancialTransaction CreateTransaction(DateTime date, Money value);
        protected abstract FinancialTransaction CreateTransaction();
        protected abstract void AssertType(FinancialTransaction transaction);

        [Test]
        public void ATransactionShouldBeCloneable() {
            var transaction = CreateTransaction(DateTime.Today, new Money(42));
            var cloned = transaction.Clone();
            AssertType(cloned);
            Assert.AreEqual(transaction.Date, cloned.Date, "Day should match");
            Assert.AreEqual(transaction.Value, cloned.Value, "Value should match");
        }

        [Test]
        public void ATransactionShouldBeClonedOnSetDate() {
            var transaction = CreateTransaction();
            var newTransaction = transaction.SetDate(new DateTime(2000, 1, 1));
            AssertType(newTransaction);
            Assert.AreNotSame(transaction, newTransaction);
            Assert.That(newTransaction.Date, Is.EqualTo(new DateTime(2000, 1, 1)));
        }

        [Test]
        public void ATransactionShouldBeClonedOnSetValue() {
            var transaction = CreateTransaction();
            var newTransaction = transaction.SetValue(new Money(50));
            AssertType(newTransaction);
            Assert.AreNotSame(transaction, newTransaction);
            Assert.That(newTransaction.Value, Is.EqualTo(new Money(50)));
        }

        [Test]
        public void ATransactionShouldBeInitializedWithMoney0AndDateToday() {
            var transaction = CreateTransaction();
            Assert.That(transaction.Date, Is.EqualTo(DateTime.Today));
            Assert.That(transaction.Value, Is.EqualTo(Money.ZERO));
        }        
    }

    [TestFixture]
    public class WithdrawTransactionTests: FinancialTransactionTestsBase {
        protected override void AssertType(FinancialTransaction transaction) {
            Assert.IsInstanceOf(typeof(WithdrawTransaction), transaction, "Should be of type WithdrawTransaction");
        }

        protected override FinancialTransaction CreateTransaction(DateTime date, Money value) {
            return new WithdrawTransaction(date, value);
        }
        
        protected override FinancialTransaction CreateTransaction() {
            return new WithdrawTransaction();
        }
        
        [Test]
        public void AppliedTransactionShouldSubtractMoney() {
            var transaction = CreateTransaction(DateTime.Today, new Money(20));
            Assert.AreEqual(new Money(10), transaction.apply(new Money(30)));
        }
        
    }

    [TestFixture]
    public class DepositTransactionTests: FinancialTransactionTestsBase {
        protected override void AssertType(FinancialTransaction transaction) {
            Assert.IsInstanceOf(typeof(DepositTransaction), transaction, "Should be of type WithdrawTransaction");
        }
        
        protected override FinancialTransaction CreateTransaction(DateTime date, Money value) {
            return new DepositTransaction(date, value);
        }

        protected override FinancialTransaction CreateTransaction() {
            return new DepositTransaction();
        }
        
        [Test]
        public void AppliedTransactionShouldAddMoney() {
            var transaction = CreateTransaction(DateTime.Today, new Money(20));
            Assert.AreEqual(new Money(50), transaction.apply(new Money(30)));
        }
    }
}


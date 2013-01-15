using NUnit.Framework;
using System;

namespace PlanAhead {

    [TestFixture]
    public class AccountTests {
        private Account account;

        [SetUp]
        public void SetUp() {
            Money initialBalance = new Money(100);
            DateTime initialDate = new DateTime(2012, 01, 01);
            account = new Account(initialDate, initialBalance);
        }

        private void AssertBalance(int moneyValue) {
            Assert.That(account.GetBalance(), Is.EqualTo(new Money(moneyValue)));
        }

        [Test]
        public void AnAccountShouldHaveABalance() {
            AssertBalance(100);
        }

        [Test]
        public void WithdrawShouldDecreaseBalance() {
            account = account.Withdraw(DateTime.Today, new Money(10));
            AssertBalance(90);
        }

        [Test]
        public void DepositShouldIncreaseBalance() {
            account = account.Deposit(DateTime.Today, new Money(10));
            AssertBalance(110);
        }
    }
}


using NUnit.Framework;
using System;

namespace PlanAhead {

    [TestFixture]
    public class AccountTests {
        private Account account;

        [SetUp]
        public void SetUp() {
            Money initialBalance = new Money(100);
            DateTime initialDate = new DateTime(2012, 1, 1);
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

        [Test]
        public void ABalanceForASpecificMonthShouldOnlyReturnTheSumOfTransactionsUpToThisMonth() {
            account = account
                .Deposit(new DateTime(2012, 2, 1), new Money(20))
                .Withdraw(new DateTime(2012, 3, 1), new Money(30))
                .Deposit(new DateTime(2012, 4, 1), new Money(40))
                .Deposit(new DateTime(2012, 5, 1), new Money(50));

            Assert.That(account.GetBalance(Month.January, 2010), Is.EqualTo(Money.ZERO), "2010");
            Assert.That(account.GetBalance(Month.January, 2012), Is.EqualTo(new Money(100)), "January");
            Assert.That(account.GetBalance(Month.February, 2012), Is.EqualTo(new Money(100 + 20)), "February");
            Assert.That(account.GetBalance(Month.March, 2012), Is.EqualTo(new Money(100 + 20 - 30)), "March");
            Assert.That(account.GetBalance(Month.April, 2012), Is.EqualTo(new Money(100 + 20 - 30 + 40)), "April");
            Assert.That(account.GetBalance(Month.May, 2012), Is.EqualTo(new Money(100 + 20 - 30 + 40 + 50)), "May");
            Assert.That(account.GetBalance(Month.June, 2012), Is.EqualTo(new Money(100 + 20 - 30 + 40 + 50)), "June");
        }

        [Test]
        public void AValueForASpecificMonthShouldOnlyReturnTheSumOfTransactionsOfThisMonth() {
            account = account
                .Deposit(new DateTime(2012, 2, 1), new Money(20))
                    .Withdraw(new DateTime(2012, 3, 1), new Money(30))
                    .Deposit(new DateTime(2012, 4, 1), new Money(40))
                    .Deposit(new DateTime(2012, 5, 1), new Money(50));
            
            Assert.That(account.GetValueOf(Month.January, 2010), Is.EqualTo(Money.ZERO), "2010");
            Assert.That(account.GetValueOf(Month.January, 2012), Is.EqualTo(new Money(100)), "January");
            Assert.That(account.GetValueOf(Month.February, 2012), Is.EqualTo(new Money(20)), "February");
            Assert.That(account.GetValueOf(Month.March, 2012), Is.EqualTo(new Money(-30)), "March");
            Assert.That(account.GetValueOf(Month.April, 2012), Is.EqualTo(new Money(40)), "April");
            Assert.That(account.GetValueOf(Month.May, 2012), Is.EqualTo(new Money(50)), "May");
            Assert.That(account.GetValueOf(Month.June, 2012), Is.EqualTo(Money.ZERO), "June");
        }
    }
}


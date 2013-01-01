using NUnit.Framework;

namespace PlanAhead {
    [TestFixture]
    public class MoneyTests {

        [Test]
        public void MoneyWithEqualAmountShouldBeEqual() {
            Money money1 = new Money(100);
            Money money2 = new Money(100);

            Assert.IsTrue(money1.Equals(money2));
        }

        [Test]
        public void MoneyWithDifferentAmountShouldNotBeEqual() {
            Money money1 = new Money(100);
            Money money2 = new Money(200);

            Assert.IsFalse(money1.Equals(money2));
        }

        [Test]
        public void MoneyCanBeCloned() {
            Money money1 = new Money(42);
            Money money2 = money1.Clone();

            Assert.AreEqual(money1, money2);
            Assert.AreNotSame(money1, money2);
        }

        [Test]
        public void MoneyCanBeMultiplied() {
            Money money = new Money(2);
            Assert.AreEqual(3 * 2, money.MultiplyBy(3).Value);
 
            money = new Money(42);
            Assert.AreEqual(42 * 42, money.MultiplyBy(42).Value);
        }

        [Test]
        public void MoneyCanBeMultipliedWithOperator() {
            Money money = new Money(2);
            Assert.AreEqual(new Money(3 * 2), 3 * money);
            Assert.AreEqual(new Money(3 * 2), money * 3);
 
            money = new Money(42);
            Assert.AreEqual(new Money(42 * 42), 42 * money);
            Assert.AreEqual(new Money(42 * 42), money * 42);
        }

        [Test]
        public void MoneyCanBeAdded() {
            Money money1 = new Money(2);
            Money money2 = new Money(5);
            Money money3 = money1.Add(money2);

            Assert.AreEqual(new Money(7), money3);
            Assert.AreNotSame(money1, money3);
            Assert.AreNotSame(money2, money3);
        }

        [Test]
        public void MoneyCanBeAddedWithOperator() {
            Money money1 = new Money(2);
            Money money2 = new Money(5);
            Money money3 = money1 + money2;

            Assert.AreEqual(new Money(7), money3);
            Assert.AreNotSame(money1, money3);
            Assert.AreNotSame(money2, money3);
        }

        [Test]
        public void MoneyCanBeSubtracted() {
            Money money1 = new Money(5);
            Money money2 = new Money(3);
            Money money3 = money1.Subtract(money2);

            Assert.AreEqual(new Money(2), money3);
            Assert.AreNotSame(money1, money3);
            Assert.AreNotSame(money2, money3);
        }

        [Test]
        public void MoneyCanBeSubtractedWithOperator() {
            Money money1 = new Money(5);
            Money money2 = new Money(3);
            Money money3 = money1 - money2;

            Assert.AreEqual(new Money(2), money3);
            Assert.AreNotSame(money1, money3);
            Assert.AreNotSame(money2, money3);
        }

        [Test]
        public void MoneyCanBeComparedForGreaterThan() {
            Money money1 = new Money(2);
            Money money2 = new Money(5);

            Assert.IsTrue(money2 > money1);
            Assert.IsFalse(money1 > money2);
        }

        [Test]
        public void MoneyCanBeComparedForLessThan() {
            Money money1 = new Money(2);
            Money money2 = new Money(5);

            Assert.IsTrue(money1 < money2);
            Assert.IsFalse(money2 < money1);
        }

        [Test]
        public void MoneyCanBeComparedForEquality() {
            Money money1 = new Money(2);
            Money money2 = new Money(5);
            Money money3 = new Money(5);

            Assert.IsTrue(money2 == money3);
            Assert.AreNotSame(money2, money3);
            Assert.IsFalse(money1 == money2);
        }

        [Test]
        public void MoneyCanBeComparedForNotEquality() {
            Money money1 = new Money(2);
            Money money2 = new Money(5);
            Money money3 = new Money(5);

            Assert.IsTrue(money1 != money3);
            Assert.IsTrue(money1 != money2);
            Assert.IsFalse(money2 != money3);
        }

        [Test]
        public void MoneyCanBeComparedForGreaterOrEquality() {
            Money money1 = new Money(2);
            Money money2 = new Money(5);
            Money money3 = new Money(5);

            Assert.IsTrue(money2 >= money3);
            Assert.IsTrue(money3 >= money1);
        }

        [Test]
        public void MoneyCanBeComparedForLessOrEquality() {
            Money money1 = new Money(2);
            Money money2 = new Money(5);
            Money money3 = new Money(5);

            Assert.IsTrue(money2 <= money3);
            Assert.IsTrue(money1 <= money3);
        }
    }
}


using NUnit.Framework;
using System;

namespace PlanAhead {

    [TestFixture]
    public class MonthTests {
        [Test]
        public void AMonthShouldBeConvertedImplicitelyToAnInt() {
            Assert.That(1 == Month.January);
            Assert.That(2 == Month.February);
            Assert.That(3 == Month.March);
            Assert.That(4 == Month.April);
            Assert.That(5 == Month.May);
            Assert.That(6 == Month.June);
            Assert.That(7 == Month.July);
            Assert.That(8 == Month.August);
            Assert.That(9 == Month.September);
            Assert.That(10 == Month.October);
            Assert.That(11 == Month.November);
            Assert.That(12 == Month.December);
        }

        private void AssertMonth(int value) {
            Month month = value;
            Assert.That(value == month.Value);
        }

        [Test]
        public void AValidIntShouldBeConvertedImplicitelyToAMonth() {
            for (int i = 1; i<=12; i++) {
                AssertMonth(i);
            }
        }

        [Test]
        public void AnInvalidIntShouldThrowAnException() {
            Assert.Throws<TypeInitializationException>(() => {
                Month month = 0;
                Assert.IsNull(month); 
            });

            Assert.Throws<TypeInitializationException>(() => {
                Month month = 13;
                Assert.IsNull(month);
            });
        }

        [Test]
        public void MonthsCanBeComparedByEquals() {
            Month month = 1;

            Assert.That(Month.January.Equals(month));
        }

        [Test]
        public void MonthsCanBeComparedForEqualityWithOperator() {
            Month month = 1;
            
            Assert.That(Month.January == month);
        }

        [Test]
        public void MonthsCanBeComparedForInEqualityWithOperator() {
            Month month = 2;
            
            Assert.That(Month.January != month);
        }
    }
}


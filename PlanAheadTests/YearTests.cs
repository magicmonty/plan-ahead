using NUnit.Framework;

namespace PlanAhead {

    [TestFixture]
    public class YearTests {
        private readonly Year year1 = 2012;
        private readonly Year year2 = 2012;
        private readonly Year year3 = 2013;
        private readonly Year year4 = 2013;

        [Test]
        public void AYearShouldBeConvertedImplicitelyToAnInt() {
            int year = new Year(2012);
            Assert.That(2012 == year);
        }
        
        [Test]
        public void AnIntShouldBeConvertedImplicitelyToAYear() {
            Year year = 2012;
            Assert.That(2012 == year.Value);
        }

        [Test]
        public void YearShouldHaveAValue() {
            Assert.That(year1.Value, Is.EqualTo(2012));
        }

        [Test]
        public void YearShouldBeComparableByEqualTo() {
            Assert.That(year1, Is.EqualTo(year2));
        }

        [Test]
        public void YearShouldBeComparableByEqualsOperator() {
            Assert.That(year1 == year2);
        }

        [Test]
        public void YearShouldBeComparableByNotEqualsOperator() {
            Assert.That(year1 != year3);
        }

        [Test]
        public void YearShouldBeComparableByGreaterThanOperator() {
            Assert.That(year3 > year1);
        }

        [Test]
        public void YearShouldBeComparableByLessThanOperator() {
            Assert.That(year1 < year3);
        }

        [Test]
        public void YearShouldBeComparableByGreaterOrEqualOperator() {
            Assert.That(year3 >= year1);
            Assert.That(year3 >= year4);
        }
        
        [Test]
        public void YearShouldBeComparableByLessOrEqualOperator() {
            Assert.That(year1 <= year3);
            Assert.That(year3 <= year4);
        }

        [Test]
        public void YearShouldBeComparableWithInt() {
            Assert.That(year1.Equals(2012));
        }

        [Test]
        public void YearShouldBeComparableWithIntByOperator() {
            Assert.That(year1 == 2012);
            Assert.That(year1 != 2013);
            Assert.That(year1 < 2013);
            Assert.That(year1 > 2010);
            Assert.That(year1 <= 2013);
            Assert.That(year1 <= 2012);
            Assert.That(year1 >= 2010);
            Assert.That(year1 >= 2012);
        }

        [Test]
        public void ToStringShouldOutputTheYear() {
            Assert.That(year1.ToString(), Is.EqualTo("2012"));
            Assert.That(year3.ToString(), Is.EqualTo("2013"));
        }
    }
}


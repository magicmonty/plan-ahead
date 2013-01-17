using NUnit.Framework;

namespace PlanAhead {

    [TestFixture]
    public class YearTests {
        private readonly Year year1 = 2012;
        private readonly Year year2 = 2012;
        private readonly Year year3 = 2013;
        private readonly Year year4 = 2013;

        [Test]
        public void YearShouldHaveAValue() {
            Assert.That(2012, Is.EqualTo(year1.Value));
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
    }
}


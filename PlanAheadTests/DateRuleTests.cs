using NUnit.Framework;

namespace PlanAhead {

    [TestFixture]
    public class DateRuleTests {

        [Test]
        public void ThereShouldBeADateRule() {
            var dateRule = new DateRule();
            Assert.IsNull(dateRule.getDateForMonth(Month.January, 2012));
        }

        [Test]
        public void AFixedDayDateRuleShouldMatchOnTheCorrectMonthAndYear() {
            var dateRule = new FixedDayDateRule(10, Month.January, 2012);
            Assert.AreEqual(10, dateRule.getDateForMonth(Month.January, 2012));
        }       

        [Test]
        public void AFixedDayDateRuleShouldNotMatchOnTheIncorrectMonth() {
            var dateRule = new FixedDayDateRule(10, Month.January, 2012);
            Assert.IsNull(dateRule.getDateForMonth(Month.February, 2012));
        }       

        [Test]
        public void AFixedDayDateRuleShouldNotMatchOnTheIncorrectYear() {
            var dateRule = new FixedDayDateRule(10, Month.January, 2012);
            Assert.IsNull(dateRule.getDateForMonth(Month.January, 2013));
        }       
    }
}


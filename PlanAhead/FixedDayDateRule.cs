namespace PlanAhead {

    public class FixedDayDateRule: DateRule {
        private int day;
        private Month month;
        private int year;

        public FixedDayDateRule(int day, Month month, int year): base() {
            this.day = day;
            this.month = month;
            this.year = year;
        }

        public override int? getDateForMonth(Month month, int year) {
            if (this.year == year && this.month == month) {
                return this.day;
            }

            return base.getDateForMonth(month, year);
        }
    }

}


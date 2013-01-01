using System;

namespace PlanAhead {
    public enum Month {
        January,
        February,
        March,
        April,
        May,
        June,
        Juli,
        August,
        September,
        October,
        November,
        December
    }

    public class DateRule {
        public DateRule() {
        }

        public virtual int? getDateForMonth(Month month, int year) {
            return null;
        }

    }
}


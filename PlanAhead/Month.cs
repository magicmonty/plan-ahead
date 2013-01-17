using System;

namespace PlanAhead {

    public class Month {
        public static readonly Month January = 1;
        public static readonly Month February = 2;
        public static readonly Month March = 3;
        public static readonly Month April = 4;
        public static readonly Month May = 5;
        public static readonly Month June = 6;
        public static readonly Month July = 7;
        public static readonly Month August = 8;
        public static readonly Month September = 9;
        public static readonly Month October = 10;
        public static readonly Month November = 11;
        public static readonly Month December = 12;

        public readonly int Value;

        private Month(int value) {
            this.Value = value;
        }

        public static implicit operator int(Month month) {
            return month.Value;
        }

        public static implicit operator Month(int value) {
            if (value < 1 || value > 12) {
                throw new TypeInitializationException("Month", new Exception(String.Format("Value {0} not allowed", value)));
            }
            return new Month(value);
        }

        public static bool operator ==(Month month1, Month month2) {
            return month1.Value == month2.Value;
        }

        public static bool operator !=(Month month1, Month month2) {
            return month1.Value != month2.Value;
        }
        
        public override bool Equals(object obj) {
            if (obj is Month) {
                return (Month)obj == this;
            }

            if (obj is int) {
                return (int)obj == this.Value;
            }

            return false;
        }
    }
}


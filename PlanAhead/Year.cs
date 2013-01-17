using System;

namespace PlanAhead {
    public class Year {

        public readonly int Value;

        public Year(int value) {
            this.Value = value;
        }

        public override bool Equals(object obj) {
            if (obj is Year) {
                return ((Year)obj).Value == Value;
            }
            return false;
        }

        public override int GetHashCode() {
            return this.Value.GetHashCode();
        }

        public static bool operator ==(Year year1, Year year2) {
            return year1.Value == year2.Value;
        }

        public static bool operator !=(Year year1, Year year2) {
            return !(year1 == year2);
        }       

        public static bool operator >(Year year1, Year year2) {
            return year1.Value > year2.Value;
        }
        
        public static bool operator <(Year year1, Year year2) {
            return !(year1 > year2);
        }       

        public static bool operator >=(Year year1, Year year2) {
            return year1.Value >= year2.Value;
        }
        
        public static bool operator <=(Year year1, Year year2) {
            return year1.Value <= year2.Value;
        }

        public static implicit operator Year(int value) {
          return new Year(value);
        }
    }
}


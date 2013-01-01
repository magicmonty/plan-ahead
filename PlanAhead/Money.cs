namespace PlanAhead {

    public class Money {

        public readonly int Value;

        public static readonly Money ZERO = new Money(0);

        public Money(int value) {
            this.Value = value;
        }

        public override bool Equals(object obj) {
            if (obj is Money) {
                return this.Value.Equals(((Money) obj).Value);
            }

            return false;
        }

        public override int GetHashCode() {
            return this.Value.GetHashCode();
        }

        public override string ToString() {
            return string.Format("{0}", this.Value);
        }

        public Money Clone() {
            return new Money(this.Value);
        }

        public Money MultiplyBy(int multiplier) {
            return new Money(this.Value * multiplier);
        }       

        public Money Add(Money moneyToAdd) {
            return new Money(this.Value + moneyToAdd.Value);
        }

        public Money Subtract(Money moneyToSubtract) {
            return new Money(this.Value - moneyToSubtract.Value);
        }

        public static bool operator >(Money money1, Money money2) {
            return money1.Value > money2.Value;
        }

        public static bool operator <(Money money1, Money money2) {
            return money1.Value < money2.Value;
        }

        public static bool operator ==(Money money1, Money money2) {
            return money1.Value == money2.Value;
        }

        public static bool operator !=(Money money1, Money money2) {
            return money1.Value != money2.Value;
        }

        public static bool operator >=(Money money1, Money money2) {
            return money1.Value >= money2.Value;
        }

        public static bool operator <=(Money money1, Money money2) {
            return money1.Value <= money2.Value;
        }

        public static Money operator +(Money money1, Money money2) {
            return money1.Add(money2);
        }

        public static Money operator -(Money money1, Money money2) {
            return money1.Subtract(money2);
        }

        public static Money operator *(Money money1, int multiplier) {
            return money1.MultiplyBy(multiplier);
        }

        public static Money operator *(int multiplier, Money money1) {
            return money1.MultiplyBy(multiplier);
        }
    }
}


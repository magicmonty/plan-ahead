namespace PlanAhead {

    public abstract class Transaction {
        public readonly Money Value;
        public readonly int Day;

        public Transaction(int day, Money value) {
            this.Value = value.Clone();
            this.Day = day;
        }

        public abstract Transaction Clone();

        public abstract Money apply(Money value);
    }

    public class WithdrawTransaction: Transaction {

        public WithdrawTransaction(int day, Money value): base(day, value) {
        }

        public override Money apply(Money value) {
            return value - this.Value;
        }

        public override Transaction Clone() {
            return new WithdrawTransaction(this.Day, this.Value);
        }
    }

    public class DepositTransaction: Transaction {
        public DepositTransaction(int day, Money value): base(day, value) {
        }

        public override Money apply(Money value) {
            return value + this.Value;
        }

        public override Transaction Clone() {
            return new DepositTransaction(this.Day, this.Value);
        }
    }
}


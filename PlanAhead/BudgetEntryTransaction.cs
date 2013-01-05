namespace PlanAhead {

    public abstract class BudgetEntryTransaction {
        public readonly Money Value;
        public readonly int Day;

        public BudgetEntryTransaction(int day, Money value) {
            this.Value = value.Clone();
            this.Day = day;
        }

        public abstract BudgetEntryTransaction Clone();

        public abstract Money apply(Money value);
    }

    public class WithdrawTransaction: BudgetEntryTransaction {

        public WithdrawTransaction(int day, Money value): base(day, value) {
        }

        public override Money apply(Money value) {
            return value - this.Value;
        }

        public override BudgetEntryTransaction Clone() {
            return new WithdrawTransaction(this.Day, this.Value);
        }
    }

    public class DepositTransaction: BudgetEntryTransaction {
        public DepositTransaction(int day, Money value): base(day, value) {
        }

        public override Money apply(Money value) {
            return value + this.Value;
        }

        public override BudgetEntryTransaction Clone() {
            return new DepositTransaction(this.Day, this.Value);
        }
    }
}


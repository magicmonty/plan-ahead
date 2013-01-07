using System;

namespace PlanAhead {

    public abstract class BudgetEntryTransaction {
        public readonly Money Value;
        public readonly DateTime Date;

        public BudgetEntryTransaction(DateTime date, Money value) {
            this.Value = value.Clone();
            this.Date = date;
        }

        public abstract BudgetEntryTransaction Clone();

        public abstract Money apply(Money value);
    }

    public class WithdrawTransaction: BudgetEntryTransaction {

        public WithdrawTransaction(DateTime date, Money value): base(date, value) {
        }

        public override Money apply(Money value) {
            return value - this.Value;
        }

        public override BudgetEntryTransaction Clone() {
            return new WithdrawTransaction(this.Date, this.Value);
        }
    }

    public class DepositTransaction: BudgetEntryTransaction {
        public DepositTransaction(DateTime date, Money value): base(date, value) {
        }

        public override Money apply(Money value) {
            return value + this.Value;
        }

        public override BudgetEntryTransaction Clone() {
            return new DepositTransaction(this.Date, this.Value);
        }
    }
}


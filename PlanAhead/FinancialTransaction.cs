using System;

namespace PlanAhead {

    public abstract class FinancialTransaction {
        public readonly Money Value;
        public readonly DateTime Date;

        public FinancialTransaction(DateTime date, Money value) {
            this.Value = value.Clone();
            this.Date = date;
        }

        public abstract FinancialTransaction Clone();

        public abstract Money apply(Money value);
    }

    public class WithdrawTransaction: FinancialTransaction {

        public WithdrawTransaction(DateTime date, Money value): base(date, value) {
        }

        public override Money apply(Money value) {
            return value - this.Value;
        }

        public override FinancialTransaction Clone() {
            return new WithdrawTransaction(this.Date, this.Value);
        }
    }

    public class DepositTransaction: FinancialTransaction {
        public DepositTransaction(DateTime date, Money value): base(date, value) {
        }

        public override Money apply(Money value) {
            return value + this.Value;
        }

        public override FinancialTransaction Clone() {
            return new DepositTransaction(this.Date, this.Value);
        }
    }
}


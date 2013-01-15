using System;

namespace PlanAhead {

    public abstract class FinancialTransaction {
        public readonly Money Value;
        public readonly DateTime Date;

        public FinancialTransaction(): this(DateTime.Today, Money.ZERO) {
        }

        public FinancialTransaction(DateTime date, Money value) {
            this.Value = value.Clone();
            this.Date = date;
        }

        public FinancialTransaction Clone() {
            return Create(this.Date, this.Value);
        }

        public FinancialTransaction SetDate(DateTime date) {
            return Create(date, this.Value);
        }

        public FinancialTransaction SetValue(Money value) {
            return Create(this.Date, value);
        }
        
        public abstract Money apply(Money value);
        protected abstract FinancialTransaction Create(DateTime date, Money value);
    }

    public class WithdrawTransaction: FinancialTransaction {

        public WithdrawTransaction(): base() {
        }

        public WithdrawTransaction(DateTime date, Money value): base(date, value) {
        }

        public override Money apply(Money value) {
            return value - this.Value;
        }

        protected override FinancialTransaction Create(DateTime date, Money value) {
            return new WithdrawTransaction(date, value);
        }
    }

    public class DepositTransaction: FinancialTransaction {
        public DepositTransaction(): base() {
        }
        
        public DepositTransaction(DateTime date, Money value): base(date, value) {
        }

        public override Money apply(Money value) {
            return value + this.Value;
        }

        protected override FinancialTransaction Create(DateTime date, Money value) {
            return new DepositTransaction(date, value);
        }
    }
}


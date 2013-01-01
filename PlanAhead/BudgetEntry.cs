using System;

namespace PlanAhead {
    public class BudgetIsClosedException: Exception {
    }

    public abstract class BudgetEntry {
        public readonly string Name;
        public readonly Month Month;
        public readonly int Year;
        public readonly Money Budget;
        public readonly bool IsClosed;

        private Transactions transactions;

        public BudgetEntry(string name, Month month, int year, Money budget) {
            this.Name = name;
            this.Month = month;
            this.Year = year;
            this.Budget = budget.Clone();
            this.IsClosed = false;

            this.transactions = new Transactions();
        }

        protected BudgetEntry(string name, Month month, int year, Money budget, bool isClosed, Transactions transactions): this(name, month, year, budget) {
            foreach (Transaction transaction in transactions) {
                this.transactions.Add(transaction.Clone());
            }
            this.IsClosed = isClosed;
        }

        protected abstract BudgetEntry CreateNewBudgetEntry(string name, Month month, int year, Money budget, bool isClosed, Transactions transactions);

        private BudgetEntry CreateNewBudgetEntry(string name, Month month, int year, Money budget) {
            return this.CreateNewBudgetEntry(name, month, year, budget, this.IsClosed, this.transactions);
        }

        protected abstract bool IsValueOverBudget(Money value);

        public Money GetNextMonthsValue() {
            var transactionsValue = this.transactions.GetValue();

            if (IsClosed || IsValueOverBudget(transactionsValue)) {
                return transactionsValue;
            }

            return Budget.Clone();
        }

        public BudgetEntry AddTransaction(Transaction transaction) {
            if (!IsClosed) {
                Transactions newTransactions = new Transactions();
                newTransactions.AddRange(this.transactions);
                newTransactions.Add(transaction);
                return CreateNewBudgetEntry(this.Name, this.Month, this.Year, this.Budget, this.IsClosed, newTransactions);
            } else {
                throw new BudgetIsClosedException();
            }
        }

        public BudgetEntry Close() {
            return CreateNewBudgetEntry(this.Name, this.Month, this.Year, this.Budget, true, this.transactions);
        }        

        public BudgetEntry CopyTo(Month month, int year) {
            return CreateNewBudgetEntry(this.Name, month, year, this.Budget);
        }
    }

    public class PositiveBudgetEntry: BudgetEntry {
        public PositiveBudgetEntry(string name, Month month, int year, Money budget): base(name, month, year, budget) {
        }

        protected PositiveBudgetEntry(string name, Month month, int year, Money budget, bool isClosed, Transactions transactions): base(name, month, year, budget, isClosed, transactions) {
        }

        protected override bool IsValueOverBudget(Money value) {
            return value >= Budget;
        }

        protected override BudgetEntry CreateNewBudgetEntry(string name, PlanAhead.Month month, int year, Money budget, bool isClosed, Transactions transactions) {
            return new PositiveBudgetEntry(name, month, year, budget, isClosed, transactions);
        }

    }

    public class NegativeBudgetEntry: BudgetEntry {
        public NegativeBudgetEntry(string name, Month month, int year, Money budget): base(name, month, year, budget) {
        }

        protected NegativeBudgetEntry(string name, Month month, int year, Money budget, bool isClosed, Transactions transactions): base(name, month, year, budget, isClosed, transactions) {
        }

        protected override bool IsValueOverBudget(Money value) {
            return value <= Budget;
        }

        protected override BudgetEntry CreateNewBudgetEntry(string name, PlanAhead.Month month, int year, Money budget, bool isClosed, Transactions transactions) {
            return new NegativeBudgetEntry(name, month, year, budget, isClosed, transactions);
        }

    }
}


using System;

namespace PlanAhead {
    public class BudgetIsClosedException: Exception {
    }

    public abstract class BudgetCategory {
        public readonly string Name;
        public readonly Money Budget;
        public readonly bool IsClosed;

        private FinancialTransactions transactions;

        public BudgetCategory(string name, Money budget) {
            this.Name = name;
            this.Budget = budget.Clone();
            this.IsClosed = false;

            this.transactions = new FinancialTransactions();
        }

        protected BudgetCategory(string name, Money budget, bool isClosed, FinancialTransactions transactions): this(name, budget) {
            foreach (FinancialTransaction transaction in transactions) {
                this.transactions.Add(transaction.Clone());
            }
            this.IsClosed = isClosed;
        }

        protected abstract BudgetCategory CreateNewBudgetEntry(string name, Money budget, bool isClosed, FinancialTransactions transactions);

        private BudgetCategory CreateNewBudgetEntry(string name, Money budget) {
            return this.CreateNewBudgetEntry(name, budget, this.IsClosed, this.transactions);
        }

        protected abstract bool IsValueOverBudget(Money value);

        public Money GetValue(Month month, int year) {
            var transactionsValue = this.transactions.GetValue(month, year);

            if (IsClosed || IsValueOverBudget(transactionsValue)) {
                return transactionsValue;
            }

            return Budget.Clone();
        }

        public BudgetCategory AddTransaction(FinancialTransaction transaction) {
            if (!IsClosed) {
                FinancialTransactions newTransactions = new FinancialTransactions();
                newTransactions.AddRange(this.transactions);
                newTransactions.Add(transaction);
                return CreateNewBudgetEntry(this.Name, this.Budget, this.IsClosed, newTransactions);
            } else {
                throw new BudgetIsClosedException();
            }
        }

        public BudgetCategory Close() {
            return CreateNewBudgetEntry(this.Name, this.Budget, true, this.transactions);
        }        
    }

    public class PositiveBudgetEntry: BudgetCategory {
        public PositiveBudgetEntry(string name, Money budget): base(name, budget) {
        }

        protected PositiveBudgetEntry(string name, Money budget, bool isClosed, FinancialTransactions transactions): base(name, budget, isClosed, transactions) {
        }

        protected override bool IsValueOverBudget(Money value) {
            return value >= Budget;
        }

        protected override BudgetCategory CreateNewBudgetEntry(string name, PlanAhead.Money budget, bool isClosed, FinancialTransactions transactions) {
            return new PositiveBudgetEntry(name, budget, isClosed, transactions);
        }

    }

    public class NegativeBudgetEntry: BudgetCategory {
        public NegativeBudgetEntry(string name, Money budget): base(name, budget) {
        }

        protected NegativeBudgetEntry(string name, Money budget, bool isClosed, FinancialTransactions transactions): base(name, budget, isClosed, transactions) {
        }

        protected override bool IsValueOverBudget(Money value) {
            return value <= Budget;
        }

        protected override BudgetCategory CreateNewBudgetEntry(string name, Money budget, bool isClosed, FinancialTransactions transactions) {
            return new NegativeBudgetEntry(name, budget, isClosed, transactions);
        }
    }
}


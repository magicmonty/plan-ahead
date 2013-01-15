using System;

namespace PlanAhead {
    public class BudgetIsClosedException: Exception {
    }

    public class BudgetCategory {
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

        public BudgetCategory(string name, Money budget, bool isClosed, FinancialTransactions transactions): this(name, budget) {
            foreach (FinancialTransaction transaction in transactions) {
                this.transactions.Add(transaction.Clone());
            }
            this.IsClosed = isClosed;
        }

        private bool IsPositiveBudget() {
            return this.Budget >= Money.ZERO;
        }

        private bool IsValueOverBudget(Money value) {
            if (IsPositiveBudget()) {
                return value >= Budget;
            }
            return value <= Budget;
        }

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
                return new BudgetCategory(this.Name, this.Budget, this.IsClosed, newTransactions);
            } else {
                throw new BudgetIsClosedException();
            }
        }

        public BudgetCategory Close() {
            return new BudgetCategory(this.Name, this.Budget, true, this.transactions);
        }        
    }
}


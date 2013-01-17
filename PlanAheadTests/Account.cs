using System;

namespace PlanAhead {
    public class Account {
        private FinancialTransactions transactions;

        public Account(DateTime initialDate, Money initialBalance) {
            this.transactions = new FinancialTransactions();
            if (initialBalance < Money.ZERO) {
                this.transactions.Add(new WithdrawTransaction(initialDate, initialBalance * -1));
            } else if (initialBalance > Money.ZERO) {
                this.transactions.Add(new DepositTransaction(initialDate, initialBalance));
            }
        }

        private Account(FinancialTransactions transactions) {
            this.transactions = new FinancialTransactions(transactions);
        }

        private Account AddTransaction<T>(DateTime date, Money value) where T: FinancialTransaction, new() {
            if (value > Money.ZERO) {
                var newTransactions = new FinancialTransactions(transactions);
                FinancialTransaction transaction = new T();
                transaction = transaction.SetDate(date).SetValue(value);
                newTransactions.Add(transaction);
                return new Account(newTransactions);
            }
            
            return new Account(this.transactions);
        }

        public Money GetValueOf(Month month, Year year) {
            return transactions.GetValueOf(month, year);
        }

        public Money GetBalance() {
            return transactions.GetTotal();
        }
        
        public Money GetBalance(Month month, Year year) {
            return transactions.GetValueUntil(month, year);
        }
        
        public Account Withdraw(DateTime date, Money value) {
            return AddTransaction<WithdrawTransaction>(date, value);
        }

        public Account Deposit(DateTime date, Money value) {
            return AddTransaction<DepositTransaction>(date, value);
        }
    }
}


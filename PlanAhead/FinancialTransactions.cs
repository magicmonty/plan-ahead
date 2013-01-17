using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanAhead {
    public class FinancialTransactions: List<FinancialTransaction> {

        public FinancialTransactions(): base() {

        }

        public FinancialTransactions(IEnumerable<FinancialTransaction> transactions):base(transactions) {

        }

        private Money GetTotalOfTransactions(IEnumerable<FinancialTransaction> transactions) {
            Money result = Money.ZERO;
            foreach (var transaction in transactions) {
                result = transaction.apply(result);
            }

            return result;
        }

        public Money GetTotal() {
            return GetTotalOfTransactions(this);
        }

        public Money GetValueOf(Month month, Year year) {
            var transactions = this.Where(transaction => (transaction.Date.Year == year.Value && transaction.Date.Month == (int)month));
            return GetTotalOfTransactions(transactions);
        }

        public Money GetValueOf(Year year) {
            var transactions = this.Where(transaction => (transaction.Date.Year == year.Value));
            return GetTotalOfTransactions(transactions);
        }

        public Money GetValueUntil(Month month, Year year) {
            var transactions = this.Where(transaction => (transaction.Date.Year <= year.Value && transaction.Date.Month <= (int)month));
            return GetTotalOfTransactions(transactions);
        }
    }
}


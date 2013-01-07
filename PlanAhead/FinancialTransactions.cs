using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanAhead {
    public class FinancialTransactions: List<FinancialTransaction> {

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

        public Money GetValue(Month month, int year) {
            var transactions = this.Where(transaction => (transaction.Date.Year == year && transaction.Date.Month == (int)month));
            return GetTotalOfTransactions(transactions);
        }

        public Money GetValue(int year) {
            var transactions = this.Where(transaction => (transaction.Date.Year == year));
            return GetTotalOfTransactions(transactions);
        }

    }
}


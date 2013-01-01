using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanAhead {
    public class Transactions: List<Transaction> {

        public Money GetValue() {
            Money result = new Money(0);           
            foreach (var transaction in this) {
                result = transaction.apply(result);
            }

            return result;
        }
    }
}


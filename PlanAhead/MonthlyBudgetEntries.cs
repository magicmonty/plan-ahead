using System.Collections.Generic;
using System;
using System.Linq;

namespace PlanAhead {

    public class BudgetEntryExistsAlreadyException: Exception {

    }

    public class MonthlyBudgetEntries {

        private int year;
        private Month month;
        private List<BudgetEntry> entries;

        public MonthlyBudgetEntries(int year, Month month): base() {
            this.year = year;
            this.month = month;
            this.entries = new List<BudgetEntry>();
        }

        private bool HasEntry(string name) {
            var foundEntries = entries.Where(e => e.Name.Equals(name));

            return (foundEntries.Count() > 0);
        }

        public BudgetEntry AddPositive(string name, Money budget) {           
            if (HasEntry(name)) {
                throw new BudgetEntryExistsAlreadyException();
            }

            BudgetEntry entry = new PositiveBudgetEntry(name, this.month, this.year, budget);
            entries.Add(entry);
            return entry;
        }

        public BudgetEntry AddNegative(string name, Money budget) {
            if (HasEntry(name)) {
                throw new BudgetEntryExistsAlreadyException();
            }

            BudgetEntry entry = new NegativeBudgetEntry(name, this.month, this.year, budget);
            entries.Add(entry);
            return entry;
        }
    }

}


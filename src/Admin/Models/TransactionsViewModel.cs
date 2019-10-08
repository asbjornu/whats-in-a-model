using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Admin.Factories;

namespace Admin.Models
{
    public class TransactionsModel : BaseModel, IEnumerable<TransactionModel>
    {
        public IEnumerable<TransactionModel> Transactions { get; set; }

        public IEnumerator<TransactionModel> GetEnumerator()
        {
            return Transactions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Transactions.GetEnumerator();
        }
    }
}

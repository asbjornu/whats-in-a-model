using System.Collections;
using System.Collections.Generic;

namespace Admin.Models
{
    public class CustomersViewModel : BaseViewModel, IEnumerable<CustomerViewModel>
    {
        public IEnumerable<CustomerViewModel> Customers { get; set; }

        public IEnumerator<CustomerViewModel> GetEnumerator()
        {
            return Customers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Customers.GetEnumerator();
        }
    }
}

using System.Collections;
using System.Collections.Generic;

namespace Admin.Models
{
    public class CustomersModel : BaseModel, IEnumerable<CustomerModel>
    {
        public IEnumerable<CustomerModel> Customers { get; set; }

        public IEnumerator<CustomerModel> GetEnumerator()
        {
            return Customers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Customers.GetEnumerator();
        }
    }
}

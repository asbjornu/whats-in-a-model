using System.Collections.Generic;

namespace Admin.Models
{
    public class CustomerViewModel : BaseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }
        public AddressViewModel Address { get; set; }
        public string UserName { get; set; }
        public string Website { get; set; }
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
        public string Json { get; set; }
    }
}

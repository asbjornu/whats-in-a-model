using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
    public class CustomerModel : BaseModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required]
        public string Email { get; set; }

        public string Ssn { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public AddressModel Address { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Website { get; set; }

        public IEnumerable<TransactionModel> Transactions { get; set; }
    }
}

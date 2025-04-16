using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class ClientAddressEntity
    {
        [Key, ForeignKey(nameof(Client))]
        public string ClientId { get; set; } = null!;
        public ClientEntity Client { get; set; } = null!;

        public string StreetName { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string BillingReference { get; set; } = null!;
    }
}

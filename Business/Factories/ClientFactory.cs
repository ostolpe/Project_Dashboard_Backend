using Business.Dtos;
using Data.Entities;

namespace Business.Factories
{
    public static class ClientFactory
    {
        public static ClientEntity CreateClientEntity(AddClientForm clientForm)
        {
            return new ClientEntity
            {
                Name = clientForm.Name,
                Email = clientForm.Email,
                Phone = clientForm.Phone,
                ImageUrl = clientForm.ImageUrl,
                Address = new ClientAddressEntity
                {
                    StreetName = clientForm.StreetName,
                    PostalCode = clientForm.PostalCode,
                    City = clientForm.City,
                    BillingReference = clientForm.BillingReference
                }
            };
        }

        public static Client CreateClient(ClientEntity entity)
        {
            return new Client
            {
                Id = entity.Id,
                Name = entity.Name,
                Phone = entity.Phone,
                ImageUrl = entity.ImageUrl,
                StreetName = entity.Address!.StreetName,
                PostalCode = entity.Address!.PostalCode,
                City = entity.Address!.City,
                BillingReference = entity.Address!.BillingReference
            };
        }

        public static void UpdateClient(ClientEntity entity, UpdateClientForm clientForm)
        {
            if (entity.Name != clientForm.Name)
                entity.Name = clientForm.Name;

            if (entity.Email != clientForm.Email)
                entity.Email = clientForm.Email;

            if (entity.Phone != clientForm.Phone)
                entity.Phone = clientForm.Phone;

            if (entity.ImageUrl != clientForm.ImageUrl)
                entity.ImageUrl = clientForm.ImageUrl;

            if (entity.Address?.StreetName != clientForm.StreetName)
                entity.Address!.StreetName = clientForm.StreetName;

            if (entity.Address?.PostalCode != clientForm.PostalCode)
                entity.Address!.PostalCode = clientForm.PostalCode;

            if (entity.Address?.City != clientForm.City)
                entity.Address!.City = clientForm.City;

            if (entity.Address?.BillingReference != clientForm.BillingReference)
                entity.Address!.BillingReference = clientForm.BillingReference;
        }
    }
}

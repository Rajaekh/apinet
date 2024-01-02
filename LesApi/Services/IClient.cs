using LesApi.Models;

namespace LesApi.Services
{
    public interface IClient
    {
        Client GetClientByPhone(string phoneNumber);
        Client GetClientById(string IdClient);
        Client AddClient(Client client);

        List<Beneficiaire> GetClientBeneficiaire( string IdClient);

    }
}

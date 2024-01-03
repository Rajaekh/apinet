using LesApi.Models;
using MongoDB.Driver;

namespace LesApi.Services
{
    public class ClientService : IClient
    {
        private readonly IBeneficiaire _IBeneficiaire;
        private readonly IMongoCollection<Client> _client;

        public ClientService(ITransfertDatabaseSettings settings, IMongoClient mongoClient, IBeneficiaire IBeneficiaire)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _IBeneficiaire = IBeneficiaire;
            _client = database.GetCollection<Client>(settings.ClientCollectionName);
        }
        public Client AddClient(Client client)
        {
            _client.InsertOne(client);
            return client;
        }

        public List<Beneficiaire> GetClientBeneficiaire(string IdClient)
        {
            List<Beneficiaire> beneficiaires = new List<Beneficiaire>(); // Initialisez la liste ici
            var client = _client.Find(Client => Client.IdClient == IdClient).FirstOrDefault();

            if (client != null && client.beneficiaires != null)
            {
                foreach (var idb in client.beneficiaires)
                {
                    beneficiaires.Add(_IBeneficiaire.GetBeneficiaireById(idb));
                }
            }

            return beneficiaires;
        }


        public Client GetClientById(string IdClient)
        {
            return _client.Find(Client => Client.IdClient == IdClient).FirstOrDefault();
        }

        public Client GetClientByPhone(string phoneNumber)
        {
            return _client.Find(Client => Client.Phone == phoneNumber).FirstOrDefault();
        }
    }
}

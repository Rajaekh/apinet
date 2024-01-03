using LesApi.Models;
using MongoDB.Driver;

namespace LesApi.Services
{
    public class BeneficiaireService : IBeneficiaire
    {
        private IMongoCollection<Beneficiaire> _beneficiaire;

        public BeneficiaireService(ITransfertDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _beneficiaire = database.GetCollection<Beneficiaire>(settings.BeneficiaireCollectionName);
        }

        public Beneficiaire AddBeneficiaire(Beneficiaire beneficiaire)
        {
            _beneficiaire.InsertOne(beneficiaire);
            return beneficiaire;
        }

        public Beneficiaire GetBeneficiaireByGSM(string gsm)
        {
            return _beneficiaire.Find(Beneficiaire => Beneficiaire.NumeroGsm == gsm).FirstOrDefault();
        }

        public Beneficiaire GetBeneficiaireById(string IdBeneficiaire)
        {
            if (IdBeneficiaire.Length != 24)
            {
                // Gérer le cas où la chaîne n'a pas la longueur attendue
                return null; // Ou une autre valeur par défaut, selon votre logique
            }

            return _beneficiaire.Find(Beneficiaire => Beneficiaire.Id == IdBeneficiaire).FirstOrDefault();
        }
    }
}

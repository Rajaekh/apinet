using LesApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace LesApi.Services
{
    public class UserService : IUser
    {
        private readonly IBeneficiaire _beneficiaire;
        private readonly IMongoCollection<User> _user;
        private readonly IMongoCollection<Transfert> _transfert;
        public UserService(ITransfertDatabaseSettings settings, IMongoClient mongoClient, IBeneficiaire beneficiaire)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _beneficiaire = beneficiaire;
            _user = database.GetCollection<User>(settings.UsersCollectionName);
            _transfert = database.GetCollection<Transfert>(settings.TransfertCollectionName);
        }

        public User AddUser(User user)
        {
            _user.InsertOne(user);
            return user;
        }

        public List<Beneficiaire> GetUserBeneficiaire(string userId)
        {
            List<Beneficiaire> beneficiaires = new List<Beneficiaire>();
            var user = _user.Find(u => u.Id == userId).FirstOrDefault();

            if (user != null && user.beneficiaires!= null)
            {
                foreach (var beneficiaireId in user.beneficiaires)
                {
                    beneficiaires.Add(_beneficiaire.GetBeneficiaireById(beneficiaireId));
                }
            }

            return beneficiaires;
        }

        public User GetUserById(string userId)
        {
            return _user.Find(u => u.Id == userId).FirstOrDefault();
        }

        public User GetUserByGSM(string gsm)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Gsm, gsm);
            return _user.Find(filter).FirstOrDefault();
        }


        public User EditUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            var result = _user.ReplaceOne(filter, user);

            if (result.ModifiedCount > 0)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        //pour savoir la date du 1 ere transfert:
        public DateTime GetDatePremierTransfert(string idClient)
        {
            var datePremierTransfert = _transfert
                .Find(t => t.IdClient == idClient && t.DataeTransfert != null)
                .SortBy(t => t.DataeTransfert)
                .Limit(1)
                .Project(t => t.DataeTransfert)
                .FirstOrDefault();

            return datePremierTransfert;
        }

        public User GetUserByIdentity(string Nidentity)
        {
            return _user.Find(u => u.N_Identity == Nidentity).FirstOrDefault();
        }
    }
}

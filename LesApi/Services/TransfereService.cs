using LesApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LesApi.Services
{
    public class TransfereService : ITransfert
    {
        private readonly IBeneficiaire _IBeneficiaire;
        private readonly IMongoCollection<Transfert> _transfert;
        private readonly IUser _user;
        public TransfereService(ITransfertDatabaseSettings settings, IMongoClient mongoClient, IUser user)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _user = user;
            _transfert = database.GetCollection<Transfert>(settings.TransfertCollectionName);
       
        }
        public Transfert AddTransfert(Transfert transfert)
        {
            _transfert.InsertOne(transfert);
            return transfert;
        }
        // verifier si le montant de tansfert  depasse le plafond annuel ;
        public bool DepasseMontantAnnuel(string idClient, DateTime dateActuelle, double montantTransfert, double PlafondAnnuel)
        {
            if (idClient != null)
            {
                // Obtenez la date du premier transfert pour le client
                DateTime datePremierTransfert = _user.GetDatePremierTransfert(idClient);

                // Vérifiez si l'utilisateur est trouvé
                var user = _user.GetUserById(idClient);

                if (user != null)
                {
                    // Vérifiez si la date actuelle est après un an à partir de la date du premier transfert
                    if (dateActuelle > datePremierTransfert.AddYears(1))
                    {
                        // Si nous sommes après un an, réinitialisez le montant annuel de transfert
                        user.MontantTransfertAnnuel = montantTransfert;
                        // Mettez à jour la date du premier transfert
                        user.DatePremierTransfert = dateActuelle;
                        return false;
                    }
                    else
                    {
                        // Si nous sommes toujours dans la même année, vérifiez le montant annuel par rapport au plafond
                        if (user.MontantTransfertAnnuel + montantTransfert > PlafondAnnuel)
                        {
                            // Le transfert ne peut pas être effectué car le plafond annuel serait dépassé
                            return true;
                        }
                        else
                        {
                            // Mettez à jour le montant annuel de transfert
                            user.MontantTransfertAnnuel += montantTransfert;
                            return false;
                        }
                    }
                }
                else
                {
                    
                    return false;
                }
            }
            else
            {
              
                return false;
            }
        }
        public Transfert EditTransfertStatus(string id, TransferModel trans)
        {
            if (id == null )
            {
                return null; // Les paramètres sont invalides, vous pouvez ajuster cela selon votre logique
            }
            // Obtenez le transfert par ID
            var filter = Builders<Transfert>.Filter.Eq(u => u.Id, id);
            var transfert = _transfert.Find(t => t.Id == id).FirstOrDefault();
            // Vérifiez si le transfert a été trouvé
            if (transfert != null)
            {
                // Mettez à jour le statut
                // on doit mettre a jour le compte de l agent:
                var user = _user.GetUserById(trans.Idagent);
                if (trans.Status.Equals("Extourné") && (transfert.Status.Equals("à servir")))
                {
                    transfert.AutreMotif = trans.AutreMotif;
                    transfert.MotifRestitution = trans.MotifRestitution;
                    transfert.Status = trans.Status;
                    //Modifier l user:
                    user.Montant= (double)(transfert.Montant + transfert.ValFrais);
                    _user.EditUser(user);
                }
                else if (trans.Equals("Restitué") && (transfert.Status.Equals("à servir") || transfert.Status.Equals("débloqué à servir")))
                {
                    transfert.AutreMotif = trans.AutreMotif;
                    transfert.MotifRestitution = trans.MotifRestitution;
                    transfert.Status = trans.Status;
                    user.Montant = trans.Montant;
                    _user.EditUser(user);
                }
                else if (trans.Equals("Payé") && (transfert.Status.Equals("à servir") || transfert.Status.Equals("débloqué à servir")))
                {
                    transfert.Status = trans.Status;
                }
                
                else if( trans.Equals("Bloqué") || trans.Equals("débloqué à servir"))
                {
                    transfert.Status = trans.Status;
                    transfert.AutreMotif = trans.AutreMotif;
                    transfert.MotifBlicage = trans.MotifBlicage;
                }
                else
                {
                    return null;
                }
               
                // Effectuez la mise à jour dans la base de données
                var result = _transfert.ReplaceOne(filter, transfert);

                // Vérifiez si la mise à jour a réussi
                if (result.ModifiedCount > 0)
                {
                    return transfert; // Retournez le transfert mis à jour
                }
                else
                {
                    return null; // La mise à jour a échoué
                }
            }
            else
            {
                return null; // Le transfert avec l'ID spécifié n'a pas été trouvé
            }
        }
        public List<Transfert> GetallTransfert()
        {
            // Utilisez la méthode Find pour obtenir tous les transferts dans la collection
            var transfertsCursor = _transfert.Find(_ => true);

            // Convertissez les documents en liste de transferts
            List<Transfert> transferts = transfertsCursor.ToList();

            return transferts;
        }
        public Transfert GetTransfertById(string id)
        {
            if (id == null || !IsValidObjectIdFormat(id))
            {
                return null;
            }
            // Utilisez la méthode Find pour obtenir le transfert avec l'ID spécifié
            var transfert = _transfert.Find(t => t.Id == id).FirstOrDefault();

            // Vérifiez si le transfert a été trouvé
            if (transfert != null)
            {
                return transfert;
            }
            else
            {
                // Si le transfert n'est pas trouvé, vous pouvez choisir de lever une exception, de retourner null, ou de gérer d'une autre manière.
                // Dans cet exemple, nous retournons null.
                return null;
            }
        }
        public static bool IsValidObjectIdFormat(string id)
        {
            return ObjectId.TryParse(id, out _);
        }

    }
}

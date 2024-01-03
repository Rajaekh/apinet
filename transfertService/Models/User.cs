using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Data;

namespace LesApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("lastname")]
        public string Lastname { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("role")]
       
        public Role Role { get; set; }


        [BsonElement("titre")]
        public string Titre { get; set; }

        [BsonElement("typePieceIdentity")]
        public string TypePieceIdentity { get; set; }

        [BsonElement("n_identity")]
        public string N_Identity { get; set; }

        [BsonElement("gsm")]
        public string Gsm { get; set; }

        [BsonElement("paysEmission")]
        public string PaysEmission { get; set; }

        [BsonElement("numeroPieceIdentite")]
        public string NumeroPieceIdentite { get; set; }

        [BsonElement("dateExpirationPiece")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DateExpirationPiece { get; set; }

        [BsonElement("validitePieceIdentite")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime ValiditePieceIdentite { get; set; }

        [BsonElement("dateNaissance")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DateNaissance { get; set; }
        [BsonElement("datePremierTransfert")]
        public DateTime DatePremierTransfert{get;set;}
        [BsonElement("profession")]
        public string Profession { get; set; }

        [BsonElement("paysNationalite")]
        public string PaysNationalite { get; set; }

        [BsonElement("paysAdresse")]
        public string PaysAdresse { get; set; }

        [BsonElement("adresseLegale")]
        public string AdresseLegale { get; set; }

        [BsonElement("ville")]
        public string Ville { get; set; }

        [BsonElement("estSupprimer")]
        public bool EstSupprimer { get; set; }

        [BsonElement("surListeNoire")]
        public bool SurListeNoire { get; set; }

        [BsonElement("montant")]
        public double Montant { get; set; }
        [BsonElement("montantTransfertAnnuel")]
        public double MontantTransfertAnnuel { get; set; }
        [BsonElement("beneficiaires")]
        public List<string> beneficiaires { get; set; } = new List<string>();

    }
}

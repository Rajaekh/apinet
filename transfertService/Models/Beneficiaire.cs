using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LesApi.Models
{
    public class Beneficiaire
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("nom")]
        public string Nom { get; set; }

        [BsonElement("prenom")]
        public string Prenom { get; set; }

        [BsonElement("numeroGsm")]
        public string NumeroGsm { get; set; }

        [BsonElement("pieceIdentity")]
        public string PieceIdentity { get; set; }
    }
}

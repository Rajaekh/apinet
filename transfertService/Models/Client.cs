using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LesApi.Models
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdClient { get; set; } = String.Empty;
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("gender")]  // Changez ceci pour correspondre au champ dans le document JSON
        public string gender { get; set; }

        [BsonElement("pays")]
        public string Pays { get; set; }
        [BsonElement("ville")]
        public string Ville { get; set; }
        [BsonElement("montant")]
        public Decimal Montant { get; set; }
        [BsonElement("isInBlackList ")]
        public bool isInBlackList { get; set; }
        [BsonElement("pieceIdentite")]
        public string PieceIdentite { get;set; }
        [BsonElement("beneficiaires")]
        public List<string> beneficiaires = new List<string>();


    }
}

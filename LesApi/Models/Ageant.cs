using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LesApi.Models
{
    public class Ageant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdBeneficiaire { get; set; } = String.Empty;
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("montant")]
        public Decimal Montant { get; set; }
    }
}

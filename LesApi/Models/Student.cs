using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace LesApi.Models
    
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }=String.Empty;
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("graduated")]
        public bool IsGraduated { get; set; }
        [BsonElement("courses")]
        public string[]? Courses { get; set; }
        [BsonElement("gender")]
        public string Gender { get; set; }=string.Empty;
        [BsonElement("age")]
        public int Age { get; set; }
    }
}

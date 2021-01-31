using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Podium_API.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("first name")]
        public string FirstName { get; set; }

        [BsonElement("last name")]
        public decimal LastName { get; set; }

        [BsonElement("date of birth")]
        public bool DateOfBirth { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
    }
}

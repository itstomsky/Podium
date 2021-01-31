using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Podium_API.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("lender")]
        public string Lender { get; set; }

        [BsonElement("interest rate")]
        public decimal InterestRate { get; set; }

        [BsonElement("fixed")]
        public bool IsFixed { get; set; }

        [BsonElement("minimum ltv")]
        public decimal MinimumLTV { get; set; }
    }
}

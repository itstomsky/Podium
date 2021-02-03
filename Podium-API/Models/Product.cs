using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Podium_API.Models
{
    public class Product
    {
        [BsonId]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("lender")]
        [JsonProperty("lender")]
        public string Lender { get; set; }

        [BsonElement("interestRate")]
        [JsonProperty("interestRate")]
        public decimal InterestRate { get; set; }

        [BsonElement("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        [BsonElement("minimumLTV")]
        [JsonIgnore]
        public decimal MinimumLTV { get; set; }

        public Product(string lender, decimal interestRate, string type, decimal minimumLTV)
        {
            Lender = lender;
            InterestRate = interestRate;
            Type = type;
            MinimumLTV = minimumLTV;
        }
    }
}

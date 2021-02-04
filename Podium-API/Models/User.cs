using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Podium_API.Models
{
    public class User
    {
        [BsonId]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("firstName")]
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [BsonElement("dateOfBirth")]
        [JsonProperty("dateOfBirth")]
        [BsonDateTimeOptions]
        public DateTime DateOfBirth { get; set; }

        [BsonElement("email")]
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}

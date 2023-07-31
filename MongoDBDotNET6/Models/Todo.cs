using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoAPI.Models
{
    public class Todo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Task { get; set; } = null!;
        public Boolean Done { get; set; } 
    }
}

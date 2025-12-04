using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProjectsAndTasks.MongoDb.Model
{
    /// <summary>
    /// E.A.T. 30-October-2025
    /// Person data model for storing users in MongoDB.
    /// </summary>
    public class Person
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("login")]
        public string Login { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
}

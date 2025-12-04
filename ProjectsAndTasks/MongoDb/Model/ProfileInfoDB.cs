using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProjectsAndTasks.MongoDb.Model
{
    public class ProfileInfoDB
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("id_person")]
        public ObjectId PersonId { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("mail")]
        public string Mail{ get; set; }

    }
}

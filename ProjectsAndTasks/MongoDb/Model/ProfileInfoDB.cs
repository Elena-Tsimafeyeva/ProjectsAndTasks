using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using ProjectsAndTasks.MongoDb.Model.InterfacesModel;

namespace ProjectsAndTasks.MongoDb.Model
{
    public class ProfileInfoDB : IProfileInfoDB
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

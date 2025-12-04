using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProjectsAndTasks.MongoDb.Model
{
    /// <summary>
    /// E.A.T. 30-October-2025
    /// Person data model for storing projects in MongoDB.
    /// </summary>
    public class Project
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("id_person")]
        public ObjectId PersonId { get; set; } 

        [BsonElement("project_name")]
        public string ProjectName { get; set; }

        [BsonElement("project_percent")]
        public int Percent { get; set; }

        [BsonElement("project_description")]
        public string ProjectDescription { get; set; }
    }
}

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProjectsAndTasks.MongoDb.Model
{
    /// <summary>
    /// E.A.T. 30-October-2025
    /// Person data model for storing tasks in MongoDB.
    /// </summary>
    public class ProjectTask
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("id_project")]
        public ObjectId ProjectId { get; set; }

        [BsonElement("task_name")]
        public string TaskName { get; set; }

        [BsonElement("task_percent")]
        public int TaskPercent { get; set; }

        [BsonElement("task_description")]
        public string TaskDescription { get; set; }
    }
}

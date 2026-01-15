using MongoDB.Bson;

namespace ProjectsAndTasks.MongoDb.Model.InterfacesModel
{
    public interface IProjectTask
    {
        ObjectId Id { get; set; }
        ObjectId ProjectId { get; set; }
        string TaskName { get; set; }
        int TaskPercent { get; set; }
        string TaskDescription { get; set; }
    }
}

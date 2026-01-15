using MongoDB.Bson;

namespace ProjectsAndTasks.MongoDb.Model.InterfacesModel
{
    public interface IProject
    {
        ObjectId Id { get; set; }
        ObjectId PersonId { get; set; }
        string ProjectName { get; set; }
        int Percent { get; set; }
        string ProjectDescription { get; set; }
    }
}

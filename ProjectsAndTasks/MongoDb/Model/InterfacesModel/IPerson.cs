using MongoDB.Bson;

namespace ProjectsAndTasks.MongoDb.Model.InterfacesModel
{
    public interface IPerson
    {
        ObjectId Id { get; set; }
        string Login { get; set; }
        string Password { get; set; }
    }
}

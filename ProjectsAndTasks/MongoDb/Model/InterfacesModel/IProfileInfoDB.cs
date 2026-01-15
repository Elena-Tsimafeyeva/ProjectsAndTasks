using MongoDB.Bson;

namespace ProjectsAndTasks.MongoDb.Model.InterfacesModel
{
    public interface IProfileInfoDB
    {
        ObjectId Id { get; set; }
        ObjectId PersonId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Mail { get; set; }
    }
    

}

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoAPI.Models;

namespace ToDoAPI.Repository
{
    public class ToDoRepository : ITodoRepository
    {
        private readonly IMongoCollection<Todo> _TodoCollection;

        public ToDoRepository(IOptions<DatabaseSettings> dabaseSettings)
        {

            var mongoClient = new MongoClient(dabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dabaseSettings.Value.DatabaseName);

            _TodoCollection = mongoDatabase.GetCollection<Todo>("Todo");
        }


        public async Task<List<Todo>> GetToDosAsync() =>
          await _TodoCollection.Find(_ => true).ToListAsync();


        public async Task<Todo?> GetToDoAsync(string id) =>
          await _TodoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateToDoTaskAsync(Todo newTask) =>
            await _TodoCollection.InsertOneAsync(newTask);

        public async Task UpdateToDoTaskAsync(string id, Todo updatedTask) =>
            await _TodoCollection.ReplaceOneAsync(x => x.Id == id, updatedTask);

        public async Task RemoveToDoTaskAsync(string id) => await _TodoCollection.DeleteOneAsync(x => x.Id == id);



    }
}
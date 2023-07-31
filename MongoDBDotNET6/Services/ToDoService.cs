using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoAPI.Models;

namespace ToDoAPI.Services
{

    // Note that we cn use the ToDoservice instead of the ToDorepository just by injecting the service into ToDo Controller
    public class ToDoService
    {

        private readonly IMongoCollection<Todo> _TodoCollection;

        public ToDoService(
            IOptions<DatabaseSettings> dabaseSettings)
        {
            var mongoClient = new MongoClient(dabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dabaseSettings.Value.DatabaseName);

            _TodoCollection = mongoDatabase.GetCollection<Todo>("Todo");
        }

        public async Task<List<Todo>> GetAsync() =>
            await _TodoCollection.Find(_ => true).ToListAsync();

        public async Task<Todo?> GetAsync(string id) =>
            await _TodoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Todo newTask) =>
            await _TodoCollection.InsertOneAsync(newTask);

        public async Task UpdateAsync(string id, Todo updatedTask) =>
            await _TodoCollection.ReplaceOneAsync(x => x.Id == id, updatedTask);

        public async Task RemoveAsync(string id) => await _TodoCollection.DeleteOneAsync(x => x.Id == id);
    }
}

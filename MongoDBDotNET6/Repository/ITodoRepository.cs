using ToDoAPI.Models;

namespace ToDoAPI.Repository
{
    public interface ITodoRepository
    {
         Task<List<Todo>> GetToDosAsync();
        Task<Todo?> GetToDoAsync(string id);
        Task CreateToDoTaskAsync(Todo todo);
        Task UpdateToDoTaskAsync(string id, Todo todo);
        Task RemoveToDoTaskAsync(string id);
    }

}

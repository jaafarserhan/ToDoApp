using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Models;
using ToDoAPI.Repository;
using ToDoAPI.Services;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {   // Declare the ToDoRepository 
        private readonly ITodoRepository _ToDoRepository;

        //Inject The ToDoRepository into the constructor
        public ToDoController(ToDoRepository ToDoRepository) => _ToDoRepository = ToDoRepository;


        #region End-points CRUD functionalites for ToDo Application

        // Get all ToDo tasks
        [HttpGet]
        public async Task<List<Todo>> Get() => await _ToDoRepository.GetToDosAsync();


        // Get ToDo task by id
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Todo>> Get(string id)
        {
            var ToDo = await _ToDoRepository.GetToDoAsync(id);

            if (ToDo is null)
            {
                return NotFound();
            }

            return ToDo;
        }


        // Create new ToDo task by passing newToDo Object
        [HttpPost]
        public async Task<IActionResult> Post(Todo newToDo)
        {
            _ToDoRepository.CreateToDoTaskAsync(newToDo);

            return NoContent();
        }


        // Update ToDo task by id and updatedToDo object
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Todo updatedToDo)
        {

            var ToDo = await _ToDoRepository.GetToDoAsync(id);

            if (ToDo is null)
            {
                return NotFound();
            }

            updatedToDo.Id = ToDo.Id;

            await _ToDoRepository.UpdateToDoTaskAsync(id, updatedToDo);

            return NoContent();
        }



        // Delete ToDo task from the list by id
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ToDo = await _ToDoRepository.GetToDoAsync(id);

            if (ToDo is null)
            {
                return NotFound();
            }

            await _ToDoRepository.RemoveToDoTaskAsync(id);

            return NoContent();
        }

        #endregion
    }
}

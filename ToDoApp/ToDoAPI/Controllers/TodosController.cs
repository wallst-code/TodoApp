using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoLibrary.DataAccess;
using ToDoLibrary.Models;


namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoData _data;
       
        // Constructor
        public TodosController(ITodoData data)
        {
            _data = data;            
        }

        private int GetUserId()
        {
            var userIdText = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdText);
        }

        // GET: api/Todos
        [HttpGet]
        public async Task<ActionResult<List<TodoModel>>> Get()
        {
            var output = await _data.GetAllAssigned(GetUserId());

            return Ok(output);
        }

        // GET api/Todos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoModel>> Get(int todoId)
        {
            var output = await _data.GetAllAssigned(GetUserId());

            return Ok(output);
        }

        // POST api/Todos
        [HttpPost]
        public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
        {
            var output = await _data.Create(GetUserId(), task);

            return Ok(output);
        }

        [HttpPut]
        // PUT api/Todos/5
        public IActionResult Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // PUT api/Todos/5/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            throw new NotImplementedException();
        }

        // DELETE api/Todos/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

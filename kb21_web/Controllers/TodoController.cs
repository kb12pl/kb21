using Microsoft.AspNetCore.Mvc;
using kb21_web.Interfaces;
using kb21_web.Models;

namespace kb21_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller    
    {
        readonly ITodoInterface db;
        public TodoController(ITodoInterface toDoInterface)
        {
            this.db = toDoInterface;
        }

        
        [HttpGet]
        public IActionResult List()
        {
            return Ok(db.GetAll());
        }  

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Todo item)
        {
            db.Insert(item);
            return Ok(item);            
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            db.Delete(id);
        }


    }
}

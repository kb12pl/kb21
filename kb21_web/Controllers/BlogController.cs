using kb21_web.Models;
using kb21_web.Servicec;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kb21_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogContext _blogContext;
        public BlogController(BlogContext blogContext) 
        { 
            _blogContext=blogContext;
        }        
        [HttpGet]
        
        //public IEnumerable<string> Get()
        public ActionResult<List<Post>> Get()
        {
            Console.WriteLine(Guid.NewGuid());            
            _blogContext.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });            
            _blogContext.SaveChanges();

            var itemLst = _blogContext.Posts.ToList();
            return new List<Post>(itemLst);
            
            //return new string[] { "value1", "value2"};
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    
    }
}

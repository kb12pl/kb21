using Microsoft.AspNetCore.Mvc;
using System.Net;
using kb21_web.Models;
using static kb21_tools.KbLog;
using kb21_tools;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kb21_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        KbLog _log;
        UploadController(KbLog log)
        {
            _log= log;
        }

        // POST api/<UploadController1>
        [HttpPost]
        public async Task<string> Post([FromForm] FileUpload a )        
        {
            ok(123);
            //ok(a.File.Length);
            return "ok";
        }
    }
}

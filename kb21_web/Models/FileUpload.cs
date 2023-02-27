using Microsoft.AspNetCore.Http;
namespace kb21_web.Models
{
    public class FileUpload
    {   
        public int Name { get; set; }
        
        public IFormFile File { get; set; }    

    }
}

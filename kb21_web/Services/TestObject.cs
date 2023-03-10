using kb21_web.Data;
using kb21_web.Models;

namespace kb21_web.Servicec
{
    public interface ITestObject
    {
        string Name { get; }
        void Write(string value);
    }

    public class TestObject:ITestObject
    {
        DataContext db;
        public TestObject(DataContext _db) 
        {
            db = _db;            
            db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            db.SaveChanges();
            Name = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }
        public void Write(string value) 
        {
            //using var db = new BlogContext();
            //db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            //db.SaveChanges();
        }
    }
}

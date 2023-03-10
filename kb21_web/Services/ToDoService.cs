using kb21_web.Interfaces;
using kb21_web.Models;
using kb21_web.Data;
using Polly;

namespace kb21_web.Services
{
    public class TodoService: ITodoInterface
    {
        readonly DataContext dc;
        
        public TodoService(DataContext dc) 
        { 
            this.dc = dc;
        }
        public void Insert(Todo item)
        { 
            dc.Add(item);
            dc.SaveChanges();            
        }
        public IEnumerable<Todo> GetAll() 
        {
            return dc.Todo;
        }
        public void Delete(int id) 
        {            
            var x=dc.Todo.Find(id);
            if (x != null)
            {
                dc.Todo.Remove(x);
                dc.SaveChanges();
            }
        }
    }
}

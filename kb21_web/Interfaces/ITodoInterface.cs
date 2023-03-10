using kb21_web.Models;

namespace kb21_web.Interfaces
{
    public interface ITodoInterface
    {
        void Insert(Todo item);
        IEnumerable<Todo> GetAll();
        void Delete(int id);
    }
}

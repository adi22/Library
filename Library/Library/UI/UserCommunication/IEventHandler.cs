using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;

namespace Library.UI.UserCommunication
{
    public interface IEventHandler<Trepository> 
        where Trepository : class, IRepository<Book>
    {
        void HandleEvents();
    }
}
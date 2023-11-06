using Library.DataAccess.Data.Entities;

namespace Library.ApplicationServices.Components.DatabaseService
{
    public interface IDatabaseService
    {
        Book ReadById(int id);
    }
}
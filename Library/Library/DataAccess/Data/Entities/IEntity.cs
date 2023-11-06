namespace Library.DataAccess.Data.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        int Length { get; set; }
        double Rating { get; set; }
    }

}
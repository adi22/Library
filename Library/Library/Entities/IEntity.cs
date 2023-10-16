namespace Library.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        string Title { get; set; }
        string Author { get; set; }
        string Description { get; set; }
        int Length { get; set; }
        int Rating { get; set; }
        decimal Price { get; set; }
    }

}

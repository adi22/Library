namespace Library.DataAccess.Data.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Length { get; set; }
        public double Rating { get; set; }
    }
}
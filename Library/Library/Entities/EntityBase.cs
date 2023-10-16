namespace Library.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public int Rating { get; set; }
        public decimal Price { get; set; }
    }
}

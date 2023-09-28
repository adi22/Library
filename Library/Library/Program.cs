using Library.Data;
using Library.Entities;
using Library.Repositories;

var bookRepository = new SqlRepository<Book>(new LibraryDbContext());
AddBooks(bookRepository);
AddMagazines(bookRepository);
WriteAllToConsole(bookRepository);

static void AddBooks(IRepository<Book> bookRepository)
{
    bookRepository.Add(new Book() { Title = "It" });
    bookRepository.Add(new Book() { Title = "Pet cementary" });
    bookRepository.Add(new Book() { Title = "Bastion" });
    bookRepository.Save();
}

static void AddMagazines(IWriteRepository<Magazine> magazineRepository)
{
    magazineRepository.Add(new Magazine() { Title = "CD Action" });
    magazineRepository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}
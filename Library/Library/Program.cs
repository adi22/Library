using Library.ApplicationServices.Components.CsvReader;
using Library.ApplicationServices.Components.DatabaseService;
using Library.ApplicationServices.Components.DataProviders;
using Library.ApplicationServices.Components.XmlExporter;
using Library.DataAccess.Data;
using Library.DataAccess.Data.Entities;
using Library.DataAccess.Data.Repositories;
using Library.UI;
using Library.UI.UserCommunication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EventHandler = Library.UI.UserCommunication.EventHandler;

var services = new ServiceCollection();

services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Book>, FileRepository<Book>>();
services.AddSingleton<IBooksProvider, BooksFromFileProvider>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlExporter, XmlExporter>();
services.AddSingleton<IDatabaseService, DatabaseService>();
services.AddSingleton<BooksFromDatabaseProvider>();
services.AddSingleton<UserOptionsInFile>();
services.AddSingleton<UserOptionsInDatabase>();
services.AddSingleton<BooksFromFileProvider>();
services.AddSingleton<IEventHandler, EventHandler>();
services.AddSingleton<IInputValidator, InputValidator>();
services.AddSingleton<IMenu, MenuMain>();
services.AddSingleton<IMenu, MenuForFile>();
services.AddSingleton<IMenu, MenuForDatabase>();
services.AddSingleton<MenuForFile>();
services.AddSingleton<MenuForDatabase>();
services.AddSingleton<MenuMain>();

services.AddDbContext<LibraryDbContext>(options => options
    .UseSqlServer("Data Source=DESKTOP-NBL7B72\\SQLEXPRESS;Initial Catalog=LibraryStorage;Integrated Security=True;TrustServerCertificate=True"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;

app.Run();
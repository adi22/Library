using Library;
using Library.Components.CsvReader;
using Library.Components.DataProviders;
using Library.Data.Entities;
using Library.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Book>, InFileRepository<Book>>();
services.AddSingleton<IBooksProvider, BooksProvider>();
services.AddSingleton<ICsvReader, CsvReader>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();

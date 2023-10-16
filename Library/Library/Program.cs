using Library;
using Library.DataProviders;
using Library.Entities;
using Library.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Book>, InFileRepository<Book>>();
services.AddSingleton<IBooksProvider, BooksProvider>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();

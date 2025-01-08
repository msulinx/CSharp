
using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using MainApp.Dialogs;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()

    .AddSingleton<IFileService>(new FileService("Data", "contacts.json")) 
    .AddSingleton <IContactRepository, ContactRepository>()
    .AddSingleton<IContactService , ContactService>()
    .AddTransient<MenuDialog>()
    .BuildServiceProvider();
    
    var menuService = serviceProvider.GetRequiredService<MenuDialog>();
    menuService.Run();
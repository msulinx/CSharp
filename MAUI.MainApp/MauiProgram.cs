using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using MAUI.MainApp.Pages;
using MAUI.MainApp.ViewModels;
using Microsoft.Extensions.Logging;

namespace MAUI.MainApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        // Pages
        
        builder.Services.AddSingleton<ContactListPage>();
        builder.Services.AddSingleton<AddContactPage>();
        builder.Services.AddSingleton<EditContactPage>();
        
        
        // ViewModels
        builder.Services.AddSingleton<ContactListViewModel>();
        builder.Services.AddSingleton<AddContactViewModel>();
        builder.Services.AddSingleton<EditContactViewModel>();
        
        // Interfaces / Services
        builder.Services.AddSingleton<IContactService, ContactService>();
        builder.Services.AddSingleton<IContactRepository, ContactRepository>();
        builder.Services.AddSingleton<IFileService>(sp =>
        {
            var directoryPath = Path.Combine(FileSystem.AppDataDirectory, "DataFiles");
            var fileName = "contacts.json";

            return new FileService(directoryPath, fileName);
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
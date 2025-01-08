
using MAUI.MainApp.ViewModels;

namespace MAUI.MainApp.Pages;

public partial class AddContactPage : ContentPage
{
    public AddContactPage(AddContactViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
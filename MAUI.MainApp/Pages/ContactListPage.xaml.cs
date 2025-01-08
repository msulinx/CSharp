
using MAUI.MainApp.ViewModels;

namespace MAUI.MainApp.Pages;

public partial class ContactListPage : ContentPage
{
    public ContactListPage(ContactListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
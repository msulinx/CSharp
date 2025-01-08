
using MAUI.MainApp.ViewModels;

namespace MAUI.MainApp.Pages;

public partial class EditContactPage : ContentPage
{
    public EditContactPage(EditContactViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
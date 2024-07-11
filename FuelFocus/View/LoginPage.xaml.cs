using FuelFocus.ViewModel;

namespace FuelFocus.View;

public partial class LoginPage : ContentPage
{
    private bool isPassword = true;
    public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		BindingContext = loginViewModel;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearEntryFields();
    }

    private void ClearEntryFields()
    {
        Password.Text = string.Empty;
        MobileNo.Text = string.Empty;
    }
    private void OnTogglePasswordVisibilityButtonClicked(object sender, EventArgs e)
    {
        isPassword = !isPassword;
        Password.IsPassword = isPassword;
        TogglePasswordVisibilityButton.Source = isPassword ? "visible.png" : "hide.png";
    }
}
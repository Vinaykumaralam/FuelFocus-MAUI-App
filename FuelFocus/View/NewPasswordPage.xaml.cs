using FuelFocus.ViewModel;
using System.Text.RegularExpressions;

namespace FuelFocus.View;

public partial class NewPasswordPage : ContentPage
{
    private string _mobileNumber;
    private bool isPassword = true;
    public NewPasswordPage(string MobileNumber, LoginViewModel loginViewModel)
	{
		InitializeComponent();
        BindingContext = loginViewModel;
        _mobileNumber = MobileNumber;
        MobileNo.Text = _mobileNumber;
	}
    private void OnToggleNewPasswordVisibilityButtonClicked(object sender, EventArgs e)
    {
        isPassword = !isPassword;
        NewPassword.IsPassword = isPassword;
        ToggleNewPasswordVisibilityButton.Source = isPassword ? "visible.png" : "hide.png";
    }
    private void OnToggleConfirmPasswordVisibilityButtonClicked(object sender, EventArgs e)
    {
        isPassword = !isPassword;
        ConfirmPassword.IsPassword = isPassword;
        ToggleConfirmPasswordVisibilityButton.Source = isPassword ? "visible.png" : "hide.png";
    }
}
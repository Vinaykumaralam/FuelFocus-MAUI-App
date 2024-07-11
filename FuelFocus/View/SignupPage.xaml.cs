using FuelFocus.ViewModel;

namespace FuelFocus.View;

public partial class SignupPage : ContentPage
{
    private bool isPassword = true;
    public SignupPage(SignUpViewModel signUpViewModel)
	{
		InitializeComponent();
        BindingContext = signUpViewModel;
	}
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        //await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        var vm = new LoginViewModel();
        await Navigation.PushAsync(new LoginPage(vm));
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ClearEntryFields();
    }

    private void ClearEntryFields()
    {
        UserName.Text = string.Empty;
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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FuelFocus.Model;
using FuelFocus.Services;
using FuelFocus.View;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace FuelFocus.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    private readonly ILoginService _loginService;
    private readonly ISignUpService _signUpService;
    public Command BackCommand { get; set; }
    public Command GoBackCommand { get; set; }
    public ObservableCollection<User> Users { get; } = new();
    public ObservableCollection<Country> CountryCodes { get; } = new();
    public LoginViewModel()
	{
		_loginService = new LoginService();
        _signUpService = new SignUpService();
        BackCommand = new Command(async () => await Back());
        GoBackCommand = new Command(async () => await GoBack());
        LoadCountryCodes();
    }
    [ObservableProperty]
    private string _mobileNo = String.Empty;

    [ObservableProperty]
    private string _password = String.Empty;

    [ObservableProperty]
    private string _newpassword = String.Empty;

    [ObservableProperty]
    private string _confirmpassword = String.Empty;
    [ObservableProperty]
    private Country _selectedCountry;
    [ObservableProperty]
    private string _displayName;
    [ObservableProperty]
    private string _countryName;

    [RelayCommand]
    async Task SignupTap()
    {
        await Shell.Current.GoToAsync(nameof(SignupPage));
    }
    [RelayCommand]
    async Task ForgotPasswordTap()
    {
        await Shell.Current.GoToAsync($"{nameof(ForgotPassPage)}?mobilenumber={MobileNo}");
        //Navigation.PushAsync(new OTPPage(MobileNumber.Text, otp));
    }
    [RelayCommand]
    async Task Login()
    {
        try
        {
            if (MobileNo == String.Empty || Password == String.Empty)
            {
                await Shell.Current.DisplayAlert("Error", "Fields should not be empty", "OK");
                return;
            }
            else
            {
                var mobileregex = new Regex(@"^[1-9]\d{9}$");
                if (!(long.TryParse(MobileNo, out long contacto) && mobileregex.IsMatch(MobileNo)))
                {
                    await Shell.Current.DisplayAlert("Error", "Enter a valid 10 digit mobile number", "Ok");
                    return;
                }
                var user = await _loginService.GetUser(long.Parse(MobileNo));
                if (user == null)
                {
                    await Shell.Current.DisplayAlert("Error", "User Not Found", "Ok");
                    return;
                }
                var userName = user.Name;
                if (VerifyPassword(Password, user.Password))
                {
                    await Shell.Current.DisplayAlert("Success", "Login is Successfull", "Ok");
                    Preferences.Set("IsLoggedIn", true);
                    Preferences.Set("UserMobileNo", MobileNo);
                    Preferences.Set("UserName", userName);
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Invalid Password", "Ok");
                    return;
                }
            }
        }
        catch(Exception ex)
        {
            throw;
        }
    }
    public bool VerifyPassword(string enteredPassword, string storedHash)
    {
        try
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    [RelayCommand]
    async Task PasswordChanged()
    {
        if (Newpassword == String.Empty || Confirmpassword == String.Empty)
        {
            await Shell.Current.DisplayAlert("Error", "Fields should not be empty", "OK");
        }
        var hasUpperCase = new Regex(@"[A-Z]+");
        var hasLowerCase = new Regex(@"[a-z]+");
        var hasDigits = new Regex(@"[0-9]+");
        var hasSpecialChar = new Regex(@"[\W]+");
        var hasWhiteSpace = new Regex(@"\s");
        if (hasUpperCase.IsMatch(Newpassword) && hasLowerCase.IsMatch(Newpassword)
            && hasDigits.IsMatch(Newpassword) && hasSpecialChar.IsMatch(Newpassword)
            && Newpassword.Length >= 8 && !hasWhiteSpace.IsMatch(Newpassword))
        {
            if (Newpassword == Confirmpassword)
            {
                string password = await _signUpService.HashPassword(Newpassword);
                User user = new User
                {
                    Mobile_No = long.Parse(MobileNo),
                    Password = password
                };
                bool result = await _loginService.UpdatePassword(user);
                if(result)
                {
                    await Shell.Current.DisplayAlert("Success", "Password Updated", "Ok");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Something went wrong!!!Try Again.", "Ok");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Password doesn't match", "Ok");
            }

        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "Password doesn't meet the requirement", "Ok");
        }

    }
    private async Task Back()
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync($"{nameof(ForgotPassPage)}");
    }
    private async Task LoadCountryCodes()
    {
        try
        {
            var countryList = await _loginService.GetCountryCodeList();
            foreach(var item in countryList)
            {
                CountryCodes.Add(new Country
                {
                    CountryName = item.CountryName,
                    CountryCode = item.CountryCode
                });
            }
            SelectedCountry = CountryCodes.FirstOrDefault(c => c.CountryCode == 91);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error",$"Something went wrong!!!{ex}","Try Again");
        }
    }
}
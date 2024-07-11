using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FuelFocus.Model;
using FuelFocus.Models;
using FuelFocus.Services;
using FuelFocus.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace FuelFocus.ViewModel;

public partial class SignUpViewModel : BaseViewModel
{
	public readonly ISignUpService _signUpService;
    public readonly ILoginService _loginService;

    public ObservableCollection<User> Users { get; } = new();
    public SignUpViewModel()
	{
		_signUpService = new SignUpService();
        _loginService = new LoginService();
	}
    [ObservableProperty]
    private string _mobileNo = String.Empty;
    [ObservableProperty]
    private string _userName = String.Empty;
    [ObservableProperty]
    private string _password = String.Empty;
    
    [RelayCommand]
    async Task SignUp()
    {
        try
        {
            if (MobileNo == String.Empty || Password == String.Empty || UserName == String.Empty)
            {
                await Shell.Current.DisplayAlert("Error", "Fields should not be empty", "OK");
            }
            else
            {
                var mobileregex = new Regex(@"^[1-9]\d{9}$");
                var hasUpperCase = new Regex(@"[A-Z]+");
                var hasLowerCase = new Regex(@"[a-z]+");
                var hasDigits = new Regex(@"[0-9]+");
                var hasSpecialChar = new Regex(@"[\W]+");
                var hasWhiteSpace = new Regex(@"\s");
                if (!(long.TryParse(MobileNo, out long contactno) && mobileregex.IsMatch(MobileNo)))
                {
                    await Shell.Current.DisplayAlert("Error", "Enter a valid 10 digit mobile number", "Ok");
                    return;
                }
                if (!(hasUpperCase.IsMatch(Password) && hasLowerCase.IsMatch(Password)
                    && hasDigits.IsMatch(Password) && hasSpecialChar.IsMatch(Password)
                    && !hasWhiteSpace.IsMatch(Password) && Password.Length>=8))
                {
                    await Shell.Current.DisplayAlert("Error", "Password must be at least 8 characters long and contain upper case, lower case, digit, and special character, and should not contain any white space.", "Ok");
                    return;
                }
                long contactNo = long.Parse(MobileNo);
                string hashedPassword = await _signUpService.HashPassword(Password);
                User user = new User
                {
                    Name = UserName,
                    Password = hashedPassword,
                    Mobile_No = contactNo,
                    Created_On = DateTime.Now,
                    Updated_On = DateTime.Now
                };
                List<User> userList = new List<User>();
                userList = await _signUpService.GetUsers();
                bool IsExists = userList.Any(x => x.Mobile_No == contactNo);
                if (!IsExists)
                {
                    var result = await _signUpService.AddUser(user);
                    if (result)
                    {
                        var userId = await _loginService.GetUser(long.Parse(MobileNo));
                        if(userId != null)
                        {
                            Vehicle vehicle = new Vehicle
                            {
                                UserID = userId.Id,
                                Mobile = userId.Mobile_No.ToString(),
                                UserName = userId.Name,
                                CarImage = null,
                                UserProfileImage= null,
                                Created_On = DateTime.Now,
                                Updated_On = DateTime.Now
                            };
                            var response = await _signUpService.AddVehicle(vehicle);
                            if (response)
                            {

                                await Shell.Current.DisplayAlert("Success", "User added successfully!!", "Ok");
                                await Shell.Current.GoToAsync("//LoginPage");
                            }
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "User not added", "Ok");
                        return;
                    }
                        
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Mobile No already exists", "Ok");
                    return;
                }
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
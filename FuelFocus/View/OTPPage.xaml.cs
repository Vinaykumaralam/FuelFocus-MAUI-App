using FuelFocus.ViewModel;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using static System.Net.WebRequestMethods;

namespace FuelFocus.View;

public partial class OTPPage : ContentPage
{
    private string _mobileNumber;
    private string _otp;
    private string _code;
	public OTPPage(string Code,string MobileNumber,string otp)
	{
		InitializeComponent();
        LoginViewModel loginViewModel = new LoginViewModel();
        BindingContext = loginViewModel;
        _mobileNumber = MobileNumber;
        _otp = otp;
        _code=Code;
        otpInstructionLabel.Text = $"Enter the OTP sent to {_mobileNumber}";
    }
    private void Confirm_OTP(object sender, EventArgs e)
    {
        LoginViewModel loginViewModel = new LoginViewModel();
        if (Number1.Text == String.Empty || Number2.Text == String.Empty || Number3.Text == String.Empty ||
            Number4.Text == String.Empty || Number5.Text == String.Empty || Number6.Text == String.Empty)
        {
            Shell.Current.DisplayAlert("Error", "Fields should not be empty", "OK");
        }
        string otp_entered = String.Empty;
        otp_entered = Number1.Text + Number2.Text + Number3.Text + Number4.Text + Number5.Text + Number6.Text;
        if (otp_entered == _otp)
            Navigation.PushAsync(new NewPasswordPage(_mobileNumber, loginViewModel));
        else
        {
            Shell.Current.DisplayAlert("Error", "Wrong OTP", "Ok");
            return;
        }
            
    }
    private void Resend_Code_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            string toPhoneNo = _code + _mobileNumber;
            string accountSid = "AC913becb0f79dc9ac0b876ae9f190870e";
            string authToken = "84653ac9c285a760aa9ceddb4c9e273e";
            string otp = GenerateOTP();
            _otp = otp;
            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
                body: $"Your OTP is: {otp}",
                from: new Twilio.Types.PhoneNumber("+12183095832"),
                to: new Twilio.Types.PhoneNumber(toPhoneNo)
            );
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Error", $"Twilio API exception: {ex.Message}", "Ok");
        }
    }
    private string GenerateOTP()
    {
        Random random = new Random();
        int otpNumber = random.Next(100000, 999999);
        return otpNumber.ToString();
    }
    void Number1_TextChanged(object sender, TextChangedEventArgs e)
   {
        if (Number1.Text.Length == 1)
        {
            Number2.Focus();
        }
    }
    void Number2_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (Number2.Text.Length == 1)
        {
            Number3.Focus();
        }
    }
    void Number3_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (Number3.Text.Length == 1)
        {
            Number4.Focus();
        }
    }
    void Number4_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (Number4.Text.Length == 1)
        {
            Number5.Focus();
        }
    }
    void Number5_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (Number5.Text.Length == 1)
        {
            Number6.Focus();
        }
    }
}
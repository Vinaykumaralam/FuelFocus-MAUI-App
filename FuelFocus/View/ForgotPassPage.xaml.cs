using FuelFocus.Model;
using FuelFocus.ViewModel;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using static System.Net.WebRequestMethods;

namespace FuelFocus.View;

[QueryProperty(nameof(MobileNo), "mobilenumber")]
public partial class ForgotPassPage : ContentPage
{
    private string mobileNumber;
    public ForgotPassPage()
	{
		InitializeComponent();
        LoginViewModel loginViewModel = new LoginViewModel();
        BindingContext = loginViewModel;
	}
    public string MobileNo
    {
        get => mobileNumber;
        set
        {
            mobileNumber = value;
            MobileNumber.Text = mobileNumber;
        }
    }
    private void Otp_Generation(object sender, EventArgs e)
	{
        //string otp = GenerateOTP();
        //Navigation.PushAsync(new OTPPage(CodeLabel.Text,MobileNumber.Text, otp));
        try
        {
            if (MobileNumber.Text == String.Empty)
            {
                Shell.Current.DisplayAlert("Error", "Fields should not be empty", "OK");
            }
            string toPhoneNo = CodeLabel.Text + MobileNumber.Text;
            string accountSid = "AC913becb0f79dc9ac0b876ae9f190870e";
            string authToken = "84653ac9c285a760aa9ceddb4c9e273e";
            string otp = GenerateOTP();
            TwilioClient.Init(accountSid, authToken);
            var message = MessageResource.Create(
                body: $"Your OTP is: {otp}",
                from: new Twilio.Types.PhoneNumber("+12183095832"),
                to: new Twilio.Types.PhoneNumber(toPhoneNo)
            );
            Navigation.PushAsync(new OTPPage(CodeLabel.Text,MobileNumber.Text, otp));
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
}
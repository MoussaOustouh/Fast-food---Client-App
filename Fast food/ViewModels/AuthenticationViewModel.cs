using Fast_food.Models.MyModels;
using Fast_food.MyHelpers;
using Fast_food.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fast_food.ViewModels
{
    
    class AuthenticationViewModel : BaseViewModel
    {

        private Client _user;
        public Client user
        {
            get
            {
                return _user;
            }
            set
            {
                SetProperty(ref _user, value);
            }
        }

        public bool IsMale { get; set; }
        public bool IsFemale { get; set; }

        private MyHttpClient myHttpClient = new MyHttpClient();

        public Command SwitchToSingUpCommand { get; }
        public Command SingInCommand { get; }
        public Command SwitchToForgotPasswordCommand { get; }
        public Command SingUpCommand { get; }

        public AuthenticationViewModel()
        {
            Title = "Sing In";

            SwitchToSingUpCommand = new Command(OnSwitchToSingUp);
            SingInCommand = new Command(OnSingIn);
            SwitchToForgotPasswordCommand = new Command(OnSwitchToForgotPassword);
            SingUpCommand = new Command(OnSingUp);

            user = new Client();

            user.firstname = "";
            user.lastname = "";
            user.gender = "";
            user.email = "";
            user.password = "";
            user.phone = "";
            user.photo = "";
            IsMale = true;
        }

        public void OnAppearing()
        {

        }


        public async void OnSingIn()
        {
            try
            {
                JObject jRes = await myHttpClient.sendHttpPostAsyncJson(Constants.ApiSignIn, user);

                if (user.email.Length == 0 || user.password.Length == 0)
                {
                    await App.Current.MainPage.DisplayAlert("Empty fields", "You must fill all the fields.", "OK");
                }
                else if (!MyHelper.IsValidEmail(user.email.Trim()))
                {
                    await App.Current.MainPage.DisplayAlert("Invalid email", "Enter a valid email : example@domain.com", "OK");
                }
                else if ((bool)jRes["HttpClient error"])
                {
                    await App.Current.MainPage.DisplayAlert("Connection failed", "Connection failed.", "OK");
                }
                else if ((bool)jRes["HttpClient parsing error"])
                {

                }
                else
                {
                    if ((bool)jRes["API error"])
                    {
                        await App.Current.MainPage.DisplayAlert("Connection failed", "Connection failed.", "OK");
                    }
                    else if (!(bool)jRes["LoggedIn"])
                    {
                        await App.Current.MainPage.DisplayAlert("Message", (string)jRes["Message"], "OK");
                    }
                    else if ((bool)jRes["LoggedIn"])
                    {
                        await MyHelper.SaveValueForAppAsync("LoggedIn", "true");
                        await MyHelper.SaveValueForAppAsync("JWT", (string)jRes["JWT"]);
                        await MyHelper.SaveValueForAppAsync("id_user", (string)jRes["id_client"]);

                        JObject user_info = (JObject)jRes["user_info"];
                        await MyHelper.SaveValueForAppAsync("user_info", user_info.ToString());
                        user = user_info.ToObject<Client>();

                        App.client = user;

                        //await App.Current.MainPage.DisplayAlert("Connection successful", (string)jRes["JWT"], "OK");

                        App.Current.MainPage = new AppShell();

                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Connection failed", "Connection failed.", "OK");
            }

        }


        public void OnSwitchToSingUp()
        {
            user.password = "";
            App.Current.MainPage.Navigation.PushAsync(new SignUpPage());
        }

        public async void OnSingUp()
        {
            user.firstname = user.firstname.Trim();
            user.lastname = user.lastname.Trim();
            user.email = user.email.Trim();
            user.phone = user.phone.Trim();

            if (IsMale)
            {
                user.gender = "Male";
            }
            else if (IsFemale)
            {
                user.gender = "Female";
            }

            if (user.firstname.Length == 0 || user.lastname.Length == 0 || user.email.Length == 0 || user.password.Length == 0 || user.phone.Length == 0)
            {
                await App.Current.MainPage.DisplayAlert("Empty field", "You must fill all the fields.", "OK");
            }
            else if (!MyHelper.IsValidEmail(user.email))
            {
                await App.Current.MainPage.DisplayAlert("Invalid email", "Enter a valid email : example@domain.com", "OK");
            }
            else
            {
                try
                {
                    JObject jRes = await myHttpClient.sendHttpPostAsyncJson(Constants.ClientCheckEmail, user);
                    
                    if ((bool)jRes["HttpClient error"])
                    {
                        await App.Current.MainPage.DisplayAlert("Connection failed", "Connection failed.", "OK");
                    }
                    else if ((bool)jRes["HttpClient parsing error"])
                    {

                    }
                    else
                    {
                        if (!(bool)jRes["Error"])
                        {
                            if ((bool)jRes["email_already_used"])
                            {
                                await App.Current.MainPage.DisplayAlert("Change the email address", "This email address is in use by another account.", "OK");
                            }
                            else
                            {
                                jRes = await myHttpClient.sendHttpPostAsyncJson(Constants.ApiSignUp, user);
                                if ((bool)jRes["HttpClient error"])
                                {
                                    await App.Current.MainPage.DisplayAlert("Connection failed", "Connection failed.", "OK");
                                }
                                else if ((bool)jRes["HttpClient parsing error"])
                                {

                                }
                                else
                                {
                                    if (!(bool)jRes["Error"])
                                    {
                                        await App.Current.MainPage.Navigation.PopAsync();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Connection failed", "Connection failed.", "OK");
                }
            }



        }

        public void OnSwitchToForgotPassword()
        {
            user.password = "";
            App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }

    }
}

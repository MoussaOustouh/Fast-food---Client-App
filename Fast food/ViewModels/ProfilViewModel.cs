using Fast_food.Models.MyModels;
using Fast_food.MyHelpers;
using Fast_food.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fast_food.ViewModels
{
    public class ProfilViewModel : BaseViewModel
    {
        private MyHttpClient myHttpClient = new MyHttpClient();

        private string _firstname;
        public string firstname { get => _firstname; set => SetProperty(ref _firstname, value); }

        private string _lastname;
        public string lastname { get => _lastname; set => SetProperty(ref _lastname, value); }

        private string _gender;
        public string gender { get => _gender; set => SetProperty(ref _gender, value); }

        private string _email;
        public string email { get => _email; set => SetProperty(ref _email, value); }

        private string _password;
        public string password { get => _password; set => SetProperty(ref _password, value); }

        private string _phone;
        public string phone { get => _phone; set => SetProperty(ref _phone, value); }

        private string _photo;
        public string photo { get => _photo; set => SetProperty(ref _photo, value); }


        private bool _IsMale;
        public bool IsMale { get => _IsMale; set => SetProperty(ref _IsMale, value); }

        private bool _IsFemale;
        public bool IsFemale { get => _IsFemale; set => SetProperty(ref _IsFemale, value); }

        private string _confirmation_code;
        public string confirmation_code { get => _confirmation_code; set => SetProperty(ref _confirmation_code, value); }


        private string _password1;
        public string password1 { get => _password1; set => SetProperty(ref _password1, value); }

        private string _password2;
        public string password2 { get => _password2; set => SetProperty(ref _password2, value); }

        private Client _client;
        public Client client { get => _client; set => SetProperty(ref _client, value); }

        public string clientsPicturesFolder { get => Constants.clientsPicturesFolder; }

        public Command LogoutCommand { get; }
        public Command ChangeInfoCommand { get; }
        public Command ChangePasswordCommand { get; }

        public ProfilViewModel()
        {

            OnAppearing();
            Title = string.Format("{0} {1}", client.firstname, client.lastname);

            LogoutCommand = new Command(Logout);
            ChangeInfoCommand = new Command(async ()=> OnChangeInfo());
            ChangePasswordCommand = new Command(async () => OnChangePassword());

        }

        protected void OnAppearing()
        {
            //await myHttpClient.sendHttpGetAsyncObject<Client>(Constants.ApiGetClientById + "?id_client=" + App.client.id_client);

            client = App.client;
            firstname = client.firstname;
            lastname = client.lastname;

            if (client.gender == "Male")
            {
                IsMale = true; IsFemale = false;
            }
            else
            {
                IsMale = false; IsFemale = true;
            }

            email = client.email;
            phone = client.phone;
            password = "";
            photo = client.photo;

            password1 = "";
            password2 = "";


        }


        private void Logout()
        {
            MyHelper.RemoveAllSavedValuesForApp();

            NavigationPage np = new NavigationPage(new SignInPage());
            App.Current.MainPage = np;
        }

        private async Task OnChangeInfo()
        {
            Client user = client;
            user.firstname = firstname;
            user.lastname = lastname;
            if (IsMale)
            {
                user.gender = "Male";
            }
            else if (IsFemale)
            {
                user.gender = "Female";
            }
            user.email = email;
            user.phone = phone;
            user.password = password;

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
                    JObject jRes = await myHttpClient.sendHttpPutAsyncJson(Constants.ApiPutChangeClientInfo, user);

                    if ((bool)jRes["HttpClient error"])
                    {
                        await App.Current.MainPage.DisplayAlert("Connection failed", "Connection failed.", "OK");
                    }
                    else if ((bool)jRes["HttpClient parsing error"])
                    {

                    }
                    else
                    {
                        if ((bool)jRes["Error"])
                        {
                            await App.Current.MainPage.DisplayAlert((string)jRes["TitleMessage"], (string)jRes["Message"], "OK");
                        }
                        else
                        {
                            await MyHelper.SaveValueForAppAsync("LoggedIn", "true");
                            await MyHelper.SaveValueForAppAsync("JWT", (string)jRes["JWT"]);
                            await MyHelper.SaveValueForAppAsync("id_user", (string)jRes["id_client"]);

                            JObject user_info = (JObject)jRes["user_info"];
                            await MyHelper.SaveValueForAppAsync("user_info", user_info.ToString());
                            user = user_info.ToObject<Client>();

                            App.client = user;
                            client = user;

                            OnAppearing();

                            await App.Current.MainPage.DisplayAlert("Notification", "The information has been changed.", "OK");
                        }
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Connection failed", "Connection failed.", "OK");
                }
            }

        }

        private async Task OnChangePassword()
        {
            if (password1.Length == 0 || password2.Length == 0)
            {
                await App.Current.MainPage.DisplayAlert("Empty field", "You must fill all the fields.", "OK");
            }
            else
            {
                client.password = password1;
                client.confirmation_code = password2;

                JObject jRes = await myHttpClient.sendHttpPutAsyncJson(Constants.ApiPutChangeClientPassword, client);

                if ((bool)jRes["Error"])
                {
                    await App.Current.MainPage.DisplayAlert((string)jRes["TitleMessage"], (string)jRes["Message"], "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Notification", "The password has been changed.", "OK");
                }

            }
        }
    }
}

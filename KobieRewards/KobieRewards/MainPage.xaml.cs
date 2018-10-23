using KobieRewards.Model;
using KobieRewards.Services;
using Newtonsoft.Json;
using System;
using System.Net;
using Xamarin.Forms;

namespace KobieRewards
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            //var authItem = new AuthItem
            //{
            //    Username = usernameEntry.Text,
            //    Password = passwordEntry.Text
            //};

            //var body = JsonConvert.SerializeObject(authItem);
            //var serviceProcessor = new ServiceProcessor();
            //var result = serviceProcessor.PostRequest<UserAccountViewModel>("users/v1/authenticate", null, body);

            //if (result.Status == HttpStatusCode.Unauthorized)
            //{
            //    validationMessage.Text = "An incorrect username or password has been entered";
            //}
            //else if (result.Status != HttpStatusCode.OK)
            //{
            //    validationMessage.Text = "An error occurred processing your login";
            //}

            //validationMessage.Text = "Successfully processed login";
        }
    }
}

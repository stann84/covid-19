using System;
using TestXamarinFirebase.Helper;
using TestXamarinFirebase.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestXamarinFirebase.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotifPage : ContentPage
    {
        Message message = new Message();
        INotification notification;

        public NotifPage()
        {
            InitializeComponent();
            notification = DependencyService.Get<INotification>();
        }

        public async void OnClickSend(Object sender, EventArgs e)
        {
            // send to device: to = deviceId           
            message.to = "/topics/Test";    // send to subscribed topic "test" 
            message.notification.title = title.Text;
            message.notification.body = body.Text;
            message.notification.sound = "Enable";

            notification.SendMessage(message);
            await DisplayAlert("Info", "Votre Notification s'est bien envoyée", "ok");
            await Navigation.PopAsync();
        }
    }
}
using System;
using TestXamarinFirebase.Helper;
using TestXamarinFirebase.Model;
using TestXamarinFirebase.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestXamarinFirebase
{
    /* ----------         Page de navigation           ----------
       ---       Verifie si on est Authentifié ou non         ---
       ---       Si oui on navigue vers la page Profil        ---
       ---  Si non on navigue vers la page d'Authentification ---
       ----------------------------------------------------------  */

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        IAuth auth;
        INotification notification;
        User user = new User();
        public MenuPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            // Crée un service de dépendence pour pouvoir utiliser les méthodes d'authentifications du projet .Android
            auth = DependencyService.Get<IAuth>();
        }
        private void OnLog(Object sender, EventArgs e)
        {
            if (user.Id != null)                        // LogOut
            {
                auth.Logout();
                BtnLog.Text = "LogIn";
                BtnEdit.IsEnabled = false;
                Label.Text = "Veuillez vous Identifier";
                user = new User{ Email = null, Id = null, Nom = null, PhotoUrl = null, Prenom = null, Tel = null};
            }               
            else                                        // LogIn
                Navigation.PushAsync(new AuthPage());
        }
        private void OnEdit(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditPage());
        }
        private void OnPush(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotifPage());
            //notification.CreateNotification("Test Xamarin-Firebase Notification", "Ceci est un test d'envoi d'une notification");
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            // A chaque fois que la page apparait on met à jour le texte d'accueil et le texte du boutton Log
            user = auth.IsAuth();   // Vérifie si on est connecté
            if (user.Id != null)    // si oui :
            {
                BtnLog.Text = "LogOut";
                BtnEdit.IsEnabled = true;
                Label.Text = "Bienvenue " + user.Email;     // Souhaite la bienvenue     
            }
            else                    // si non :
            {
                BtnLog.Text = "LogIn";
                BtnEdit.IsEnabled = false;
                Label.Text = "Veuillez vous Identifier";    // Vous invite à vous identifier
            }
        }
    }
}
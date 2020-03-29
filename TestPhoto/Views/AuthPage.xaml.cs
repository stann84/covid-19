using System;
using TestXamarinFirebase.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestXamarinFirebase.Views
{
    /* ----------         Page d'Authentification           ----------
       ---           Permet de créer un nouveau compte             ---
       ---                  ou de s'identifier                     ---
       ---  une fois identifié on retourne à la page de navigation ---
       ---------------------------------------------------------------  */

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        IAuth auth;
        User user = new User();
        public AuthPage()
        {
            InitializeComponent();
            // Crée un service de dépendence pour pouvoir utiliser les méthodes d'authentifications du projet .Android
            auth = DependencyService.Get<IAuth>();
        }
        public async void LoginClicked(object sender, EventArgs e)  // pour s'identifier
        {
            try
            {
                user = await auth.LoginWithEmailPassword(EmailInput.Text, PasswordInput.Text);
                if (user != null)                   
                    await Navigation.PopAsync();   // retourne à la page menu                   
                else
                    await DisplayAlert("Erreur d'Authentication", "E-mail ou Mot de passe incorect. Veuillez réessayer!", "OK");
            }
            catch (Exception error)
            {
                await DisplayAlert("Erreur de connection", error.Message, "OK");
            }
        }

        public async void SignUpClicked(object sender, EventArgs e) // pour créer un compte
        {
            bool create = await auth.SignUpWithEmailPassword(EmailInput.Text, PasswordInput.Text);

            if (create)
            {
                await DisplayAlert("Création du compte validé", "Bienvenue !", "OK");
                await Navigation.PopAsync();   // retourne à la page menu
            }
            else
            {
                await DisplayAlert("Echec de la création du compte", "Veuillez réessayer !", "OK");
            }
        }
        
    }
}
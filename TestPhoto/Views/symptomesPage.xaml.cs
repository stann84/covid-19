using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestXamarinFirebase.Helper;
using TestXamarinFirebase.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestXamarinFirebase
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SymptomesPage : ContentPage
{

        IAuth auth;                     // Accès à l'Identification
        User user = new User();         // Objet ou seront stockées les données de l'Utilisateur
        FBStorage storage = new FBStorage("tchat-7c40f.appspot.com");  // url vers le storage firebase
        FBDatabase dataBase = new FBDatabase("https://tchat-7c40f.firebaseio.com/"); // url vers la Database firebase


        public SymptomesPage()
    {

            InitializeComponent();

            // Crée un service de dépendence pour pouvoir utiliser les méthodes d'authentifications du projet .Android
            auth = DependencyService.Get<IAuth>();
            user = auth.IsAuth();   // Récuppère l'id & l'email
            VerifyData();           // Vérifie s'il y a des données dans la DataBase concernant l'utilisateur, si oui elles seront inclues et affichées 

        }
        public async void VerifyData()
        {
            user = await dataBase.ReadDatabase(user);

            // si l'utilisateur a déjà rempli les symptomes, on l'affiche
            
            Fievre.IsChecked = user.Fievre;
            Toux.IsChecked = user.Toux;
            MauxDeGorge.IsChecked = user.MauxDeGorge;
            Courbature.IsChecked = user.Courbature;
            Odorat.IsChecked = user.Odorat;
            Fatigue.IsChecked = user.Fatigue;
            GeneRespiratoire.IsChecked = user.GeneRespiratoire;
            Diarrhee.IsChecked = user.Diarrhee;
            Conjonctivite.IsChecked = user.Conjonctivite;
        }







        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            user.Fievre = Fievre.IsChecked;
            user.Toux = Toux.IsChecked;
            user.MauxDeGorge = MauxDeGorge.IsChecked;
            user.Courbature = Courbature.IsChecked;
            user.Odorat = Odorat.IsChecked;
            user.Fatigue = Fatigue.IsChecked;
            user.GeneRespiratoire = GeneRespiratoire.IsChecked;
            user.Diarrhee = Diarrhee.IsChecked;
            user.Conjonctivite = Conjonctivite.IsChecked;
            user.Depiste = Depiste.IsChecked;

            //modifie les données dans la DataBase
            await dataBase.UpdateUser(user);
            await DisplayAlert("Sauvegarde de votre profil réussi", "", "ok");
            await Navigation.PushAsync(new StatistiquesPage());
        }
    }
}
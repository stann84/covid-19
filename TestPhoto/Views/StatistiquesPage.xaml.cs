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
public partial class StatistiquesPage : ContentPage
{
        FBDatabase dataBase = new FBDatabase("https://tchat-7c40f.firebaseio.com/"); // url vers la Database firebase
        User user = new User();         // Objet ou seront stockées les données de l'Utilisateur
        // public int Fievre { get; }

        public StatistiquesPage()
    {
        InitializeComponent();
            VerifyData();

        }
       // Fievre.Text = user.Fievre;

        public async void VerifyData()
        {
            // je lis la base de donnée
            user = await dataBase.ReadDatabase(user);

            // var nbrFievre = user.Fievre;
           // Fievre.Count;
            // Fievre = nbrFievre;


        }

        public async void Geolocalisation_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }

       
        // user.Fievre = Fievre.Text
    }
}
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using TestXamarinFirebase.Helper;
using TestXamarinFirebase.Model;
using Xamarin.Forms;

namespace TestXamarinFirebase
{
    /* ----------          Page d'edition du profil             ----------
       ---  mise en place du plugin Media pour sélectionner une photo  ---
       ---  mise en place du Storage firebase pour y insérer la photo  ---
       ---          renvoie toutes les infos dans la DataBase          ---
       -------------------------------------------------------------------  */

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EditPage : ContentPage
    {
        bool photo = false;             // pour savoir s'il y a une photo de profil
        IAuth auth;                     // Accès à l'Identification
        User user = new User();         // Objet ou seront stockées les données de l'Utilisateur
        MediaFile file;                 // Plugin Media pour uploader une photo
        string tableName = "images";    // nom du dossier ou sont stocké les photos de profil dans firebase
        FBStorage storage = new FBStorage("tchat-7c40f.appspot.com");  // url vers le storage firebase
        FBDatabase dataBase = new FBDatabase("https://tchat-7c40f.firebaseio.com/"); // url vers la Database firebase

        public EditPage()
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

            // si l'utilisateur a déjà rempli son profil, on l'affiche
            if (user.Prenom != null)
                Prenom.Text = user.Prenom;
            if (user.Nom != null)
                Nom.Text = user.Nom;
            if (user.Tel != null)
                Tel.Text = user.Tel;
            if (user.PhotoUrl != null)
            {
                imgff.Source = user.PhotoUrl;
                photo = true;
            }
            
        }
        private async void BtnPick_Clicked(Object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    // on rétréci la photo ci celle-ci est plus grande de 640 pixels
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 640,
                    // on compresse ensuite la photo pour réduire la taille du fichier à stocker
                    CompressionQuality = 50
                });

                if (file == null)   // si l'utilisateur a annulé
                    return;
                else                // si une photo à été chargée
                {
                    // charge l'image dans l'objet Image
                    photo = true;
                    imgff.Source = "";  // Efface l'image précédemment chargée
                    imgChoosed.Source = ImageSource.FromStream(() =>
                    {
                        var imageStram = file.GetStream();
                        return imageStram;
                    });
                }                             
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        // Sauvegarder

        private async void BtnSave_Clicked(Object sender, EventArgs e)
        {
            if(photo)
            {
                if(file != null)  // si on a uploadé une nouvelle image
                {
                    //Sauvegarde la photo dans le storage
                    await storage.UploadFile(file.GetStream(), tableName, Path.GetFileName(file.Path));

                    // récuppère dans la variable path, le lien url de la photo via son nom
                    string path = await storage.DownloadFile(tableName, Path.GetFileName(file.Path));

                    // si la photo de profil a changé on Efface l'ancienne du storage
                    if (user.PhotoUrl != null && user.PhotoUrl != path)
                    {
                        await storage.DeleteFile(tableName, user.PhotoName);
                    }
                    user.PhotoUrl = path;   // Enregistre le lien de la (nouvelle) photo de profil
                    user.PhotoName = Path.GetFileName(file.Path); // Enregistre le nom de la photo
                }               
            }
            
            // inscrit les données dans l'objet utilisateur
            user.Prenom = Prenom.Text;
            user.Nom = Nom.Text;
            user.Tel = Tel.Text;

            //modifie les données dans la DataBase
            await dataBase.UpdateUser(user);
            await DisplayAlert("Sauvegarde de votre profil réussi", "", "ok");
            await Navigation.PushAsync(new SymptomesPage());
        }

    }
}

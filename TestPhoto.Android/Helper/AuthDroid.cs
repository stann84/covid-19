using System;
using System.Threading.Tasks;
using Firebase.Auth;
using TestXamarinFirebase.Droid;
using TestXamarinFirebase.Model;
using Xamarin.Forms;


[assembly: Dependency(typeof(AuthDroid))]
namespace TestXamarinFirebase.Droid
{
    class AuthDroid : IAuth
    {
        public async Task<User> LoginWithEmailPassword(string email, string password)
        {
            User util = new User();
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                util.Id = user.User.Uid;
                util.Email = user.User.Email;
                return util;              
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return null;
            }
        }

        public async Task<bool> SignUpWithEmailPassword(string email, string password)
        {
            try
            {
                bool create = FirebaseAuth.Instance.CreateUserWithEmailAndPassword(email, password).IsComplete;
                await Task.Delay(2500);     // délai, seul moyen que j'ai trouvé pour l'instant pour attendre la fin de la création du compte
                bool util = FirebaseAuth.Instance.CurrentUser != null;
                return util;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public User IsAuth()
        {
            User util = new User();
            var user = FirebaseAuth.Instance.CurrentUser;
            if (user != null)
            {
                util.Email = user.Email;
                util.Id = user.Uid;
                return util;
            }
            else
                return new User() { Id = null, Email = null, Nom = null, PhotoUrl = null, Prenom = null, Tel = null };
        }
        
        public void Logout()
        {
            var auth = FirebaseAuth.Instance;
            auth.SignOut();
        }
    }
}
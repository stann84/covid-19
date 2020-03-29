using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestXamarinFirebase.Model;

namespace TestXamarinFirebase
{
    public interface IAuth
    {
        Task<User> LoginWithEmailPassword(string email, string password);
        Task<bool> SignUpWithEmailPassword(string email, string password);
        User IsAuth();
        void Logout();
    }

}

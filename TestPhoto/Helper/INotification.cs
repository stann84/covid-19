using System;
using System.Collections.Generic;
using System.Text;
using TestXamarinFirebase.Model;

namespace TestXamarinFirebase.Helper
{
    public interface INotification
    {
        void CreateNotification(String title, String message);
        void SendMessage(Message message);
    }
}

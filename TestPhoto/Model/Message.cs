using System;
using System.Collections.Generic;
using System.Text;

namespace TestXamarinFirebase.Model
{
    public class Message
    {
        public string to { get; set; }
        public Notif notification = new Notif();
}
    public class Notif
    {
        public string title { get; set; }
        public string body { get; set; }
        public string sound { get; set; }
    }
}

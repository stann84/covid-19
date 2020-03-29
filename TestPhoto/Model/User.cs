using System;
using System.Collections.Generic;
using System.Text;

namespace TestXamarinFirebase.Model
{
    public class User
    {
        // Identitee
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoName { get; set; }
        public string Tel { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        // Symptomes
        public bool Fievre { get; set; }
        public bool Toux { get; set; }
        public bool MauxDeGorge { get; set; }
        public bool Courbature { get; set; }
        public bool Odorat { get; set; }
        public bool Fatigue { get; set; }
        public bool GeneRespiratoire { get; set; }
        public bool Diarrhee { get; set; }
        public bool Conjonctivite { get; set; }
        //Geolocalisation
        public string Longitude { get; set; }
        public string Latitude { get; set; }
    }
}

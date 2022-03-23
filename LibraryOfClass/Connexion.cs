using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryOfClass
{
    public class Connexion
    {
        private bool laConnexion;
        private User userActif;


        public Connexion()
        {
            //laConnexion = new User();
            LaConnexion = false;
            UserActif = null;
        }

        public bool LaConnexion { get => laConnexion; set => laConnexion = value; }
        public User UserActif { get => userActif; set => userActif = value; }


        public bool verifConnexion()
        {
            bool leRetour = false;
            if (LaConnexion)
            {
                leRetour = true;
            }
            return leRetour;
        }

        public void creerConnexion(User unUser)
        {
            LaConnexion = true;
            UserActif = unUser;
        }

        public void deconnexion()
        {
            LaConnexion = false;
            UserActif = null;
        }

        public string getUserStatut()
        {
            string statut = UserActif.Statut.ToLower();
            return statut;
        }



    }
}

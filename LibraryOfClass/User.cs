using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryOfClass
{
    public class User
    {
        private string identifiant;
        private string statut;
        private string mail;


        public User(string un_identifiant, string un_statut, string un_mail)
        {
            Identifiant = un_identifiant;
            Statut = un_statut;
            Mail = un_mail;
        }

        public string Identifiant { get => identifiant; set => identifiant = value; }
        public string Statut { get => statut; set => statut = value; }
        public string Mail { get => mail; set => mail = value; }
    }
}

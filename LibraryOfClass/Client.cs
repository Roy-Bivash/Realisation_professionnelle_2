using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryOfClass
{
    public class Client
    {
        private int id;
        private string identifiant;
        private string statut;
        private string mail;
        private Abonnement lAbonnement;

        public Client(int unId, string unIdentifiant, string unStatut, string unMail, Abonnement unAbonnement)
        {
            Id = unId;
            Identifiant = unIdentifiant;
            Statut = unStatut;
            Mail = unMail;
            LAbonnement = unAbonnement;
        }

        public int Id { get => id; set => id = value; }
        public string Identifiant { get => identifiant; set => identifiant = value; }
        public string Statut { get => statut; set => statut = value; }
        public string Mail { get => mail; set => mail = value; }
        public Abonnement LAbonnement { get => lAbonnement; set => lAbonnement = value; }
    }
}

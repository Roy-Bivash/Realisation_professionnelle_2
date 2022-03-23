using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryOfClass
{
    public class Type_abonnement
    {
        private int id;
        private string nom;
        private int nbMaxFacture;
        private int nbMaxDevis;
        private double prix;

        public Type_abonnement(int unId, string unNom, int unNbMaxFacture, int unNbMaxDevis, double unPrix)
        {
            Id = unId;
            Nom = unNom;
            NbMaxFacture = unNbMaxFacture;
            NbMaxDevis = unNbMaxDevis;
            Prix = unPrix;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public int NbMaxFacture { get => nbMaxFacture; set => nbMaxFacture = value; }
        public int NbMaxDevis { get => nbMaxDevis; set => nbMaxDevis = value; }
        public double Prix { get => prix; set => prix = value; }
    }
}

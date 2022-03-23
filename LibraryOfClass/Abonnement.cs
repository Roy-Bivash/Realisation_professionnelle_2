using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryOfClass
{
    public class Abonnement
    {
        private string statut_abonnement;
        private string date_abonnement;
        private string date_fin;
        private Type_abonnement leTypeAbo;
        private int nb_fact_restant;
        private int nb_devis_restant;

        public Abonnement(string unStatut, string uneDateAbonnement, string uneDateFin, Type_abonnement unTypeAbo, int unNb_fact_restant, int unNb_devis_restant)
        {
            Statut_abonnement = unStatut;
            Date_abonnement = uneDateAbonnement;
            Date_fin = uneDateFin;
            LeTypeAbo = unTypeAbo;
            Nb_fact_restant = unNb_fact_restant;
            Nb_devis_restant = unNb_devis_restant;
        }

        public string Statut_abonnement { get => statut_abonnement; set => statut_abonnement = value; }
        public string Date_abonnement { get => date_abonnement; set => date_abonnement = value; }
        public string Date_fin { get => date_fin; set => date_fin = value; }
        public Type_abonnement LeTypeAbo { get => leTypeAbo; set => leTypeAbo = value; }
        public int Nb_fact_restant { get => nb_fact_restant; set => nb_fact_restant = value; }
        public int Nb_devis_restant { get => nb_devis_restant; set => nb_devis_restant = value; }
    }
}

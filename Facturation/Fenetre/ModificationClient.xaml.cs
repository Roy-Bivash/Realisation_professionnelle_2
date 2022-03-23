using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using LibraryOfClass;

namespace Facturation.Fenetre
{
    /// <summary>
    /// Logique d'interaction pour ModificationClient.xaml
    /// </summary>
    public partial class ModificationClient : Window
    {
        private Client leClient;
        private bool abonnementClient;
        GstBDD gstBDD;

        public ModificationClient(Client unClient)
        {
            InitializeComponent();
            leClient = unClient;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gstBDD = new GstBDD();
            txtId.Text = leClient.Identifiant.ToString();
            txtMail.Text = leClient.Mail.ToString();
            
            if (leClient.LAbonnement != null)
            {
                if (leClient.LAbonnement.Statut_abonnement != "true")
                {
                    txtAbo.Text = "L'abonnement est expiré";
                    txtNbDevisRestant.Text = "Aucun";
                    txtNbFacturesRestant.Text = "Aucun";
                    btnModifierAjouterAbo.Content = "Ajouter";
                    abonnementClient = false; //le client n'a pas d'abonnement
                }
                else
                {
                    txtAbo.Text = "Abonnement actif du : " + leClient.LAbonnement.Date_abonnement.ToString() + " au : " + leClient.LAbonnement.Date_fin.ToString();
                    txtNbDevisRestant.Text = leClient.LAbonnement.Nb_devis_restant.ToString();
                    txtNbFacturesRestant.Text = leClient.LAbonnement.Nb_fact_restant.ToString();
                    btnModifierAjouterAbo.Content = "Modifier";
                    
                    abonnementClient = true; //le client est abonnée
                }
            }
            else
            {
                txtAbo.Text = "Le client n'est pas abonée";
                txtNbDevisRestant.Text = "Aucun";
                txtNbFacturesRestant.Text = "Aucun";
                btnModifierAjouterAbo.Content = "Ajouter";
                abonnementClient = false; //le client n'a pas d'abonnement
            }
        }


        private void btnModifierId_Click(object sender, RoutedEventArgs e)
        {
            ModifierIdClient fenetreModiferIdClient = new ModifierIdClient(leClient.Id, txtId.Text);
            fenetreModiferIdClient.Show();
            this.Close();
        }

        private void btnModifierMail_Click(object sender, RoutedEventArgs e)
        {
            ModifierMailClient fenetreModifierMailClient = new ModifierMailClient(leClient.Id, txtMail.Text);
            fenetreModifierMailClient.Show();
            this.Close();
        }
        private void btnModifierAjouterAbo_Click(object sender, RoutedEventArgs e)
        {
            if (btnModifierAjouterAbo.Content.ToString() == "Ajouter")
            {
                //MessageBox.Show("Ajouter");
                AjouterAbonnementClient fenetreAjouterAbonnementClient = new AjouterAbonnementClient(leClient.Id);
                fenetreAjouterAbonnementClient.Show();

                this.Close();
            }
            else
            {
                if (btnModifierAjouterAbo.Content.ToString() == "Modifier")
                {
                    MessageBox.Show("Modifier");
                    ModifierAbonnementClient fenetreModifierAbonnementClient = new ModifierAbonnementClient(leClient.Id);
                    fenetreModifierAbonnementClient.Show();

                    this.Close();
                }
            }
        }

        private void btnModifierNbDevisRestant_Click(object sender, RoutedEventArgs e)
        {
            if (abonnementClient)
            {
                ModifierNbDevis fenetreModifierNbDevis = new ModifierNbDevis(leClient.Id, leClient.LAbonnement.Nb_devis_restant);
                fenetreModifierNbDevis.Show();
                this.Close();
                //string input = Interaction.InputBox("Prompt", "Title", "Default", x_coordinate, y_coordinate);
            }
            else
            {
                MessageBox.Show("L'utilisateur n'est pas abonné par consequant ne peu pas avoir de Devis", "Pas d'abonnement", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnModifierNbFactRestant_Click(object sender, RoutedEventArgs e)
        {
            if (abonnementClient)
            {
                ModifierNbFactures fenetreModifierNbFactures = new ModifierNbFactures(leClient.Id, leClient.LAbonnement.Nb_fact_restant);
                fenetreModifierNbFactures.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("L'utilisateur n'est pas abonné par consequant ne peu pas avoir de Factures", "Pas d'abonnement", MessageBoxButton.OK ,MessageBoxImage.Information);
            }
        }

        private void btnSuppAbonnement_Click(object sender, RoutedEventArgs e)
        {
            if (abonnementClient)
            {
                gstBDD.suppAbonnementClient(leClient.Id);
                MessageBox.Show("L'abonnement à bien été supprimé", "Confirmation", MessageBoxButton.OK);

                this.Close();
            }
            else
            {
                MessageBox.Show("L'utilisateur n'est pas abonné", "Pas d'abonnement", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

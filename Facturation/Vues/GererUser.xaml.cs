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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryOfClass;
using Facturation.Fenetre;

namespace Facturation.Vues
{
    /// <summary>
    /// Logique d'interaction pour GererUser.xaml
    /// </summary>
    public partial class GererUser : Page
    {
        public GererUser()
        {
            InitializeComponent();
        }
        GstBDD gstBDD;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            gstBDD = new GstBDD();

            //Vider les champs de text :
            lstBoxUsers.ItemsSource = gstBDD.getAllClients();
            txtId.Text = "";
            txtMail.Text = "";
            txtAbonnement.Text = "";
            txtDebutAbonnement.Text = "";
            txtFinAbonnement.Text = "";
            txtNbDevisRestant.Text = "";
            txtNbFacturesRestant.Text = "";
            txtGlobale.Text = "Veuillez selectionnez un utilisateur";
        }

        private void lstBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBoxUsers.SelectedItem != null)
            {
                btnModifierClient.Visibility = Visibility.Visible;//Rendre le bouton Modifier visible a la selection d'un client
                txtGlobale.Text = "";
                txtId.Text = "Identifiant : " + (lstBoxUsers.SelectedItem as Client).Identifiant.ToString();
                txtMail.Text = "Mail : " + (lstBoxUsers.SelectedItem as Client).Mail.ToString();

                //Si la class abonnement du client n'est pas vide :
                if ((lstBoxUsers.SelectedItem as Client).LAbonnement != null)
                {
                    //Si le client a un abonnement expiré :
                    if ((lstBoxUsers.SelectedItem as Client).LAbonnement.Statut_abonnement != "true")
                    {
                        txtAbonnement.Text = "Abonnement (Expiré): " + (lstBoxUsers.SelectedItem as Client).LAbonnement.LeTypeAbo.Nom.ToString();
                    }
                    //Si le client a un abonnement actif :
                    else
                    {
                        txtAbonnement.Text = "Abonnement : " + (lstBoxUsers.SelectedItem as Client).LAbonnement.LeTypeAbo.Nom.ToString();
                    }
                    txtDebutAbonnement.Text = "Debut Abonnement : " + (lstBoxUsers.SelectedItem as Client).LAbonnement.Date_abonnement.ToString();
                    txtFinAbonnement.Text = "Fin Abonnement : " + (lstBoxUsers.SelectedItem as Client).LAbonnement.Date_fin.ToString();
                    txtNbDevisRestant.Text = "Nombre de devis restant : " + (lstBoxUsers.SelectedItem as Client).LAbonnement.Nb_devis_restant.ToString();
                    txtNbFacturesRestant.Text = "Nombre de factures restant : " + (lstBoxUsers.SelectedItem as Client).LAbonnement.Nb_fact_restant.ToString();
                }
                //Si la class abonnement du client est vide :
                else
                {
                    txtAbonnement.Text = "Pas abonné";
                    txtDebutAbonnement.Text = "";
                    txtFinAbonnement.Text = "";
                    txtNbDevisRestant.Text = "";
                    txtNbFacturesRestant.Text = "";
                }

            }

        }

        private void btnModifierClient_Click(object sender, RoutedEventArgs e)
        {
            if (lstBoxUsers.SelectedItem != null)
            {
                ModificationClient fenetreModifClient = new ModificationClient(lstBoxUsers.SelectedItem as Client);
                fenetreModifClient.Show();
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un client à modifier", "Erreur de selection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Re remplie la lstBoxUsers :
            lstBoxUsers.ItemsSource = gstBDD.getAllClients();

            btnModifierClient.Visibility = Visibility.Hidden;//Rendre le bouton Invisible lorsque rien n'est selectionner

            //Vider les Chmaps de text :
            txtId.Text = "";
            txtMail.Text = "";
            txtAbonnement.Text = "";
            txtDebutAbonnement.Text = "";
            txtFinAbonnement.Text = "";
            txtNbDevisRestant.Text = "";
            txtNbFacturesRestant.Text = "";
            txtGlobale.Text = "Veuillez selectionnez un utilisateur";
        }



        private void barDeRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show("hello");
            if(barDeRecherche.Text != "")
            {
                lstBoxUsers.ItemsSource = gstBDD.getAllClientsByLogin(barDeRecherche.Text);
            }
            else
            {
                lstBoxUsers.ItemsSource = gstBDD.getAllClients();
            }

        }
    }
}

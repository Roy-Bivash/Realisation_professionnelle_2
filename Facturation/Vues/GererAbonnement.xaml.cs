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
    /// Logique d'interaction pour GererAbonnement.xaml
    /// </summary>
    public partial class GererAbonnement : Page
    {
        public GererAbonnement()
        {
            InitializeComponent();
        }
        GstBDD gstBDD;
        GstGlobale gstGlobale;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            gstBDD = new GstBDD();
            gstGlobale = new GstGlobale();
            lstBoxAbonnements.ItemsSource = gstBDD.getAllTypeAbonnement();
            cadreSelection.Visibility = Visibility.Hidden;
            btnSupprimer.Visibility = Visibility.Hidden;
        }

        private void lstBoxAbonnements_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstBoxAbonnements.SelectedItem != null)
            {
                cadreSelection.Visibility = Visibility.Visible;
                btnSupprimer.Visibility = Visibility.Visible;

                Type_abonnement leTypeAbo = lstBoxAbonnements.SelectedItem as Type_abonnement;
                txtId.Text = "Numero : " + leTypeAbo.Id.ToString();
                txtNomAbo.Text = "Nom de l'abonnement : " + leTypeAbo.Nom.ToString();
                txtNbMaxDevis.Text = "Nombre maximum de devis : " + leTypeAbo.NbMaxDevis.ToString();
                txtNbMaxFact.Text = "Nombre maximum de factures : " + leTypeAbo.NbMaxFacture.ToString();
                txtPrix.Text = "Prix : " + leTypeAbo.Prix.ToString();
            }
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            
            AjouterTypeAbo fenetreAjouterTypeAbo = new AjouterTypeAbo();
            fenetreAjouterTypeAbo.Show();
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (lstBoxAbonnements.SelectedItem != null)
            {
                var suppression = MessageBox.Show("Etes vous sur de vouloir supprimer ?", "Supprimer", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (suppression == MessageBoxResult.OK)
                {
                    int idTypeAboSelect = (lstBoxAbonnements.SelectedItem as Type_abonnement).Id;
                    if (!gstGlobale.verifTypeAboUtiliser(idTypeAboSelect, gstBDD.getAllClients()))
                    {
                        //Dans le cas ou l'abonnement n'est utilisé par aucun client alors la suppression de vient possible :
                        gstBDD.suppTypeAbonnement(idTypeAboSelect);
                    }
                    else
                    {
                        MessageBox.Show("Ce type d'abonnement est utilisé par un client par conséquant ne peut pas être supprimer", "Suppression impossible", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un abonnement", "Erreur de selection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnModifierNom_Click(object sender, RoutedEventArgs e)
        {
            if(lstBoxAbonnements.SelectedItem != null)
            {
                Type_abonnement typeAboSelect = (lstBoxAbonnements.SelectedItem as Type_abonnement);
                ModifierNomTypeAbo fenetreModifierNomTypeAbo = new ModifierNomTypeAbo(typeAboSelect.Id, typeAboSelect.Nom);
                fenetreModifierNomTypeAbo.Show();
            }
        }

        private void btnModifierMaxFac_Click(object sender, RoutedEventArgs e)
        {
            if (lstBoxAbonnements.SelectedItem != null)
            {
                Type_abonnement typeAboSelect = lstBoxAbonnements.SelectedItem as Type_abonnement;
                ModifierMaxFactTypeAbo fenetreModifierMaxFactTypeAbo = new ModifierMaxFactTypeAbo(typeAboSelect.Id, typeAboSelect.NbMaxFacture);
                fenetreModifierMaxFactTypeAbo.Show();
            }
        }

        private void btnModifierMaxDevis_Click(object sender, RoutedEventArgs e)
        {
            if (lstBoxAbonnements.SelectedItem != null)
            {
                Type_abonnement typeAboSelect = lstBoxAbonnements.SelectedItem as Type_abonnement;
                ModifierMaxDevisTypeAbo fenetreModifierMaxDevisTypeAbo = new ModifierMaxDevisTypeAbo(typeAboSelect.Id, typeAboSelect.NbMaxDevis);
                fenetreModifierMaxDevisTypeAbo.Show();
            }
        }

        private void btnModifierPrix_Click(object sender, RoutedEventArgs e)
        {
            if (lstBoxAbonnements.SelectedItem != null)
            {
                Type_abonnement typeAboSelect = lstBoxAbonnements.SelectedItem as Type_abonnement;
                ModifierPrixTypeAbo fenetreModifierPrixTypeAbo = new ModifierPrixTypeAbo(typeAboSelect.Id, typeAboSelect.Prix);
                fenetreModifierPrixTypeAbo.Show();
            }
        }

        private void btnActualiser_Click(object sender, RoutedEventArgs e)
        {
            gstBDD = new GstBDD();
            lstBoxAbonnements.ItemsSource = gstBDD.getAllTypeAbonnement();
            cadreSelection.Visibility = Visibility.Hidden;
            btnSupprimer.Visibility = Visibility.Hidden;


            //Pour eviter que l'utilsateur clique sur supprimer ou modifier et se retrouve a modifier sa precedente selection :
            if (lstBoxAbonnements.SelectedItem != null)
            {
                lstBoxAbonnements.SelectedItem = null;
            }
        }
    }
}

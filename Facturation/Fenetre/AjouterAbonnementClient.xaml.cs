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
using LibraryOfClass;

namespace Facturation.Fenetre
{
    /// <summary>
    /// Logique d'interaction pour AjouterAbonnementClient.xaml
    /// </summary>
    public partial class AjouterAbonnementClient : Window
    {
        private int idClient;
        GstBDD gstBDD;

        public AjouterAbonnementClient(int lIdClient)
        {
            InitializeComponent();
            idClient = lIdClient;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gstBDD = new GstBDD();
            cboTypeAbonnement.ItemsSource = gstBDD.getAllTypeAbonnement();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if(cboDuree.SelectedItem != null)
            {
                if (cboTypeAbonnement.SelectedItem != null)
                {
                    int laDuree = Convert.ToInt16(cboDuree.SelectionBoxItem);
                    Type_abonnement leTypeAbonnement = cboTypeAbonnement.SelectedItem as Type_abonnement;

                    gstBDD.ajouterAbonnementClient(idClient, laDuree, leTypeAbonnement);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Veuillez selectionner un type d'abonnement", "Erreur de selection", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une durée", "Erreur de selection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

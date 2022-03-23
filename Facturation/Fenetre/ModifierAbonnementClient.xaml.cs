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
    /// Logique d'interaction pour ModifierAbonnementClient.xaml
    /// </summary>
    public partial class ModifierAbonnementClient : Window
    {
        private int idClient;
        GstBDD gstBDD;

        public ModifierAbonnementClient(int lIdClient)
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
            if (cboTypeAbonnement.SelectedItem != null)
            {
                //MessageBox.Show((cboTypeAbonnement.SelectedItem as Type_abonnement).Nom.ToString());

                Type_abonnement leTypeAbonnement = cboTypeAbonnement.SelectedItem as Type_abonnement;
                gstBDD.modifierTypeAbonnementClient(idClient, leTypeAbonnement.Id);

                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez selectionner un type d'abonnement", "Erreur de selection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

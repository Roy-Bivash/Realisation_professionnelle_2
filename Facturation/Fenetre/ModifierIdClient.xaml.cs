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
    /// Logique d'interaction pour ModifierIdClient.xaml
    /// </summary>
    public partial class ModifierIdClient : Window
    {
        private int idClient;
        private string identifiant;
        GstBDD gstBDD;

        public ModifierIdClient(int lIdClient, string id)
        {
            InitializeComponent();
            txtIdModifier.Text = id.ToString();

            idClient = lIdClient;
            identifiant = id.ToString();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdModifier.Text != "")
            {
                if (txtIdModifier.Text != identifiant)
                {

                    gstBDD = new GstBDD();
                    gstBDD.modifierIdentifiantClient(idClient, txtIdModifier.Text);


                    this.Close();
                }
                else
                {
                    MessageBox.Show("Vous n'avez effectué aucune modification", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir l'identifiant", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

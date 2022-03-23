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
    /// Logique d'interaction pour ModifierNomTypeAbo.xaml
    /// </summary>
    public partial class ModifierNomTypeAbo : Window
    {
        private string oldNom;
        private int idTypeAbo;
        GstBDD gstBDD;

        public ModifierNomTypeAbo(int lIdTypeAbo, string leNom)
        {
            InitializeComponent();


            idTypeAbo = lIdTypeAbo;
            oldNom = leNom;
            txtNomAbo.Text = leNom.ToString();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomAbo.Text != "")
            {
                if (txtNomAbo.Text != oldNom)
                {
                    gstBDD = new GstBDD();

                    gstBDD.modifierNomTypeAbonnement(idTypeAbo, txtNomAbo.Text);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Vous n'avez effectué aucune modification", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir un nom", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

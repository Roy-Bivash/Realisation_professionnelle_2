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
    /// Logique d'interaction pour ModifierNbFactures.xaml
    /// </summary>
    public partial class ModifierNbFactures : Window
    {
        private int idClient;
        private int nbFactures;

        GstBDD gstBDD;
        public ModifierNbFactures(int lIdClient, int nbFacturesRestant)
        {
            InitializeComponent();

            idClient = lIdClient;
            nbFactures = nbFacturesRestant;

            txtNbModifier.Text = nbFactures.ToString();
            gstBDD = new GstBDD();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (txtNbModifier.Text != "")
            {
                //Utiliser un try catch pour eviter les saisie de string a la place des int
                try
                {
                    int nbFacturesSasie = Convert.ToInt32(txtNbModifier.Text);
                    if (nbFacturesSasie != nbFactures)
                    {
                        gstBDD.modifierNbFactRestant(idClient, nbFacturesSasie);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Vous avez fait aucune modification", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Veuillez saisir des chiffres");
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir le nombre de Factures", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}

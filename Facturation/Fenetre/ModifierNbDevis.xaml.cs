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
    /// Logique d'interaction pour ModifierNbDevis.xaml
    /// </summary>
    public partial class ModifierNbDevis : Window
    {
        private int idClient;
        private int nbDevis;

        GstBDD gstBDD;

        public ModifierNbDevis(int lIdClient, int nbDevisRestant)
        {
            InitializeComponent();

            idClient = lIdClient;
            nbDevis = nbDevisRestant;

            txtNbModifier.Text = nbDevis.ToString();
            gstBDD = new GstBDD();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (txtNbModifier.Text != "")
            {
                //Utiliser un try catch pour eviter les saisie de string a la place des int
                try
                {
                    int nbDevisSasie = Convert.ToInt32(txtNbModifier.Text);
                    if(nbDevisSasie != nbDevis)
                    {
                        gstBDD.modifierNbDevisRestant(idClient, nbDevisSasie);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Vous avez fait aucune modification", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch(System.Exception)
                {
                    MessageBox.Show("Veuillez saisir des chiffres");
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir le nombre de Devis", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}

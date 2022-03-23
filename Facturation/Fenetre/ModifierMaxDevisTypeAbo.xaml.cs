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
    /// Logique d'interaction pour ModifierMaxDevisTypeAbo.xaml
    /// </summary>
    public partial class ModifierMaxDevisTypeAbo : Window
    {
        private int idTypeAbo;
        private int oldNbDevis;

        GstBDD gstBDD;

        public ModifierMaxDevisTypeAbo(int lIdTypeAbo, int leNbDevis)
        {
            InitializeComponent();

            InitializeComponent();
            idTypeAbo = lIdTypeAbo;
            oldNbDevis = leNbDevis;

            txtNbModifier.Text = leNbDevis.ToString();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (txtNbModifier.Text != "")
            {
                //Utiliser un try catch pour eviter les saisie de string a la place des int
                try
                {
                    int nbFacturesSasie = Convert.ToInt32(txtNbModifier.Text);
                    if (nbFacturesSasie != oldNbDevis)
                    {
                        gstBDD = new GstBDD();
                        gstBDD.modifierNbMaxDevisTypeAbonnementById(idTypeAbo, nbFacturesSasie);
                        MessageBox.Show("Modification effectué, veuillez actualiser pour voire la modifcation", "Succés", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Vous avez fait aucune modification", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Veuillez saisir un nombre entier");
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir le nombre de Devis maximum", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

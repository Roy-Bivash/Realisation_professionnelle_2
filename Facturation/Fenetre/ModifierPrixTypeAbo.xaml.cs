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
    /// Logique d'interaction pour ModifierPrixTypeAbo.xaml
    /// </summary>
    public partial class ModifierPrixTypeAbo : Window
    {
        private int idClient;
        private string prix;

        GstBDD gstBDD;
        public ModifierPrixTypeAbo(int lIdClient, double unPrix)
        {
            InitializeComponent();

            idClient = lIdClient;
            prix = unPrix.ToString();

            txtNbModifier.Text = unPrix.ToString();
            gstBDD = new GstBDD();
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (txtNbModifier.Text != "")
            {
                var prixSasie = txtNbModifier.Text;
                //MessageBox.Show(prixSasie.Replace(',','.'));

                //Utiliser un try catch pour eviter les saisie de string a la place des int
                try
                {
                    //double prixSasie = Convert.ToDouble(txtNbModifier.Text);
                    if (prixSasie != prix)
                    {
                        gstBDD.modfierPrixTypeAbo(idClient, prixSasie);

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
                MessageBox.Show("Veuillez saisir le prix", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
    }
}

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
    /// Logique d'interaction pour ModifierMailClient.xaml
    /// </summary>
    public partial class ModifierMailClient : Window
    {
        private int idClient;
        private string mail;
        GstBDD gstBDD;

        public ModifierMailClient(int lIdClient, string OldMail)
        {
            InitializeComponent();

            txtMailModifier.Text = OldMail.ToString();

            idClient = lIdClient;
            mail = OldMail;
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if (txtMailModifier.Text != "")
            {
                if (txtMailModifier.Text != mail)
                {

                    gstBDD = new GstBDD();
                    gstBDD.modifierMailClient(idClient, txtMailModifier.Text);


                    this.Close();
                }
                else
                {
                    MessageBox.Show("Vous n'avez effectué aucune modification", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir un mail", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}

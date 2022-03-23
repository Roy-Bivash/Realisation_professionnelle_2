using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Facturation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        Connexion cnx;
        GstBDD gstBDD;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cnx = new Connexion();
            gstBDD = new GstBDD();

            if (cnx.verifConnexion())
            {
                Application.Current.MainWindow.Close();
            }

        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            if (txtIdentifiant.Text != "")
            {
                if (txtMDP.Text != "")
                {
                    string saisie_id = txtIdentifiant.Text.ToString();
                    string saisie_mdp = txtMDP.Text.ToString();

                    if (gstBDD.seConnecter(saisie_id, saisie_mdp))
                    {
                        Dashboard leDashBoard = new Dashboard(gstBDD.getUserInfosById(saisie_id, saisie_mdp), "Accueil");
                        leDashBoard.Show();
                        Application.Current.MainWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("Votre mots de passe ou identifiant est incorect", "erreur de connexion", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez saisir un mots de passe", "erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir un identifiant", "erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            var deconnexion = MessageBox.Show("Etes vous sur de vouloir quitter l'application ?", "Quitter", MessageBoxButton.OKCancel);
            if (deconnexion == MessageBoxResult.OK)
            {
                Environment.Exit(0);//Cette ligne permet de fermer toute l'application
            }
        }


        private void txtMDP_TextChanged(object sender, TextChangedEventArgs e)
        {
            var isCapsLockToggled = Keyboard.IsKeyToggled(Key.CapsLock);

            if (isCapsLockToggled)
            {
                capLock.Text = "Maj";

            }
            else
            {
                capLock.Text = "";
            }
        }
    }
}

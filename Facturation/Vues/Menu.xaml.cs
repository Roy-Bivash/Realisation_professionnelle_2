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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryOfClass;

namespace Facturation.Vues
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        string currentPage;

        public Menu(string theCurrentPage)
        {
            InitializeComponent();
            currentPage = theCurrentPage;
        }

        Connexion cnx;

        //Declarer des couleurs à reutiliser plus trad :
        private Brush SelectedColor = (Brush)new BrushConverter().ConvertFromString("#16A085");
        private Brush UnselectedColor = (Brush)new BrushConverter().ConvertFromString("#A3E4D7");


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cnx = new Connexion();

            //Selection de la page a afficher au lancement de la dashboard
            switch (currentPage)
            {
                case "Accueil":
                    MainContent.Content = new Accueil();
                    break;
                case "VueGlobale":
                    MainContent.Content = new VueGlobale();
                    break;
                case "GererUser":
                    MainContent.Content = new GererUser();
                    break;
                case "GererAbonnement":
                    MainContent.Content = new GererAbonnement();
                    break;


            }
            //Changer les boutons de couleur :
            btnVueGlobale.Background = UnselectedColor;
            btnGererUser.Background = UnselectedColor;
            btnGererAbonnement.Background = UnselectedColor;
            btnDeconnexion.Background = UnselectedColor;
        }

        private void btnVueGlobale_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new VueGlobale();

            //Changer les boutons de couleur :
            btnVueGlobale.Background = SelectedColor;
            btnGererUser.Background = UnselectedColor;
            btnGererAbonnement.Background = UnselectedColor;
        }

        private void btnGererUser_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new GererUser();

            //Changer les boutons de couleur :
            btnVueGlobale.Background = UnselectedColor;
            btnGererUser.Background = SelectedColor;
            btnGererAbonnement.Background = UnselectedColor;
        }

        private void btnGererAbonnement_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new GererAbonnement();

            //Changer les boutons de couleur :
            btnVueGlobale.Background = UnselectedColor;
            btnGererUser.Background = UnselectedColor;
            btnGererAbonnement.Background = SelectedColor;
        }

        private void btnBDD_Click(object sender, RoutedEventArgs e)
        {

            //Changer les boutons de couleur :
            btnVueGlobale.Background = UnselectedColor;
            btnGererUser.Background = UnselectedColor;
            btnGererAbonnement.Background = UnselectedColor;
        }

        private void btnDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            var deconnexion = MessageBox.Show("Etes vous sur de vouloir vous deconnecter ?", "Deconnexion", MessageBoxButton.OKCancel);
            if (deconnexion == MessageBoxResult.OK)
            {
                cnx.deconnexion();
                //MainWindow pageConnexion = new MainWindow();
                //pageConnexion.Show();

                Environment.Exit(0);//Cette ligne permet de fermer toute l'application

            }

        }

    }
}

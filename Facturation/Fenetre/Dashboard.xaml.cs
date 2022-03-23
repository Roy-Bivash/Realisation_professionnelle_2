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
using Facturation.Vues;
using LibraryOfClass;

namespace Facturation.Fenetre
{
    /// <summary>
    /// Logique d'interaction pour Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        Connexion cnx;
        string currentPage;

        public Dashboard(User leUser, string theCurrentPage)
        {
            InitializeComponent();
            cnx = new Connexion();
            cnx.creerConnexion(leUser);
            currentPage = theCurrentPage;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (cnx.getUserStatut() == "admin")
            {
                DashboardContent.Content = new Vues.Menu(currentPage);
            }
            else
            {
                DashboardContent.Content = new nonAdmin();
            }
        }
    }
}

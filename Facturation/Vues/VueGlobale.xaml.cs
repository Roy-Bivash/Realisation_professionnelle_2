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
using LiveCharts;
using LiveCharts.Wpf;
using LibraryOfClass;

namespace Facturation.Vues
{
    /// <summary>
    /// Logique d'interaction pour VueGlobale.xaml
    /// </summary>
    public partial class VueGlobale : Page
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        GstBDD gstBDD;
        GstGlobale gstGlobale;

        public VueGlobale()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            gstBDD = new GstBDD();
            gstGlobale = new GstGlobale();



            //Debut des actions sur le GrapheAbonnement :
            Dictionary<string, int> lesDatasGraphAboClient = gstGlobale.getNbClientAbooneAndNonAbonne(gstBDD.getAllClients());

            txtNbClients.Text = "Total de " + lesDatasGraphAboClient["totalClient"] + " clients";

            int nbClientNonAbonne = lesDatasGraphAboClient["nbClientNonAbonne"];
            int nbClientAbonne = lesDatasGraphAboClient["nbClientAbonne"];

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Nombre de clients",
                    Values = new ChartValues<int> { nbClientNonAbonne, nbClientAbonne }
                }
            };

            Labels = new[] { "Clients non abonnés", "Clients abonnés" };
            Formatter = value => value.ToString("N");
            DataContext = this;
            //Fin des actions sur le GrapheAbonnement


            //Debut des actions sur le GraphTypeAbo :
            PieSeries ps;
            ChartValues<int> line;
            Dictionary<string, int> lesDatasGraphTypeAbo = new Dictionary<string, int>();


            lesDatasGraphTypeAbo = gstBDD.getNbAboInEachType();
            foreach (string cle in lesDatasGraphTypeAbo.Keys)
            {
                ps = new PieSeries();
                line = new ChartValues<int>();
                line.Add(lesDatasGraphTypeAbo[cle]);
                ps.Values = line;
                ps.Title = cle;
                ps.DataLabels = true;
                GraphTypeAbo.Series.Add(ps);
            }
            GraphTypeAbo.LegendLocation = LegendLocation.Bottom;
            //Fin des actions sur le GraphTypeAbo


            lstBoxDernierClients.ItemsSource = gstBDD.getRecentClientsAbo();



        }


        private Brush hoverColer = (Brush)new BrushConverter().ConvertFromString("#A3E4D7");
        private Brush nonHoverColor = (Brush)new BrushConverter().ConvertFromString("transparent");









        private void lstBoxDernierClients_MouseEnter(object sender, MouseEventArgs e)
        {
            lstBoxDernierClients.BorderBrush = hoverColer;
        }
        private void lstBoxDernierClients_MouseLeave(object sender, MouseEventArgs e)
        {
            lstBoxDernierClients.BorderBrush = nonHoverColor;

        }

        private void borderGraphAbonnement_MouseEnter(object sender, MouseEventArgs e)
        {
            borderGraphAbonnement.BorderBrush = hoverColer;
        }

        private void borderGraphAbonnement_MouseLeave(object sender, MouseEventArgs e)
        {
            borderGraphAbonnement.BorderBrush = nonHoverColor;
        }

        private void borderGraphTypeAbo_MouseEnter(object sender, MouseEventArgs e)
        {
            borderGraphTypeAbo.BorderBrush = hoverColer;
        }

        private void borderGraphTypeAbo_MouseLeave(object sender, MouseEventArgs e)
        {
            borderGraphTypeAbo.BorderBrush = nonHoverColor;
        }
    }


}


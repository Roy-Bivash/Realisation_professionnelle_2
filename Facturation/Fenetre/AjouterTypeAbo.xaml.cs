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
    /// Logique d'interaction pour AjouterTypeAbo.xaml
    /// </summary>
    public partial class AjouterTypeAbo : Window
    {
        GstGlobale gstGlobale;
        GstBDD gstBDD;
        public AjouterTypeAbo()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gstGlobale = new GstGlobale();
            gstBDD = new GstBDD();
            if(txtNom.Text != "")
            {
                if(txtNbMaxFactures.Text != "")
                {
                    if(txtNbMaxDevis.Text != "")
                    {
                        if(txtPrix.Text != "")
                        {
                            int nbFactures = gstGlobale.verifSasieInt(txtNbMaxFactures.Text);
                            if (nbFactures != 0)
                            {
                                int nbDevis = gstGlobale.verifSasieInt(txtNbMaxDevis.Text);
                                if (nbDevis != 0)
                                {

                                    //Utiliser un try catch pour eviter les saisie de string a la place des int
                                    try
                                    {
                                        //double prixSasie = Convert.ToDouble(txtNbModifier.Text);
                                        gstBDD.ajouterNouvTypeAbo(txtNom.Text, nbFactures, nbDevis, txtPrix.Text);
                                        MessageBox.Show("Le nouvelle abonnement est créer, veuillez actauliser la page pour voir le changement", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                                        this.Close();
                                    }
                                    catch (System.Exception)
                                    {
                                        MessageBox.Show("Veuillez saisir des chiffres");
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("La saisie du numbre de devis est incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("La saisie du numbre de factures est incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("La saisie du prix est incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("La saisie du numbre de devis est incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("La saisie du nombre de factures est incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("La saisie de la designation est incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

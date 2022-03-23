using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryOfClass
{
    public class GstGlobale
    {
        //Verrifier si le type abonnement n'est pas utiliser par un client avant sa suppression
        //Renvoie true si un client utilise le type abonnement
        public bool verifTypeAboUtiliser(int idTypeAbo, List<Client> lesClient)
        {
            bool trouve = false;
            foreach(Client leClient in lesClient)
            {
                if(leClient.LAbonnement.LeTypeAbo.Id == idTypeAbo)
                {
                    trouve = true; //Si trouvé
                    break;
                }
            }
            return trouve;
        }

        //Cette fonction renvoie un dictionnary contenant :
        //totalClient : Nombre total de client dans la base de données
        //nbClientAbonne : Le nombre totale de clients abonné aux services
        //nbClientNonAbonne : Le nombre totale de client qui ne sont pas abonné aux services
        public Dictionary<string, int> getNbClientAbooneAndNonAbonne(List<Client> lesClients)
        {
            Dictionary<string, int> lesDatas = new Dictionary<string, int>();

            int totalClient = lesClients.Count;
            int nbClientAbonne = 0;
            int nbClientNonAbonne = 0;
            foreach (Client leClient in lesClients)
            {
                if(leClient.LAbonnement != null)
                {
                    if (leClient.LAbonnement.Statut_abonnement == "true")
                    {
                        nbClientAbonne++;
                    }
                }
            }

            nbClientNonAbonne = totalClient - nbClientAbonne;

            lesDatas.Add("totalClient", totalClient);
            lesDatas.Add("nbClientAbonne", nbClientAbonne);
            lesDatas.Add("nbClientNonAbonne", nbClientNonAbonne);

            return lesDatas;
        }





        //Verifie une saisie string
        //Si la saisie peu etre convertie en int alors renvoie la valeur convertie en int
        //Si la saie ne peu pas etre convertie en int alors renvoie 0;
        public int verifSasieInt(string saisie)
        {
            int nbFacturesSasie = 0;
            // Utiliser un try catch pour eviter les saisie de string a la place des int
            try
            {
                nbFacturesSasie = Convert.ToInt32(saisie);
            }
            catch (System.Exception)
            {
                nbFacturesSasie = 0;
            }
            return nbFacturesSasie;
        }








    }
}

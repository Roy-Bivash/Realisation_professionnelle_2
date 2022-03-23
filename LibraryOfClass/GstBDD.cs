using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;


namespace LibraryOfClass
{
    public class GstBDD
    {
        //Ce gestionnaire est permet toutes les communication avec la base de données
        private MySqlConnection cnx;
        private MySqlCommand cmd;
        private MySqlDataReader dr;


        // Constructeur
        public GstBDD()
        {
            string chaine = "Server=localhost;Database=projet_bts;Uid=root;Pwd=;SslMode=none";
            cnx = new MySqlConnection(chaine);
            cnx.Open();
        }


        //Cette fonction permet de veirifer si les informations saisie pour la connection sont correct pour permettre la la connexion
        public bool seConnecter(string user_id, string user_mdp)
        {
            bool connexion = false;
            cmd = new MySqlCommand("SELECT user.id FROM user WHERE user.identifiant = '" + user_id + "' AND user.mdp = md5('" + user_mdp + "');", cnx);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                connexion = true;
            }
            dr.Close();
            return connexion;
        }

        //Recupere les informations de l'utilisateur par sont identifiant et mots de passe
        public User getUserInfosById(string user_id, string user_mdp)
        {
            User unUser = null;
            cmd = new MySqlCommand("SELECT `identifiant`, `statut`, `mail` FROM `user` WHERE user.identifiant = '" + user_id + "' AND user.mdp = md5('" + user_mdp + "');", cnx);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                unUser = new User(dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            return unUser;
        }

        //Renvoie tous les types d'abonnement dans la liste de type_abonnement
        public List<Type_abonnement> getAllTypeAbonnement()
        {
            List<Type_abonnement> lesTypeAbonnements = new List<Type_abonnement>();
            cmd = new MySqlCommand("SELECT `id`, `nom`, `nb_max_facture`, `nb_max_devis`, `prix_abo` FROM `type_abonnement`;", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Type_abonnement unNouvTypeAbo = new Type_abonnement(Convert.ToInt16(dr[0]), dr[1].ToString(), Convert.ToInt16(dr[2]), Convert.ToInt16(dr[3]), Convert.ToDouble(dr[4]));
                lesTypeAbonnements.Add(unNouvTypeAbo);
            }
            dr.Close();
            return lesTypeAbonnements;
        }

        //Renvoie les informations d'un abonnement en particulier par son id
        public Type_abonnement getInfosAbonnementById(int IdAbonnement)
        {
            Type_abonnement leTypeAbonnement = null;
            cmd = new MySqlCommand("SELECT `id`, `nom`, `nb_max_facture`, `nb_max_devis`, `prix_abo` FROM `type_abonnement` WHERE id = " + IdAbonnement + ";", cnx);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                leTypeAbonnement = new Type_abonnement(Convert.ToInt16(dr[0]), dr[1].ToString(), Convert.ToInt16(dr[2]), Convert.ToInt16(dr[3]), Convert.ToDouble(dr[4]));
            }
            dr.Close();
            return leTypeAbonnement;
        }

        //Renvoie le nombre total de clients abonné ou non abonné
        public int getNbtotalClients()
        {
            int nbClients = 0;
            cmd = new MySqlCommand("Select COUNT(id) FROM user;", cnx);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                nbClients = Convert.ToInt32(dr[0]);
            }
            dr.Close();
            return nbClients;
        }

        //Recupere le nombre d'abonnement dans chaque type d'abonnement
        //Renvoie un disctionnaire avec le nom de l'abonnement en string et le nombre de clients en int
        public Dictionary<string, int> getNbAboInEachType()
        {
            Dictionary<string, int> lesDatas = new Dictionary<string, int>();

            cmd = new MySqlCommand("SELECT type_abonnement.nom, COUNT(*) FROM type_abonnement INNER JOIN abonnement ON abonnement.num_type_abo = type_abonnement.id GROUP BY(type_abonnement.nom);", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lesDatas.Add(dr[0].ToString(), Convert.ToInt16(dr[1].ToString()));
            }
            dr.Close();

            return lesDatas;
        }

        //Renvoie les informations de tous les clients dans une liste de Client
        public List<Client> getAllClients()
        {
            List<Client> lesClients = new List<Client>();
            cmd = new MySqlCommand("SELECT user.id, user.identifiant, user.statut, user.mail, type_abonnement.id, type_abonnement.nom, type_abonnement.nb_max_facture, type_abonnement.nb_max_devis, type_abonnement.prix_abo, abonnement.abo_statut, abonnement.date_abonnement, abonnement.date_fin, abonnement.nb_fact_restant, abonnement.nb_devis_restant FROM `user`LEFT JOIN abonnement ON abonnement.num_user = user.id LEFT JOIN type_abonnement ON type_abonnement.id = abonnement.num_type_abo WHERE user.statut = 'client' ORDER BY(user.id);", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Client unNouvClient = null;

                //recuperer l'id de l'abonnement :
                var abonnement = dr[4].ToString();
                var aboStatut = dr[9].ToString();
                //Si l'id de l'abonnemnt existe et si le statut de l'abonnement est egale a "true" :
                if (abonnement != "" || aboStatut == "true")
                {
                    //Le client a un abonnement
                    Type_abonnement unType_abonnement = new Type_abonnement(Convert.ToInt16(dr[4]), dr[5].ToString(), Convert.ToInt32(dr[6]), Convert.ToInt32(dr[7]), Convert.ToDouble(dr[8]));
                    Abonnement unNouvAbonnement = new Abonnement(dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), unType_abonnement, Convert.ToInt32(dr[12]), Convert.ToInt32(dr[13]));
                    unNouvClient = new Client(Convert.ToInt16(dr[0]), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), unNouvAbonnement);
                }
                else
                {
                    //Le client n'a pas d'abonnement
                    unNouvClient = new Client(Convert.ToInt16(dr[0]), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), null);
                }
                lesClients.Add(unNouvClient);
            }
            dr.Close();
            return lesClients;
        }


        //Renvoie la meme chose que la methode getAllClients(), mais filtre par le login recuperer
        //Utiliser pour mettre a jour la liste en fonction de se qui est saisie dans la bare de recherche
        public List<Client> getAllClientsByLogin(string login)
        {
            List<Client> lesClients = new List<Client>();
            cmd = new MySqlCommand("SELECT user.id, user.identifiant, user.statut, user.mail, type_abonnement.id, type_abonnement.nom, type_abonnement.nb_max_facture, type_abonnement.nb_max_devis, type_abonnement.prix_abo, abonnement.abo_statut, abonnement.date_abonnement, abonnement.date_fin, abonnement.nb_fact_restant, abonnement.nb_devis_restant FROM `user`LEFT JOIN abonnement ON abonnement.num_user = user.id LEFT JOIN type_abonnement ON type_abonnement.id = abonnement.num_type_abo WHERE user.statut = 'client' AND user.identifiant LIKE '%" + login + "%' ORDER BY(user.id);", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Client unNouvClient = null;

                //recuperer l'id de l'abonnement :
                var abonnement = dr[4].ToString();
                var aboStatut = dr[9].ToString();
                //Si l'id de l'abonnemnt existe et si le statut de l'abonnement est egale a "true" :
                if (abonnement != "" || aboStatut == "true")
                {
                    //Le client a un abonnement
                    Type_abonnement unType_abonnement = new Type_abonnement(Convert.ToInt16(dr[4]), dr[5].ToString(), Convert.ToInt32(dr[6]), Convert.ToInt32(dr[7]), Convert.ToDouble(dr[8]));
                    Abonnement unNouvAbonnement = new Abonnement(dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), unType_abonnement, Convert.ToInt32(dr[12]), Convert.ToInt32(dr[13]));
                    unNouvClient = new Client(Convert.ToInt16(dr[0]), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), unNouvAbonnement);
                }
                else
                {
                    //Le client n'a pas d'abonnement
                    unNouvClient = new Client(Convert.ToInt16(dr[0]), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), null);
                }
                lesClients.Add(unNouvClient);
            }
            dr.Close();
            return lesClients;
        }


        //Renvoie les informations de tous les clients abonnés depuis moins de 31 jours
        public List<Client> getRecentClientsAbo()
        {
            List<Client> lesClients = new List<Client>();
            cmd = new MySqlCommand("SELECT user.id, user.identifiant, user.statut, user.mail, type_abonnement.id, type_abonnement.nom, type_abonnement.nb_max_facture, type_abonnement.nb_max_devis, type_abonnement.prix_abo, abonnement.abo_statut, abonnement.date_abonnement, abonnement.date_fin, abonnement.nb_fact_restant, abonnement.nb_devis_restant FROM `user`LEFT JOIN abonnement ON abonnement.num_user = user.id LEFT JOIN type_abonnement ON type_abonnement.id = abonnement.num_type_abo WHERE user.statut = 'client' AND abonnement.date_abonnement > CURRENT_DATE()-31;", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Client unNouvClient = null;

                //recuperer l'id de l'abonnement :
                var abonnement = dr[4].ToString();
                var aboStatut = dr[9].ToString();
                //Si l'id de l'abonnemnt existe et si le statut de l'abonnement est egale a "true" :
                if (abonnement != "" || aboStatut == "true")
                {
                    //Le client a un abonnement
                    Type_abonnement unType_abonnement = new Type_abonnement(Convert.ToInt16(dr[4]), dr[5].ToString(), Convert.ToInt32(dr[6]), Convert.ToInt32(dr[7]), Convert.ToDouble(dr[8]));
                    Abonnement unNouvAbonnement = new Abonnement(dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), unType_abonnement, Convert.ToInt32(dr[12]), Convert.ToInt32(dr[13]));
                    unNouvClient = new Client(Convert.ToInt16(dr[0]), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), unNouvAbonnement);
                }
                else
                {
                    //Le client n'a pas d'abonnement
                    unNouvClient = new Client(Convert.ToInt16(dr[0]), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), null);
                }
                lesClients.Add(unNouvClient);
            }
            dr.Close();
            return lesClients;
        }

        //Modifier l'identifiant du client
        public void modifierIdentifiantClient(int idClient, string newIdentifiantClient)
        {
            cmd = new MySqlCommand("UPDATE `user` SET `identifiant` = '" + newIdentifiantClient + "' WHERE `user`.`id` = " + idClient + ";", cnx);
            cmd.ExecuteReader();
        }

        //Modifier le mail du client
        public void modifierMailClient(int idClient, string newMailClient)
        {
            cmd = new MySqlCommand("UPDATE `user` SET `mail` = '" + newMailClient + "' WHERE `user`.`id` = '" + idClient + "';", cnx);
            cmd.ExecuteReader();
        }

        //Modifier le nombre de devis restant du client
        public void modifierNbDevisRestant(int idClient, int newNbDevis)
        {
            //cmd = new MySqlCommand("UPDATE `abonnement` SET `nb_devis_restant` = '" + newNbDevis + "' WHERE `abonnement`.`id` = '" + idClient + "';", cnx);
            cmd = new MySqlCommand("UPDATE `abonnement` SET `nb_devis_restant` = '" + newNbDevis + "' WHERE abonnement.num_user = " + idClient + ";", cnx);
            cmd.ExecuteReader();
        }

        //Modifier le nombre de factures restant du client
        public void modifierNbFactRestant(int idClient, int newNbFactures)
        {
            cmd = new MySqlCommand("UPDATE `abonnement` SET `nb_fact_restant` = '" + newNbFactures + "' WHERE abonnement.num_user = " + idClient + ";", cnx);
            cmd.ExecuteReader();
        }

        //Ajouter un abonnement a un client
        //La durré de l'abonnement est precisé en int dans nbJoursAbonnement
        public void ajouterAbonnementClient(int idClient, int nbJoursAbonnement, Type_abonnement lAbonnement)
        {
            cmd = new MySqlCommand("INSERT INTO `abonnement`(`id`, `num_user`, `abo_statut`, `date_abonnement`, `date_fin`, `num_type_abo`, `nb_fact_restant`, `nb_devis_restant`) VALUES(null," + idClient + ", 'true', CURRENT_DATE(), CURRENT_DATE()," + lAbonnement.Id + "," + lAbonnement.NbMaxFacture + "," + lAbonnement.NbMaxDevis + "); UPDATE `abonnement` SET `date_fin` = DATE_ADD(`date_fin` , INTERVAL " + nbJoursAbonnement + " DAY) WHERE `num_user` = " + idClient + ";", cnx);
            cmd.ExecuteReader();
        }

        //Changer de type d'abonnement a un client
        public void modifierTypeAbonnementClient(int idClient, int newIdTypeAbonnement)
        {
            cmd = new MySqlCommand("UPDATE `abonnement` SET `num_type_abo` = '" + newIdTypeAbonnement + "' WHERE abonnement.num_user = " + idClient + ";", cnx);
            cmd.ExecuteReader();
        }

        //Supprimer l'abonnement d'un client
        public void suppAbonnementClient(int idClient)
        {
            cmd = new MySqlCommand("DELETE FROM `abonnement` WHERE `abonnement`.`num_user` = " + idClient + ";", cnx);
            cmd.ExecuteReader();
        }

        //Supprimer un type d'abonnement
        //Pour supprimer un type d'abonnement il faudra désabonner tous ceux qui y sont abonné pour eviter une erreur MyQsl
        public void suppTypeAbonnement(int idTypeAbonnement)
        {
            cmd = new MySqlCommand("DELETE FROM `type_abonnement` WHERE `type_abonnement`.`id` = " + idTypeAbonnement + "; ", cnx);
            cmd.ExecuteReader();
        }

        //Modifier le nom d'un type d'abonnement specifique
        public void modifierNomTypeAbonnement(int idTypeAbo, string newNom)
        {
            cmd = new MySqlCommand("UPDATE `type_abonnement` SET `nom` = '" + newNom + "' WHERE `type_abonnement`.`id` = " + idTypeAbo + "; ", cnx);
            cmd.ExecuteReader();
        }

        //Modifier le nombre de factures maximal d'un type d'abonnement specifique
        public void modifierNbMaxFactTypeAbonnementById(int idTypeAbo, int newNb)
        {
            cmd = new MySqlCommand("UPDATE `type_abonnement` SET `nb_max_facture` = '" + newNb + "' WHERE `type_abonnement`.`id` = " + idTypeAbo + "; ", cnx);
            cmd.ExecuteReader();
        }

        //Modifier le nombre de devis maximal d'un type d'abonnement specifique
        public void modifierNbMaxDevisTypeAbonnementById(int idTypeAbo, int newNb)
        {
            cmd = new MySqlCommand("UPDATE `type_abonnement` SET `nb_max_devis` = '" + newNb + "' WHERE `type_abonnement`.`id` = " + idTypeAbo + "; ", cnx);
            cmd.ExecuteReader();
        }
        //Modifier le prix d'un type d'abonnement specifique
        public void modfierPrixTypeAbo(int idTypeAbo, string newPrix)
        {
            cmd = new MySqlCommand("UPDATE `type_abonnement` SET `prix_abo` = '" + newPrix.Replace(',', '.') + "' WHERE `type_abonnement`.`id` = " + idTypeAbo + "; ", cnx);
            cmd.ExecuteReader();
        }

        //Ajouter un nouveau type d'abonnement
        public void ajouterNouvTypeAbo(string nom, int nbMaxFact, int nbMaxDevis, string prix)
        {
            cmd = new MySqlCommand("INSERT INTO `type_abonnement`(`id`, `nom`, `nb_max_facture`, `nb_max_devis`, `prix_abo`) VALUES (NULL,'" + nom + "','" + nbMaxFact + "','" + nbMaxDevis + "','" + prix.Replace(',', '.') + "')", cnx);
            cmd.ExecuteReader();
        }

        


    }
}

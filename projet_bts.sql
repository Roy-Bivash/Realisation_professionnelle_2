-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : sam. 19 mars 2022 à 14:54
-- Version du serveur :  5.7.31
-- Version de PHP : 7.4.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `projet_bts`
--

DELIMITER $$
--
-- Procédures
--
DROP PROCEDURE IF EXISTS `proc_maj_abo`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `proc_maj_abo` (IN `id_user` INT)  NO SQL
BEGIN
    SET @report_curante_date = (SELECT CURRENT_DATE);
    SET @report_v_date_fin = (SELECT `date_fin` FROM abonnement WHERE abonnement.num_user = id_user);
    IF(@report_v_date_fin < @report_curante_date) THEN
        UPDATE `abonnement` SET `abo_statut` = 'false' WHERE `abonnement`.`num_user` = id_user;
    ELSEIF(@report_v_date_fin > @report_curante_date) THEN
    	UPDATE `abonnement` SET `abo_statut` = 'true' WHERE `abonnement`.`num_user` = id_user;
    END IF;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `abonnement`
--

DROP TABLE IF EXISTS `abonnement`;
CREATE TABLE IF NOT EXISTS `abonnement` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `num_user` int(11) NOT NULL,
  `abo_statut` varchar(50) COLLATE utf8_bin NOT NULL,
  `date_abonnement` date DEFAULT NULL,
  `date_fin` date NOT NULL,
  `num_type_abo` int(11) DEFAULT NULL,
  `nb_fact_restant` int(11) NOT NULL,
  `nb_devis_restant` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `num_user` (`num_user`),
  KEY `num_type_abo` (`num_type_abo`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Déchargement des données de la table `abonnement`
--

INSERT INTO `abonnement` (`id`, `num_user`, `abo_statut`, `date_abonnement`, `date_fin`, `num_type_abo`, `nb_fact_restant`, `nb_devis_restant`) VALUES
(4, 4, 'false', '2022-01-12', '2022-02-09', 2, 0, 0),
(14, 3, 'true', '2022-03-09', '2022-04-09', 2, 20, 30),
(16, 7, 'true', '2022-03-12', '2022-04-12', 4, 400, 500),
(17, 12, 'true', '2022-03-13', '2022-04-13', 3, 49, 70),
(18, 10, 'true', '2022-03-13', '2022-04-13', 4, 400, 500),
(19, 8, 'true', '2022-03-13', '2022-04-13', 1, 10, 20),
(21, 11, 'true', '2022-03-15', '2022-04-15', 2, 20, 30),
(22, 2, 'true', '2022-03-17', '2022-04-17', 1, 4, 18);

-- --------------------------------------------------------

--
-- Structure de la table `destinataire`
--

DROP TABLE IF EXISTS `destinataire`;
CREATE TABLE IF NOT EXISTS `destinataire` (
  `id_destinataire` int(11) NOT NULL AUTO_INCREMENT,
  `num_user_proprietaire` int(11) NOT NULL,
  `nom_denomination` text COLLATE utf8_bin NOT NULL,
  `adresse_rue` text COLLATE utf8_bin NOT NULL,
  `adresse_code_ville` text COLLATE utf8_bin NOT NULL,
  `tva_intra` text COLLATE utf8_bin,
  PRIMARY KEY (`id_destinataire`),
  KEY `num_user_proprietaire` (`num_user_proprietaire`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Déchargement des données de la table `destinataire`
--

INSERT INTO `destinataire` (`id_destinataire`, `num_user_proprietaire`, `nom_denomination`, `adresse_rue`, `adresse_code_ville`, `tva_intra`) VALUES
(1, 1, 'Halogene', '74 rue des Faubourg Saint Denis', '75010 Paris', 'FR09383119153'),
(6, 2, 'Assistance Active', '130 rue de la Falaise', '75019 Paris', NULL),
(7, 12, 'Nom Prenom', '42 Avenue Leclerc', '75020 Paris', NULL);

-- --------------------------------------------------------

--
-- Structure de la table `facture_devis`
--

DROP TABLE IF EXISTS `facture_devis`;
CREATE TABLE IF NOT EXISTS `facture_devis` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `num_user` int(11) NOT NULL,
  `type` varchar(10) COLLATE utf8_bin NOT NULL,
  `num_fact_devis` varchar(150) COLLATE utf8_bin NOT NULL,
  `num_destinataire` int(11) NOT NULL,
  `date_facture` date NOT NULL,
  `total_ttc` double NOT NULL,
  `num_vendeur` int(11) NOT NULL,
  `model_facture_devis` int(11) NOT NULL,
  `tva_auto_liquidation` varchar(7) COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`id`),
  KEY `num_destinataire` (`num_destinataire`),
  KEY `num_user` (`num_user`),
  KEY `num_vendeur` (`num_vendeur`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Déchargement des données de la table `facture_devis`
--

INSERT INTO `facture_devis` (`id`, `num_user`, `type`, `num_fact_devis`, `num_destinataire`, `date_facture`, `total_ttc`, `num_vendeur`, `model_facture_devis`, `tva_auto_liquidation`) VALUES
(9, 2, 'facture', '2487', 6, '2022-02-25', 12, 2, 1, 'false'),
(11, 2, 'facture', '1234567890', 6, '2022-03-23', 288, 3, 1, 'false'),
(12, 2, 'facture', '23456', 6, '2022-03-17', 23, 3, 1, 'false'),
(13, 2, 'devis', '1324Y', 6, '2022-03-19', 7854, 3, 2, 'false'),
(14, 2, 'devis', '85964BFJ', 6, '2022-03-12', 74.4, 3, 1, 'false'),
(15, 2, 'devis', '85964BFJ', 6, '2022-03-12', 74.4, 3, 1, 'false'),
(16, 2, 'facture', 'GSNE43543', 6, '2022-03-25', 14.4, 4, 1, 'false'),
(17, 2, 'facture', 'DHS43524532432423', 6, '2022-04-05', 227.75, 3, 2, 'false'),
(18, 2, 'facture', 'GSR452VC', 6, '2022-04-09', 128, 4, 2, 'true'),
(19, 2, 'facture', '2345678GHJK', 6, '2022-03-25', 4, 4, 2, 'false'),
(20, 2, 'facture', '12DFBFDB', 6, '2022-03-26', 69, 4, 2, 'true'),
(21, 2, 'devis', 'ZR', 6, '2022-03-26', 36, 2, 1, 'false'),
(22, 2, 'devis', '241231', 6, '2022-03-24', 12, 2, 2, 'false'),
(23, 12, 'facture', '9635VK32-021', 7, '2022-03-30', 249.99, 5, 2, 'false'),
(24, 2, 'facture', '765432213', 6, '2022-03-23', 24, 2, 2, 'false');

--
-- Déclencheurs `facture_devis`
--
DROP TRIGGER IF EXISTS `tr_maj_nb_convert_fact_update`;
DELIMITER $$
CREATE TRIGGER `tr_maj_nb_convert_fact_update` BEFORE UPDATE ON `facture_devis` FOR EACH ROW BEGIN
    IF(OLD.type = "devis") THEN
        IF(NEW.type = "facture") THEN
            UPDATE `abonnement` SET `nb_fact_restant`= abonnement.nb_fact_restant - 1 WHERE abonnement.num_user = NEW.num_user;
        END IF;
    END IF;
END
$$
DELIMITER ;
DROP TRIGGER IF EXISTS `tr_maj_nb_fact_insert`;
DELIMITER $$
CREATE TRIGGER `tr_maj_nb_fact_insert` AFTER INSERT ON `facture_devis` FOR EACH ROW BEGIN
IF(NEW.type = "facture") THEN
    UPDATE `abonnement` SET `nb_fact_restant`= abonnement.nb_fact_restant - 1 WHERE abonnement.num_user = NEW.num_user;
ELSEIF(NEW.type = "devis") THEN
    UPDATE `abonnement` SET `nb_devis_restant`= abonnement.nb_devis_restant - 1 WHERE abonnement.num_user = NEW.num_user;
END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `ligne_facture_devis`
--

DROP TABLE IF EXISTS `ligne_facture_devis`;
CREATE TABLE IF NOT EXISTS `ligne_facture_devis` (
  `id_ligne` int(11) NOT NULL AUTO_INCREMENT,
  `num_facture` int(150) NOT NULL,
  `description` text COLLATE utf8_bin NOT NULL,
  `quantite` int(11) NOT NULL,
  `unité` varchar(50) COLLATE utf8_bin NOT NULL,
  `prix_unitaire_ht` double NOT NULL,
  `tva_ligne` double DEFAULT NULL,
  PRIMARY KEY (`id_ligne`),
  KEY `num_facture` (`num_facture`)
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Déchargement des données de la table `ligne_facture_devis`
--

INSERT INTO `ligne_facture_devis` (`id_ligne`, `num_facture`, `description`, `quantite`, `unité`, `prix_unitaire_ht`, `tva_ligne`) VALUES
(10, 9, 'Pontalon', 12, 'PIECE', 1, 20),
(13, 11, 'Pontalon', 12, 'PIECE', 24, 20),
(14, 12, 'Un sweet', 23, 'PIECE', 1, 20),
(15, 13, 'dfsn', 231, 'm²', 34, 34),
(16, 14, 'Un sweet', 12, '0', 2, 20),
(17, 14, 'Pontalon', 12, '0', 4.2, 12),
(18, 16, 'Un sweet', 12, '0', 1.2, 21),
(19, 17, 'Pontalon', 12, '0', 12, 20),
(20, 17, 'Un sweet', 25, '0', 3.35, 20),
(21, 18, 'Pontalon', 32, '0', 4, NULL),
(22, 19, 'FDFDS', 2, '0', 2, 20),
(23, 20, 'gvzevq', 23, '0', 3, NULL),
(24, 21, 'Pontalon', 12, 'PIECE', 3, 23),
(25, 22, 'Pontalon', 12, 'PIECE', 1, 20),
(26, 23, 'Matelas', 1, 'Piece', 249.99, 20),
(27, 24, 'Pontalon', 12, 'm²', 2, 20);

-- --------------------------------------------------------

--
-- Structure de la table `type_abonnement`
--

DROP TABLE IF EXISTS `type_abonnement`;
CREATE TABLE IF NOT EXISTS `type_abonnement` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(50) COLLATE utf8_bin NOT NULL,
  `nb_max_facture` int(11) NOT NULL,
  `nb_max_devis` int(11) NOT NULL,
  `prix_abo` double NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_bin ROW_FORMAT=COMPACT;

--
-- Déchargement des données de la table `type_abonnement`
--

INSERT INTO `type_abonnement` (`id`, `nom`, `nb_max_facture`, `nb_max_devis`, `prix_abo`) VALUES
(1, 'basic', 10, 20, 24.99),
(2, 'standard', 20, 30, 34.99),
(3, 'plus', 50, 70, 54.99),
(4, 'premium', 400, 500, 99.99);

-- --------------------------------------------------------

--
-- Structure de la table `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `identifiant` text COLLATE utf8_bin NOT NULL,
  `mdp` text COLLATE utf8_bin NOT NULL,
  `statut` varchar(40) COLLATE utf8_bin NOT NULL,
  `mail` text COLLATE utf8_bin,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Déchargement des données de la table `user`
--

INSERT INTO `user` (`id`, `identifiant`, `mdp`, `statut`, `mail`) VALUES
(1, 'bivash', 'd7b01aa5dc5619841a6aec839fb03aa6', 'admin', 'a'),
(2, 'naruto', 'e174a96505e62d3ba3c642f58922320c', 'client', 'contact@narutomail.com'),
(3, 'test', '4b377d23309d4ed39c9da5791417aeff', 'client', 'mr.tester@thetester.com'),
(4, 'luffy', '753ec494f664e16bae0d8c34f46ac2e5', 'client', 'luffy@mail.com'),
(6, 'gabriel', '734071f6baab24205fbbe0b6fdd3135a', 'client', 'gabriel75@gmail.com'),
(7, 'quentin', '63dbcadb1cb8dbc2a7298b16432e6f3a', 'client', 'quentin75@gmail.com'),
(8, 'gael', '31785d992ee13c6ab2c113aac666512b', 'client', 'gael75@outlook.com'),
(9, 'thomas', '4a1b2641170f033d56ffb66af06d2ee7', 'client', 'thomas75@outlook.fr'),
(10, 'oliver', '81639b08be30e35059e5f1d3b1b2ee04', 'client', 'oliver75@gmail.com'),
(11, 'enzo', '8facfd533f572d4234acda5e10a8310c', 'client', 'enzo75@outlook.fr'),
(12, 'alexandre', '4c552f46e63e024e8932ec306dc6d09b', 'client', 'alexandre75@outlook.fr');

-- --------------------------------------------------------

--
-- Structure de la table `vendeur`
--

DROP TABLE IF EXISTS `vendeur`;
CREATE TABLE IF NOT EXISTS `vendeur` (
  `id_vendeur` int(11) NOT NULL AUTO_INCREMENT,
  `num_user_proprietaire` int(11) NOT NULL,
  `denomination` text COLLATE utf8_bin NOT NULL,
  `adresse_rue` text COLLATE utf8_bin NOT NULL,
  `adresse_code_ville` text COLLATE utf8_bin NOT NULL,
  `siret` varchar(14) COLLATE utf8_bin NOT NULL,
  `ape` text COLLATE utf8_bin NOT NULL,
  `tva_intra` text COLLATE utf8_bin NOT NULL,
  `RIB_NOM` text COLLATE utf8_bin,
  `RIB_IBAN` text COLLATE utf8_bin,
  `RIB_BIC` text COLLATE utf8_bin,
  `logo` text COLLATE utf8_bin,
  PRIMARY KEY (`id_vendeur`),
  KEY `num_user_proprietaire` (`num_user_proprietaire`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Déchargement des données de la table `vendeur`
--

INSERT INTO `vendeur` (`id_vendeur`, `num_user_proprietaire`, `denomination`, `adresse_rue`, `adresse_code_ville`, `siret`, `ape`, `tva_intra`, `RIB_NOM`, `RIB_IBAN`, `RIB_BIC`, `logo`) VALUES
(2, 2, 'SIMBA PLUS', '12 rue de Paris', '75015 Paris', '12345678900012', '1234A', 'FR98123456789', NULL, NULL, NULL, NULL),
(3, 2, 'RD SERVICES', '38 rue Lecuyer', '93300 Aubervilliers', '82134532500015', '9823Z', 'FR87821345325', NULL, NULL, NULL, NULL),
(4, 2, 'la plat', '12 rue du jardin', '75013 Paris', '56456587500014', '8767Z', 'FR45564565875', NULL, NULL, NULL, '26.jpg'),
(5, 12, 'Vendeur', '130 rue Jean de la Fontaine', '75013 Paris', '98767439700013', '2349Z', 'FR34987674397', NULL, NULL, NULL, NULL);

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `abonnement`
--
ALTER TABLE `abonnement`
  ADD CONSTRAINT `abonnement_ibfk_1` FOREIGN KEY (`num_user`) REFERENCES `user` (`id`),
  ADD CONSTRAINT `abonnement_ibfk_2` FOREIGN KEY (`num_type_abo`) REFERENCES `type_abonnement` (`id`);

--
-- Contraintes pour la table `destinataire`
--
ALTER TABLE `destinataire`
  ADD CONSTRAINT `destinataire_ibfk_1` FOREIGN KEY (`num_user_proprietaire`) REFERENCES `user` (`id`);

--
-- Contraintes pour la table `facture_devis`
--
ALTER TABLE `facture_devis`
  ADD CONSTRAINT `facture_devis_ibfk_1` FOREIGN KEY (`num_destinataire`) REFERENCES `destinataire` (`id_destinataire`),
  ADD CONSTRAINT `facture_devis_ibfk_2` FOREIGN KEY (`num_user`) REFERENCES `user` (`id`),
  ADD CONSTRAINT `facture_devis_ibfk_3` FOREIGN KEY (`num_vendeur`) REFERENCES `vendeur` (`id_vendeur`);

--
-- Contraintes pour la table `ligne_facture_devis`
--
ALTER TABLE `ligne_facture_devis`
  ADD CONSTRAINT `ligne_facture_devis_ibfk_1` FOREIGN KEY (`num_facture`) REFERENCES `facture_devis` (`id`);

--
-- Contraintes pour la table `vendeur`
--
ALTER TABLE `vendeur`
  ADD CONSTRAINT `vendeur_ibfk_1` FOREIGN KEY (`num_user_proprietaire`) REFERENCES `user` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

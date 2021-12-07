-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: localhost    Database: poultryfarm_anton_coursework_1
-- ------------------------------------------------------
-- Server version	5.7.31-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `breeds`
--

DROP TABLE IF EXISTS `breeds`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `breeds` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  `averageMonthEggCount` double NOT NULL,
  `averageWeight` double NOT NULL,
  `idDiet` int(10) unsigned NOT NULL,
  `wasDeleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_diet_to_breeds_idx` (`idDiet`),
  CONSTRAINT `fk_diet_to_breeds` FOREIGN KEY (`idDiet`) REFERENCES `diets` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `breeds`
--

LOCK TABLES `breeds` WRITE;
/*!40000 ALTER TABLE `breeds` DISABLE KEYS */;
INSERT INTO `breeds` VALUES (1,'Рамирес',50,300,1,0),(2,'Рамзес',45,420,2,0),(3,'Внезапус',35,510,1,0);
/*!40000 ALTER TABLE `breeds` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cell_codes`
--

DROP TABLE IF EXISTS `cell_codes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cell_codes` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `idWorkshops` int(10) unsigned NOT NULL,
  `idRow` int(10) unsigned NOT NULL,
  `idCell` int(10) unsigned NOT NULL,
  `wasDeleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_workshop_to_cellCodes_idx` (`idWorkshops`),
  KEY `fk_row_to_cellCodes_idx` (`idRow`),
  KEY `fk_cell_to_cellCodes_idx` (`idCell`),
  CONSTRAINT `fk_cell_to_cellCodes` FOREIGN KEY (`idCell`) REFERENCES `cells` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_row_to_cellCodes` FOREIGN KEY (`idRow`) REFERENCES `rows` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_workshop_to_cellCodes` FOREIGN KEY (`idWorkshops`) REFERENCES `workshops` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cell_codes`
--

LOCK TABLES `cell_codes` WRITE;
/*!40000 ALTER TABLE `cell_codes` DISABLE KEYS */;
INSERT INTO `cell_codes` VALUES (1,1,1,1,0),(2,1,1,2,0),(3,1,1,3,0),(4,1,2,4,0),(5,1,2,5,0),(6,1,2,6,0),(7,1,3,7,0),(8,1,3,8,0),(9,1,3,9,0),(10,1,4,10,0),(11,1,4,11,0),(12,1,4,12,0),(13,1,5,13,0),(14,1,5,14,0),(15,1,5,15,0),(16,2,6,16,0),(17,2,6,17,0),(18,2,6,18,0),(19,2,7,19,0),(20,2,7,20,0),(21,2,7,21,0),(22,2,8,22,0),(23,2,8,23,0),(24,2,8,24,0),(25,3,9,25,0),(26,3,9,26,0),(27,3,9,27,0),(28,3,10,28,0),(29,3,10,29,0),(30,3,10,30,0),(31,3,11,31,0),(32,3,11,32,0),(33,3,11,33,0);
/*!40000 ALTER TABLE `cell_codes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cells`
--

DROP TABLE IF EXISTS `cells`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cells` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `number` varchar(16) NOT NULL,
  `wasDeleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cells`
--

LOCK TABLES `cells` WRITE;
/*!40000 ALTER TABLE `cells` DISABLE KEYS */;
INSERT INTO `cells` VALUES (1,'С001',0),(2,'С002',0),(3,'С003',0),(4,'С004',0),(5,'С005',0),(6,'С006',0),(7,'С007',0),(8,'С008',0),(9,'С009',0),(10,'С010',0),(11,'С011',0),(12,'С012',0),(13,'С013',0),(14,'С014',0),(15,'С015',0),(16,'В001',0),(17,'В002',0),(18,'В003',0),(19,'В004',0),(20,'В005',0),(21,'В006',0),(22,'В007',0),(23,'В008',0),(24,'В009',0),(25,'СВ001',0),(26,'СВ002',0),(27,'СВ003',0),(28,'СВ004',0),(29,'СВ005',0),(30,'СВ006',0),(31,'СВ007',0),(32,'СВ008',0),(33,'СВ009',0);
/*!40000 ALTER TABLE `cells` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `chickens`
--

DROP TABLE IF EXISTS `chickens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `chickens` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `idBreed` int(10) unsigned NOT NULL,
  `idCellCode` int(10) unsigned NOT NULL,
  `weight` double NOT NULL,
  `age` int(11) NOT NULL,
  `averageMonthEggCount` double NOT NULL,
  `wasDeleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_breed_to_chickens_idx` (`idBreed`),
  KEY `fk_cellCode_to_chickens_idx` (`idCellCode`),
  CONSTRAINT `fk_breed_to_chickens` FOREIGN KEY (`idBreed`) REFERENCES `breeds` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_cellCode_to_chickens` FOREIGN KEY (`idCellCode`) REFERENCES `cell_codes` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `chickens`
--

LOCK TABLES `chickens` WRITE;
/*!40000 ALTER TABLE `chickens` DISABLE KEYS */;
INSERT INTO `chickens` VALUES (1,1,1,300,4,52,0),(2,1,2,280,3,55,0),(3,1,3,315,12,46,0),(4,1,4,320,24,49,0),(5,1,5,298,14,50,0),(6,2,16,411,31,46,0),(7,2,17,416,3,48,0),(8,2,18,438,4,43,0),(9,2,19,426,7,42,0),(10,2,20,441,25,44,0),(11,3,25,501,12,39,0),(12,3,26,509,31,36,0),(13,3,27,516,13,32,0),(14,3,28,537,3,31,0),(15,3,29,496,1,40,0);
/*!40000 ALTER TABLE `chickens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `diets`
--

DROP TABLE IF EXISTS `diets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `diets` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `number` varchar(16) NOT NULL,
  `name` varchar(50) NOT NULL,
  `composition` varchar(200) NOT NULL,
  `wasDeleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `diets`
--

LOCK TABLES `diets` WRITE;
/*!40000 ALTER TABLE `diets` DISABLE KEYS */;
INSERT INTO `diets` VALUES (1,'ТП021','Свежий нут','нут, скорлупа, камешки, кукуруза, пшено',0),(2,'ТН011','Альфонс','пшено, кукуруза, скорлупа, пшеница, камешки',0);
/*!40000 ALTER TABLE `diets` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `egg_production`
--

DROP TABLE IF EXISTS `egg_production`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `egg_production` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `idChicken` int(10) unsigned NOT NULL,
  `idMonth` int(10) unsigned NOT NULL,
  `monthEggCount` int(11) NOT NULL,
  `wasDeleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_chicken_to_eggProduction_idx` (`idChicken`),
  KEY `fk_month_to_eggProduction_idx` (`idMonth`),
  CONSTRAINT `fk_chicken_to_eggProduction` FOREIGN KEY (`idChicken`) REFERENCES `chickens` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_month_to_eggProduction` FOREIGN KEY (`idMonth`) REFERENCES `month` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `egg_production`
--

LOCK TABLES `egg_production` WRITE;
/*!40000 ALTER TABLE `egg_production` DISABLE KEYS */;
INSERT INTO `egg_production` VALUES (1,1,4,53,0),(2,2,4,54,0),(3,3,4,46,0),(4,4,4,51,0),(5,5,4,48,0),(6,6,4,48,0),(7,7,4,47,0),(8,8,4,44,0),(9,9,4,40,0),(10,10,4,43,0),(11,11,4,39,0),(12,12,4,36,0),(13,13,4,32,0),(14,14,4,28,0),(15,15,4,44,0);
/*!40000 ALTER TABLE `egg_production` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `month`
--

DROP TABLE IF EXISTS `month`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `month` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `year` int(11) NOT NULL,
  `month` int(11) NOT NULL,
  `wasDeleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `month`
--

LOCK TABLES `month` WRITE;
/*!40000 ALTER TABLE `month` DISABLE KEYS */;
INSERT INTO `month` VALUES (1,2020,10,0),(2,2020,11,0),(3,2020,12,0),(4,2021,1,0);
/*!40000 ALTER TABLE `month` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `persons`
--

DROP TABLE IF EXISTS `persons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `persons` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `surname` varchar(80) NOT NULL,
  `name` varchar(80) NOT NULL,
  `patronymic` varchar(80) NOT NULL,
  `passport` varchar(20) NOT NULL,
  `wasDeleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `persons`
--

LOCK TABLES `persons` WRITE;
/*!40000 ALTER TABLE `persons` DISABLE KEYS */;
INSERT INTO `persons` VALUES (1,'Семечкин','Артур','Кононович','НГ563525',0),(2,'Севастьянов','Кирилл','Антонович','ЕК998374',0),(3,'Укапретова','Анна','Викторовна','ДЛ098364',0),(4,'Пармезан','Филарет','Ованович','ЗШ847562',0);
/*!40000 ALTER TABLE `persons` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `row_employments`
--

DROP TABLE IF EXISTS `row_employments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `row_employments` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `idRow` int(10) unsigned NOT NULL,
  `idWorker` int(10) unsigned NOT NULL,
  `workerRowPriority` int(11) NOT NULL,
  `wasDeleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_workers_to_rowEmployments_idx` (`idWorker`),
  KEY `fk_row_to_rowEmployments_idx` (`idRow`),
  CONSTRAINT `fk_row_to_rowEmployments` FOREIGN KEY (`idRow`) REFERENCES `rows` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_worker_to_rowEmployments` FOREIGN KEY (`idWorker`) REFERENCES `workers` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `row_employments`
--

LOCK TABLES `row_employments` WRITE;
/*!40000 ALTER TABLE `row_employments` DISABLE KEYS */;
INSERT INTO `row_employments` VALUES (1,1,1,1,0),(2,1,2,2,0),(3,2,1,1,0),(4,2,2,2,0),(5,3,1,1,0),(6,3,2,2,0),(7,4,2,1,0),(8,4,1,2,0),(9,5,2,1,0),(10,5,1,2,0),(11,6,3,1,0),(12,6,4,2,0),(13,7,3,1,0),(14,7,4,2,0),(15,8,3,1,0),(16,8,4,2,0),(17,9,4,1,0),(18,9,3,2,0),(19,10,4,1,0),(20,10,3,2,0),(21,11,4,1,0),(22,11,3,2,0);
/*!40000 ALTER TABLE `row_employments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rows`
--

DROP TABLE IF EXISTS `rows`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rows` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `number` varchar(16) NOT NULL,
  `wasDeleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rows`
--

LOCK TABLES `rows` WRITE;
/*!40000 ALTER TABLE `rows` DISABLE KEYS */;
INSERT INTO `rows` VALUES (1,'В001',0),(2,'В002',0),(3,'В003',0),(4,'В004',0),(5,'В005',0),(6,'С001',0),(7,'С002',0),(8,'С003',0),(9,'СВ001',0),(10,'СВ002',0),(11,'СВ003',0);
/*!40000 ALTER TABLE `rows` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `workers`
--

DROP TABLE IF EXISTS `workers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `workers` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `idPerson` int(10) unsigned NOT NULL,
  `salary` double NOT NULL,
  `wasDismissed` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `fk_persons_to_workers_idx` (`idPerson`),
  CONSTRAINT `fk_persons_to_workers` FOREIGN KEY (`idPerson`) REFERENCES `persons` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `workers`
--

LOCK TABLES `workers` WRITE;
/*!40000 ALTER TABLE `workers` DISABLE KEYS */;
INSERT INTO `workers` VALUES (1,1,14000,0),(2,2,12000,0),(3,3,17000,0),(4,4,19000,0);
/*!40000 ALTER TABLE `workers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `workshops`
--

DROP TABLE IF EXISTS `workshops`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `workshops` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `number` varchar(50) NOT NULL,
  `wasDeleted` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `number_UNIQUE` (`number`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `workshops`
--

LOCK TABLES `workshops` WRITE;
/*!40000 ALTER TABLE `workshops` DISABLE KEYS */;
INSERT INTO `workshops` VALUES (1,'01 Восточный ',0),(2,'02 Северный',0),(3,'03 Северо-восточный',0);
/*!40000 ALTER TABLE `workshops` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-01-18 13:10:26

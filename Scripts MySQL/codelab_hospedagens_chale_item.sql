-- MySQL dump 10.13  Distrib 8.0.28, for Win64 (x86_64)
--
-- Host: localhost    Database: codelab_hospedagens
-- ------------------------------------------------------
-- Server version	8.0.28

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
-- Table structure for table `chale_item`
--

DROP TABLE IF EXISTS `chale_item`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `chale_item` (
  `id_chale` varchar(36) NOT NULL,
  `nome_item` varchar(45) NOT NULL,
  PRIMARY KEY (`id_chale`,`nome_item`),
  KEY `item_chale_fk_idx` (`nome_item`),
  CONSTRAINT `chale_item_fk` FOREIGN KEY (`id_chale`) REFERENCES `chale` (`id`),
  CONSTRAINT `item_chale_fk` FOREIGN KEY (`nome_item`) REFERENCES `item` (`nome`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `chale_item`
--

LOCK TABLES `chale_item` WRITE;
/*!40000 ALTER TABLE `chale_item` DISABLE KEYS */;
INSERT INTO `chale_item` VALUES ('5ded1bf4-a0be-4310-8d2e-6bf0c58b835d','Chuveiro'),('a2d7aa19-75c3-492a-9641-5934e689ac30','Chuveiro'),('ec4a5545-e19f-461b-ac00-94800d0c9a56','Chuveiro'),('5ded1bf4-a0be-4310-8d2e-6bf0c58b835d','Cobertor'),('a2d7aa19-75c3-492a-9641-5934e689ac30','Frigobar'),('de7da4c6-33e4-4918-9427-3f1716573957','Frigobar'),('ec4a5545-e19f-461b-ac00-94800d0c9a56','Frigobar'),('5ded1bf4-a0be-4310-8d2e-6bf0c58b835d','Piscina'),('ec4a5545-e19f-461b-ac00-94800d0c9a56','Piscina'),('5ded1bf4-a0be-4310-8d2e-6bf0c58b835d','Travesseiro'),('c216a268-d703-4f51-8b44-45e9d861f0c4','Travesseiro'),('de7da4c6-33e4-4918-9427-3f1716573957','Travesseiro');
/*!40000 ALTER TABLE `chale_item` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-03-07  1:59:46

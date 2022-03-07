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
-- Table structure for table `hospedagem`
--

DROP TABLE IF EXISTS `hospedagem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hospedagem` (
  `id` varchar(36) NOT NULL,
  `id_chale` varchar(36) NOT NULL,
  `id_cliente` varchar(36) NOT NULL,
  `data_inicio` datetime NOT NULL,
  `data_fim` datetime NOT NULL,
  `quantidade_pessoas` int NOT NULL,
  `desconto` decimal(19,2) NOT NULL,
  `valor_final` decimal(19,2) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `cliente_hospedagem_fk_idx` (`id_cliente`),
  KEY `chale_hospedagem_fk_idx` (`id_chale`),
  CONSTRAINT `chale_hospedagem_fk` FOREIGN KEY (`id_chale`) REFERENCES `chale` (`id`),
  CONSTRAINT `cliente_hospedagem_fk` FOREIGN KEY (`id_cliente`) REFERENCES `cliente` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hospedagem`
--

LOCK TABLES `hospedagem` WRITE;
/*!40000 ALTER TABLE `hospedagem` DISABLE KEYS */;
INSERT INTO `hospedagem` VALUES ('ca463ff2-3eb0-4469-b4fb-179028384a3c','de7da4c6-33e4-4918-9427-3f1716573957','3b6dd449-678a-4d07-858f-dbd5cbc47ebe','2022-03-12 00:00:00','2022-03-15 00:00:00',2,20.00,280.00),('ce6865dd-3276-48a5-9e1f-a38ae4d963b2','5ded1bf4-a0be-4310-8d2e-6bf0c58b835d','04d57184-41b4-48b4-b96c-167256181a6b','2022-03-06 00:00:00','2022-03-07 00:00:00',2,50.00,150.00),('db48116d-a08c-4b6e-aad5-a27a797eff38','de7da4c6-33e4-4918-9427-3f1716573957','3b6dd449-678a-4d07-858f-dbd5cbc47ebe','2022-03-06 00:00:00','2022-03-10 00:00:00',2,20.00,280.00),('db48116d-a08c-4b6e-aad5-a27a997eff38','5ded1bf4-a0be-4310-8d2e-6bf0c58b835d','3b6dd449-678a-4d07-858f-dbd5cbc47ebe','2022-03-04 00:00:00','2022-03-05 00:00:00',2,20.00,294.00);
/*!40000 ALTER TABLE `hospedagem` ENABLE KEYS */;
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

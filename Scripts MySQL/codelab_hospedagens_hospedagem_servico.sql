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
-- Table structure for table `hospedagem_servico`
--

DROP TABLE IF EXISTS `hospedagem_servico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hospedagem_servico` (
  `id_hospedagem` varchar(36) NOT NULL,
  `id_servico` varchar(45) NOT NULL,
  `valor` decimal(19,2) NOT NULL,
  PRIMARY KEY (`id_hospedagem`,`id_servico`),
  KEY `servico_hospedagem_fk_idx` (`id_servico`),
  CONSTRAINT `hospedagem_servico_fk` FOREIGN KEY (`id_hospedagem`) REFERENCES `hospedagem` (`id`),
  CONSTRAINT `servico_hospedagem_fk` FOREIGN KEY (`id_servico`) REFERENCES `servico` (`nome`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hospedagem_servico`
--

LOCK TABLES `hospedagem_servico` WRITE;
/*!40000 ALTER TABLE `hospedagem_servico` DISABLE KEYS */;
INSERT INTO `hospedagem_servico` VALUES ('ca463ff2-3eb0-4469-b4fb-179028384a3c','CafeManha',100.00),('ce6865dd-3276-48a5-9e1f-a38ae4d963b2','CafeManha',100.00),('db48116d-a08c-4b6e-aad5-a27a797eff38','CafeManha',0.00);
/*!40000 ALTER TABLE `hospedagem_servico` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-03-07  1:59:47

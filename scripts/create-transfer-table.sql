CREATE TABLE `Transfer` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ClientId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `ContactId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Value` decimal(65,30) NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `CreatedBy` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
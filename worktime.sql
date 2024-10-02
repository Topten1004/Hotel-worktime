SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for __efmigrationshistory
-- ----------------------------
DROP TABLE IF EXISTS `__efmigrationshistory`;
CREATE TABLE `__efmigrationshistory`  (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`MigrationId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of __efmigrationshistory
-- ----------------------------
INSERT INTO `__efmigrationshistory` VALUES ('20230621003653_initial', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230621015841_databaUdate', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230622074114_addPointer', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230622075753_addPassage', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230622081414_addLogTime', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230623071245_addCode', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230623081834_latestDatabase', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230629060456_addType', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230629121512_removeType', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230706151715_addUsers', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230708201925_addWebAccess', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230708201937_Updatedatabase', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230708202141_addAccess', '6.0.1');
INSERT INTO `__efmigrationshistory` VALUES ('20230724200200_initDatabase', '6.0.1');

-- ----------------------------
-- Table structure for employee
-- ----------------------------
DROP TABLE IF EXISTS `employee`;
CREATE TABLE `employee`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SSN` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `FirstName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `LastName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `EntryDate` datetime(6) NOT NULL,
  `TAG` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `WebAccess` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 22 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of employee
-- ----------------------------
INSERT INTO `employee` VALUES (1, '392261 ', 'Gregory ', 'RECHE', '2023-06-15 00:00:00.000000', '165549786', 0);
INSERT INTO `employee` VALUES (2, '293132 ', 'Sesilia', 'TIIHIVA', '2023-06-15 00:00:00.000000', '464688979', 0);
INSERT INTO `employee` VALUES (3, '204655', 'Victorine ', 'CILANE', '2023-06-15 00:00:00.000000', '455511231', 0);
INSERT INTO `employee` VALUES (4, '375617 ', 'Gabriel', 'TALIA', '2023-06-15 00:00:00.000000', '121645894', 0);
INSERT INTO `employee` VALUES (5, '274856 ', 'Brigitte', 'ILOAI', '2023-06-15 00:00:00.000000', '112235687', 0);

-- ----------------------------
-- Table structure for passage
-- ----------------------------
DROP TABLE IF EXISTS `passage`;
CREATE TABLE `passage`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `EmployeeId` int NOT NULL,
  `PointerId` int NOT NULL,
  `LogTime` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `IX_Passage_EmployeeId`(`EmployeeId` ASC) USING BTREE,
  INDEX `IX_Passage_PointerId`(`PointerId` ASC) USING BTREE,
  CONSTRAINT `FK_Passage_Employee_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `employee` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT,
  CONSTRAINT `FK_Passage_Pointer_PointerId` FOREIGN KEY (`PointerId`) REFERENCES `pointer` (`Id`) ON DELETE CASCADE ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 55 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of passage
-- ----------------------------

-- ----------------------------
-- Table structure for pointer
-- ----------------------------
DROP TABLE IF EXISTS `pointer`;
CREATE TABLE `pointer`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Code` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of pointer
-- ----------------------------
INSERT INTO `pointer` VALUES (1, 'Pointeuse B2N', '99999');
INSERT INTO `pointer` VALUES (2, 'Web Employee', '00000');

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Login` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `MDP` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Role` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES (1, 'Gregory ', 'RECHE', 'chs@chs.nc', 'admin', 'Admin5', 'MANAGER');
INSERT INTO `user` VALUES (3, 'Chris', 'Groe', 'c@gmail.com', 'chris', 'chris', 'EMPLOYEE');
INSERT INTO `user` VALUES (4, 'Sesilia', 'TIIHIVA', 'c@gmail.com', 'employee1', 'employee1', 'EMPLOYEE');
INSERT INTO `user` VALUES (5, 'Victorine ', 'CILANE', 'c@gmail.com', 'employee2', 'employee2', 'EMPLOYEE');
INSERT INTO `user` VALUES (6, 'Gabriel', 'TALIA', 'c@gmail.com', 'employee3', 'employee3', 'EMPLOYEE');
INSERT INTO `user` VALUES (7, 'Brigitte', 'ILOAI', 'c@gmail.com', 'employee4', 'employee4', 'EMPLOYEE');

SET FOREIGN_KEY_CHECKS = 1;

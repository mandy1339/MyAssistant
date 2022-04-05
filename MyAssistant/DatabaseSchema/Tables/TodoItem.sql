CREATE TABLE `TodoItem` (
  `PKey` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(250) CHARACTER SET utf8 NOT NULL,
  `CreatedDate` datetime DEFAULT CURRENT_TIMESTAMP,
  `DueDate` date DEFAULT NULL,
  `Category` enum('W','P','S') NOT NULL,
  `IsComplete` bit(1) NOT NULL,
  `Priority` tinyint(3) unsigned NOT NULL,
  `DateCompleted` datetime DEFAULT NULL,
  `UserID` int(11) DEFAULT NULL,
  `GroupID` int(11) DEFAULT NULL,
  PRIMARY KEY (`PKey`)
) ENGINE=InnoDB AUTO_INCREMENT=243 DEFAULT CHARSET=latin1;

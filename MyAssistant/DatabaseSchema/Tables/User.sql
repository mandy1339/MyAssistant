CREATE TABLE `User` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(100) NOT NULL,
  `Password` varchar(255) CHARACTER SET utf8 NOT NULL,
  `PhoneNumber` varchar(32) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `GroupID` int(10) unsigned NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

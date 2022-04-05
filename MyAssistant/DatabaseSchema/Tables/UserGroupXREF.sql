CREATE TABLE `UserGroupXREF` (
  `UserID` int(10) unsigned NOT NULL,
  `GroupID` int(10) unsigned NOT NULL,
  PRIMARY KEY (`UserID`,`GroupID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

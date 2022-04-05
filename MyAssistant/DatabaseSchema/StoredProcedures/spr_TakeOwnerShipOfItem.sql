DELIMITER $$
CREATE PROCEDURE spr_TakeOwnershipOfItem (
	IN ID INT,
    IN UserID INT
)
BEGIN
	UPDATE `TodoItem` SET `UserID` = UserID WHERE `PKey` = ID;
END
$$
DELIMITER ;
DELIMITER $$
CREATE DEFINER=`mandy1339`@`%` PROCEDURE `spr_AddItem`(
-- Pass NULL Group ID if creating for an individual
-- Pass NULL UserID if creating for a group
	OUT PKey INT,
	IN Description NVARCHAR(250),
	IN DueDate DATE,
	IN Category CHAR(1),
	IN IsComplete BIT,
	IN Priority TINYINT,
    IN UserID INT,
    IN GroupID INT
)
BEGIN
	-- Handle Creating Item For Individual (Null GroupID)    
	IF (GroupID IS NULL AND UserID IS NOT NULL)
    THEN
		INSERT INTO TodoItem (
			Description,
			DueDate,
			Category,
			IsComplete,
			Priority,
            UserID
		)
		VALUES (
			Description,
			DueDate,
			Category,
			IsComplete,
			Priority,
            UserID
		);
        SELECT LAST_INSERT_ID() INTO PKey;
	-- Handle Creating Item For Group (Null UserID)  
	ELSEIF (UserID IS NULL AND GroupID IS NOT NULL)
    THEN
		INSERT INTO TodoItem (
			Description,
			DueDate,
			Category,
			IsComplete,
			Priority,
            GroupID
		)
		VALUES (
			Description,
			DueDate,
			Category,
			IsComplete,
			Priority,
            GroupID
		);
        SELECT LAST_INSERT_ID() INTO PKey;
    ELSE
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'GroupID OR UserID Must Be NULL';
	END IF;
    
	SELECT LAST_INSERT_ID() INTO PKey;
END$$
DELIMITER ;

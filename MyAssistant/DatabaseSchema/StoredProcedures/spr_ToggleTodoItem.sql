DELIMITER $$
CREATE DEFINER=`mandy1339`@`%` PROCEDURE `spr_ToggleTodoItem`(
	ID INT
)
BEGIN
	-- HANDLE TOGGLE ON
	IF ((SELECT IsComplete FROM TodoItem WHERE PKey = ID ) = 0)
	THEN
		UPDATE TodoItem SET IsComplete = 1, DateCompleted = NOW() WHERE PKey = ID;
	
	-- HANDLE TOGGLE OFF
	ELSE	
		UPDATE TodoItem SET IsComplete = 0, DateCompleted = NULL WHERE PKey = ID;
	END IF;
END$$
DELIMITER ;

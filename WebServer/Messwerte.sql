/*10.000 Messwerte in den letzten 10 Jahre verteilt*/

--Seit 2004 ungefähr 10 Jahre, Temperaturen zwischen -15 bis 30 Grad 

DECLARE @i int
SET @i = 0
	WHILE (@i < 10000)
	BEGIN 
		INSERT INTO TempSensor
		VALUES (-15+(30-(-15))*RAND(CHECKSUM(NEWID())), DATEADD(DAY, ABS(CHECKSUM(NEWID()) % 3650), '2004-01-01'));
	SET @i = (@i + 1)
END

--Dauer 3 sek 

SELECT  * from TempSensor 
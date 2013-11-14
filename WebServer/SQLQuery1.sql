DROP TABLE TempSensor

CREATE TABLE TempSensor
(
	Temperatur int not null,
	Datum Date 
);

INSERT INTO TempSensor
VALUES(18, '2007-04-30');

INSERT INTO TempSensor
VALUES(14, GETDATE());

INSERT INTO TempSensor
VALUES(20, '2008-09-19');

INSERT INTO TempSensor
VALUES(-1, '2012-01-01');

INSERT INTO TempSensor
VALUES(0, '2013-02-12');
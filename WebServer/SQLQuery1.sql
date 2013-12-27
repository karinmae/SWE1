DROP TABLE TempSensor

CREATE TABLE TempSensor
(
	id INT IDENTITY NOT NULL PRIMARY KEY ,
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

SELECT Temperatur, Datum FROM TempSensor WHERE Datum = '2013-02-12' Order by Datum;

SELECT id, Name, Beschreibung FROM Esoterik WHERE id= '7' Order by id;

--Esoterik Plugin
DROP TABLE Esoterik;

CREATE TABLE Esoterik
(
	id INT PRIMARY KEY,
	Name varchar(30),
	Beschreibung varchar (300)
);

INSERT INTO Esoterik
VALUES(0, 'Lady Heather Mills', 'Eine britische Baronin');

INSERT INTO Esoterik
VALUES(1,'Feng Hua', 'Ein chinesischer Soldat');

INSERT INTO Esoterik
VALUES(2,'Paloma Rodriguez', 'Eine spanische Tänzerin');

INSERT INTO Esoterik
VALUES(3,'Sir Edgar Ravenswood', 'Ein britischer Lord');

INSERT INTO Esoterik
VALUES(4,'Fei Luang', 'Eine chinesische Söldnerin');

INSERT INTO Esoterik
VALUES(5,'Thorbjörn Gudmunson', 'Ein isländischer Wikinger');

INSERT INTO Esoterik
VALUES(6,'Halldora Ibensdottir', 'Eine isländische Wikingerfrau');

INSERT INTO Esoterik
VALUES(7,'Gomez del Bosque', 'Ein spanischer Waldarbeiter');

INSERT INTO Esoterik
VALUES(8,'Keoni', 'Ein polynesischer Entdecker');

INSERT INTO Esoterik
VALUES(9,'Noelani', 'Eine polynesische Entdeckerin');
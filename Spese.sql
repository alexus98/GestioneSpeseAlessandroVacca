CREATE DATABASE GestioneSpese;

CREATE TABLE Categoria
(
	Id INT IDENTITY(1,1) NOT NULL,
	Categoria VARCHAR(100) NOT NULL
	CONSTRAINT PK_Categoria PRIMARY KEY(Id)
);

CREATE TABLE Spesa
(
	Id INT IDENTITY(1,1) NOT NULL,
	Data DATETIME NOT NULL,
	CategoriaId INT NOT NULL,
	Descrizione VARCHAR(500) NOT NULL,
	Utente VARCHAR(100) NOT NULL,
	Importo DECIMAL(5,2) NOT NULL,
	Approvato BIT NOT NULL,
	CONSTRAINT PK_Spese PRIMARY KEY(Id),
	CONSTRAINT FK_SpeseCategorie FOREIGN KEY(CategoriaId) REFERENCES Categoria(Id)
);

INSERT INTO Categoria VALUES ('Elettronica');
INSERT INTO Categoria VALUES ('Carne');
INSERT INTO Categoria VALUES ('Frutta e verdura');
INSERT INTO Categoria VALUES ('Casa');

INSERT INTO Spesa VALUES('2021-10-05 17:20:00',1,'Smartphone android','Alessandro',200,1);
INSERT INTO Spesa VALUES('2021-11-05 11:20:00',4,'Albero di natale','Francesca',50.99,1);
INSERT INTO Spesa VALUES('2021-12-23 10:30:00',3,'Pomodori','Lucia',2.50,0);
INSERT INTO Spesa VALUES('2021-10-23 17:40:00',2,'Porceddu','Efisio',15.50,0);
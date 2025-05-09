CREATE DATABASE AgendaContactos

CREATE TABLE Categorias (
Id INT PRIMARY KEY IDENTITY,
Nombre NVARCHAR(50)
);

INSERT INTO Categorias (Nombre) VALUES ('Familia'), ('Amigos'), ('Trabajo');

CREATE TABLE Contactos (
Codigo INT PRIMARY KEY IDENTITY,
Nombre NVARCHAR(100),
Apellido NVARCHAR(100),
Telefono NVARCHAR(100),
Correo NVARCHAR(150),
CategoriaId int,
FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id)
);
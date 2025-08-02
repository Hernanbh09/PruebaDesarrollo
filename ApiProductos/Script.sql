CREATE DATABASE ApiProductos;
GO

USE ApiProductos;
GO

CREATE TABLE Productos (
    Id INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(18,2) NOT NULL
);
GO

INSERT INTO Productos (Nombre, Precio) VALUES
('Portatil', 100000.00),
('Teclado', 35000.00);
GO

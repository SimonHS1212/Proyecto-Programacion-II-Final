-- Crear la base de datos
CREATE DATABASE GestionEquipos;
GO

-- Usar la base de datos
USE GestionEquipos;
GO

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    CorreoElectronico NVARCHAR(50) UNIQUE NOT NULL,
    Telefono NVARCHAR(20) NOT NULL
);

-- Tabla de Técnicos
CREATE TABLE Tecnicos (
    TecnicoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Especialidad NVARCHAR(50) NOT NULL
);

-- Tabla de Equipos
CREATE TABLE Equipos (
    EquipoID INT PRIMARY KEY IDENTITY(1,1),
    TipoEquipo NVARCHAR(50) NOT NULL,
    Modelo NVARCHAR(50) NOT NULL,
    UsuarioID INT,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID) ON DELETE SET NULL
);

-- Tabla de Reparaciones
CREATE TABLE Reparaciones (
    ReparacionID INT PRIMARY KEY IDENTITY(1,1),
    EquipoID INT NOT NULL,
    FechaSolicitud DATE NOT NULL DEFAULT GETDATE(),
    Estado NVARCHAR(50) NOT NULL,
    FOREIGN KEY (EquipoID) REFERENCES Equipos(EquipoID) ON DELETE CASCADE
);

-- Tabla de Detalles de Reparación
CREATE TABLE DetallesReparacion (
    DetalleID INT PRIMARY KEY IDENTITY(1,1),
    ReparacionID INT NOT NULL,
    Descripcion NVARCHAR(MAX) NOT NULL,
    FechaInicio DATE NULL,
    FechaFin DATE NULL,
    FOREIGN KEY (ReparacionID) REFERENCES Reparaciones(ReparacionID) ON DELETE CASCADE
);

-- Tabla de Asignaciones
CREATE TABLE Asignaciones (
    AsignacionID INT PRIMARY KEY IDENTITY(1,1),
    ReparacionID INT NOT NULL,
    TecnicoID INT NOT NULL,
    FechaAsignacion DATE NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (ReparacionID) REFERENCES Reparaciones(ReparacionID) ON DELETE CASCADE,
    FOREIGN KEY (TecnicoID) REFERENCES Tecnicos(TecnicoID) ON DELETE CASCADE
	);
	Go

-- Procedimientos para la tabla Usuarios
CREATE PROCEDURE InsertarUsuario
    @Nombre NVARCHAR(50),
    @CorreoElectronico NVARCHAR(50),
    @Telefono NVARCHAR(20)
AS
BEGIN
    INSERT INTO Usuarios (Nombre, CorreoElectronico, Telefono)
    VALUES (@Nombre, @CorreoElectronico, @Telefono);
END;
GO

CREATE PROCEDURE ConsultarUsuarios
AS
BEGIN
    SELECT * FROM Usuarios;
END;
Go
CREATE PROCEDURE ActualizarUsuario
    @UsuarioID INT,
    @Nombre NVARCHAR(50),
    @CorreoElectronico NVARCHAR(50),
    @Telefono NVARCHAR(20)
AS
BEGIN
    UPDATE Usuarios
    SET Nombre = @Nombre, CorreoElectronico = @CorreoElectronico, Telefono = @Telefono
    WHERE UsuarioID = @UsuarioID;
END;
Go
CREATE PROCEDURE EliminarUsuario
    @UsuarioID INT
AS
BEGIN
    DELETE FROM Usuarios WHERE UsuarioID = @UsuarioID;
END;
Go
CREATE PROCEDURE InsertarTecnico
    @Nombre NVARCHAR(50),
    @Especialidad NVARCHAR(50)
AS
BEGIN
    INSERT INTO Tecnicos (Nombre, Especialidad)
    VALUES (@Nombre, @Especialidad);
END;
Go
CREATE PROCEDURE ConsultarTecnicos
AS
BEGIN
    SELECT * FROM Tecnicos;
END;
Go
CREATE PROCEDURE ActualizarTecnico
    @TecnicoID INT,
    @Nombre NVARCHAR(50),
    @Especialidad NVARCHAR(50)
AS
BEGIN
    UPDATE Tecnicos
    SET Nombre = @Nombre, Especialidad = @Especialidad
    WHERE TecnicoID = @TecnicoID;
END;
Go
CREATE PROCEDURE EliminarTecnico
    @TecnicoID INT
AS
BEGIN
    DELETE FROM Tecnicos WHERE TecnicoID = @TecnicoID;
END;
Go
CREATE PROCEDURE InsertarEquipo
    @TipoEquipo NVARCHAR(50),
    @Modelo NVARCHAR(50),
    @UsuarioID INT
AS
BEGIN
    INSERT INTO Equipos (TipoEquipo, Modelo, UsuarioID)
    VALUES (@TipoEquipo, @Modelo, @UsuarioID);
END;
Go
CREATE PROCEDURE ConsultarEquipos
AS
BEGIN
    SELECT * FROM Equipos;
END;
Go
CREATE PROCEDURE ActualizarEquipo
    @EquipoID INT,
    @TipoEquipo NVARCHAR(50),
    @Modelo NVARCHAR(50),
    @UsuarioID INT
AS
BEGIN
    UPDATE Equipos
    SET TipoEquipo = @TipoEquipo, Modelo = @Modelo, UsuarioID = @UsuarioID
    WHERE EquipoID = @EquipoID;
END;
Go
CREATE PROCEDURE EliminarEquipo
    @EquipoID INT
AS
BEGIN
    DELETE FROM Equipos WHERE EquipoID = @EquipoID;
END;
Go
CREATE PROCEDURE InsertarReparacion
    @EquipoID INT,
    @Estado NVARCHAR(50)
AS
BEGIN
    INSERT INTO Reparaciones (EquipoID, FechaSolicitud, Estado)
    VALUES (@EquipoID, GETDATE(), @Estado);
END;
Go
CREATE PROCEDURE ConsultarReparaciones
AS
BEGIN
    SELECT * FROM Reparaciones;
END;
Go
CREATE PROCEDURE ActualizarReparacion
    @ReparacionID INT,
    @Estado NVARCHAR(50)
AS
BEGIN
    UPDATE Reparaciones
    SET Estado = @Estado
    WHERE ReparacionID = @ReparacionID;
END;
Go
CREATE PROCEDURE EliminarReparacion
    @ReparacionID INT
AS
BEGIN
    DELETE FROM Reparaciones WHERE ReparacionID = @ReparacionID;
END;
Go
CREATE PROCEDURE InsertarDetalleReparacion
    @ReparacionID INT,
    @Descripcion NVARCHAR(MAX),
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    INSERT INTO DetallesReparacion (ReparacionID, Descripcion, FechaInicio, FechaFin)
    VALUES (@ReparacionID, @Descripcion, @FechaInicio, @FechaFin);
END;
Go
CREATE PROCEDURE ConsultarDetallesReparacion
AS
BEGIN
    SELECT * FROM DetallesReparacion;
END;
Go
CREATE PROCEDURE ActualizarDetalleReparacion
    @DetalleID INT,
    @Descripcion NVARCHAR(MAX),
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    UPDATE DetallesReparacion
    SET Descripcion = @Descripcion, FechaInicio = @FechaInicio, FechaFin = @FechaFin
    WHERE DetalleID = @DetalleID;
END;
Go
CREATE PROCEDURE EliminarDetalleReparacion
    @DetalleID INT
AS
BEGIN
    DELETE FROM DetallesReparacion WHERE DetalleID = @DetalleID;
END;
Go
CREATE PROCEDURE InsertarAsignacion
    @ReparacionID INT,
    @TecnicoID INT
AS
BEGIN
    INSERT INTO Asignaciones (ReparacionID, TecnicoID, FechaAsignacion)
    VALUES (@ReparacionID, @TecnicoID, GETDATE());
END;
Go
CREATE PROCEDURE ConsultarAsignaciones
AS
BEGIN
    SELECT * FROM Asignaciones;
END;
Go
CREATE PROCEDURE ActualizarAsignacion
    @AsignacionID INT,
    @ReparacionID INT,
    @TecnicoID INT
AS
BEGIN
    UPDATE Asignaciones
    SET ReparacionID = @ReparacionID, TecnicoID = @TecnicoID
    WHERE AsignacionID = @AsignacionID;
END;
Go
CREATE PROCEDURE EliminarAsignacion
    @AsignacionID INT
AS
BEGIN
    DELETE FROM Asignaciones WHERE AsignacionID = @AsignacionID;
END;
Go

USE GestionEquipos;


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="ExamenFinal_Alejandro.CapaVista.Menu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Menú Principal</title>
    <style>
        /* Estilos generales */
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
        }

        h1 {
            text-align: center;
            color: #333;
            margin-top: 20px;
        }

        p {
            text-align: center;
            color: #666;
        }

        /* Estilo del menú */
        ul {
            list-style-type: none;
            margin: 0;
            padding: 0;
            overflow: hidden;
            background-color: #333;
            display: flex;
            justify-content: center;
        }

        ul li {
            display: inline;
        }

        ul li a {
            display: block;
            color: white;
            text-align: center;
            padding: 14px 20px;
            text-decoration: none;
            transition: background-color 0.3s;
        }

        ul li a:hover {
            background-color: #575757;
        }

        ul li a.active {
            background-color: #4CAF50;
        }

        /* Contenedor principal */
        .container {
            margin: 30px auto;
            padding: 20px;
            background: white;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            max-width: 600px;
            text-align: center;
        }

        .btn {
            display: inline-block;
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            margin: 10px;
            border: none;
            border-radius: 4px;
            text-decoration: none;
            font-size: 16px;
            transition: background-color 0.3s;
        }

        .btn:hover {
            background-color: #45a049;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Menú de navegación -->
        <ul>
            <li><a class="active" href="Menu.aspx">Home</a></li>
            <li><a href="Usuarios.aspx">Usuarios</a></li>
            <li><a href="Equipos.aspx">Equipos</a></li>
            <li><a href="Tecnicos.aspx">Técnicos</a></li>
            <li><a href="Reparaciones.aspx">Reparaciones</a></li>
            <li><a href="DetallesReparacion.aspx">Detalles Reparación</a></li>
            <li><a href="Asignaciones.aspx">Asignaciones</a></li>
        </ul>

        <!-- Contenido principal -->
        <div class="container">
            <h1>Menú Principal</h1>
            <p>Seleccione una opción del menú para gestionar el sistema.</p>
            <a href="Usuarios.aspx" class="btn">Gestionar Usuarios</a>
            <a href="Equipos.aspx" class="btn">Gestionar Equipos</a>
            <a href="Tecnicos.aspx" class="btn">Gestionar Técnicos</a>
            <a href="Reparaciones.aspx" class="btn">Gestionar Reparaciones</a>
            <a href="DetallesReparacion.aspx" class="btn">Gestionar Detalles de Reparaciones</a>
            <a href="Asignaciones.aspx" class="btn">Gestionar Asignaciones</a>
        </div>
    </form>
</body>
</html>

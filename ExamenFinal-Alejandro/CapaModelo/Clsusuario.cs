using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenFinal_Alejandro.CapaModelo
{
    public class Clsusuario
    {
        // Aributos

		private static int id;
		private static string nombre;
        private static string Correo;
        private static string Telefono;


        // Constructor

        public Clsusuario() 
        {
            
        }


        // Metodos

        public int Getid() 
        { 
            return id; 
        
        }

        //Setter = Asignar un valor a un archivo metodo void asignar

        public void Setid(int Id)
        {
            id = Id;
        }

        public string Getnombre()
        {
            return nombre;
        }

        //Setter = Asignar un valor a un archivo metodo void asignar

        public void Setnombre(String Nombre)
        {
            nombre = Nombre;
        }

    }
}
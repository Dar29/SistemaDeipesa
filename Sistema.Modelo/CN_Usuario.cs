using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Datos;
using Sistema.Entidades;
using Sistema.Modelo.Recursos;

namespace Sistema.Modelo
{
    public class CN_Usuario
    {
        private CD_Usuario objSistemaDatos = new CD_Usuario();

        public List<Usuarios> Listar()
        {
            return objSistemaDatos.Listar();
        }

        public int Registrar(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El campo nombres es requerido";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El campo apellidos es requerido";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El campo correo es requerido";
            }


            // FALTA LOGICA
            if (string.IsNullOrEmpty(Mensaje))
            {

                string clave = CN_Recursos.GenerarClave();

                string asunto = "CREACION CUENTA";
                string msj_correo = "<h3>Su cuenta fue creada correctamente</h3></br><p>Su clave para acceder es: !clave!</p>";
                msj_correo = msj_correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, msj_correo);

                if (respuesta)
                {
                    obj.Contrasenia = Encriptador.Encriptar(clave);
                    return objSistemaDatos.Registrar(obj, out Mensaje);
                }

                obj.Contrasenia = Encriptador.Encriptar(clave);

                return objSistemaDatos.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "El campo nombres es requerido";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El campo apellidos es requerido";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El campo correo es requerido";
            }

            // FALTA LOGICA
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objSistemaDatos.Editar(obj, out Mensaje);

            }
            else
            {
                return false;
            }

        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objSistemaDatos.Eliminar(id, out Mensaje);
        }
    }
}

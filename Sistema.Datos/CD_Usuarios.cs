using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace Sistema.Datos
{
    public class CD_Usuario
    {
        public List<Usuarios> Listar()
        { 
            List<Usuarios> lista = new List<Usuarios>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) {

                    string query = "select IdUsuario, Nombres, Apellidos, Usuario, Correo, Estado from Tbl_Usuario";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        
                        while (dr.Read())
                        {
                            lista.Add(new Usuarios {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString(),
                                Usuario = dr["Usuario"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Estado = Convert.ToInt32(dr["Estado"])
                            
                            });
                        }
                        
                    }
                }


            }
            catch (Exception)
            {

                lista = new List<Usuarios>();
            }


            return lista;
        }
    }
}

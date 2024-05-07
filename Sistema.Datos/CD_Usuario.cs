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

                    string query = "select * from Tbl_Usuario";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        
                        while (dr.Read())
                        {
                            lista.Add(new Usuarios {
                                IdUsuario = dr["IdUsuario"] != DBNull.Value ? Convert.ToInt32(dr["IdUsuario"]) : 0,
                                Nombres = dr["Nombres"] != DBNull.Value ? dr["Nombres"].ToString() : string.Empty,
                                Apellidos = dr["Apellidos"] != DBNull.Value ? dr["Apellidos"].ToString() : string.Empty,
                                Usuario = dr["Usuario"] != DBNull.Value ? dr["Usuario"].ToString() : string.Empty,
                                Correo = dr["Correo"] != DBNull.Value ? dr["Correo"].ToString() : string.Empty,
                                Contrasenia = dr["Contrasenia"] != DBNull.Value ? dr["Contrasenia"].ToString() : string.Empty,
                                Reestablecer = dr["Reestablecer"] != DBNull.Value ? Convert.ToBoolean(dr["Reestablecer"]) : false,
                                FechaRegistro = dr["FechaRegistro"] != DBNull.Value ? Convert.ToDateTime(dr["FechaRegistro"]) : DateTime.MinValue,
                                FechaModificacion = dr["FechaModificacion"] != DBNull.Value ? Convert.ToDateTime(dr["FechaModificacion"]) : DateTime.MinValue,
                                Activo = dr["Activo"] != DBNull.Value ? Convert.ToBoolean(dr["Activo"]) : false


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

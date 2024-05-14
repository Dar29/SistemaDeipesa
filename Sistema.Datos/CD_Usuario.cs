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


        public int Registrar(Usuarios obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistraUsuario", oconexion);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Contrasenia", obj.Contrasenia);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }

            return idautogenerado;
        }

        public bool Editar(Usuarios obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("Nombres", obj.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", obj.Apellidos);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top(1) from Tbl_Usuario where IdUsuario = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;

                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;

        }
    }
}

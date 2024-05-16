using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Entidades;
using System.Data.SqlClient;
using System.Data;
using Sistema.Entidades.Utils;

namespace Sistema.Datos
{
    public class CD_Categoria
    {
        public List<CategoriaMaterial> Listar()
        { 
            var lista = new List<CategoriaMaterial>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select * from Tbl_Categoria";
                    SqlCommand cmd = new SqlCommand(query, oconexion)
                    {
                        CommandType = CommandType.Text
                    };

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lista.Add(new CategoriaMaterial
                            {
                                IdCategoria = dr["IdCategoria"] != DBNull.Value ? Convert.ToInt32(dr["IdCategoria"]) : default,
                                Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : default,
                                Estado = dr["Estado"] != DBNull.Value ? Convert.ToInt32(dr["Estado"]) : default,
                            });
                        }

                    }
                }
            }
            catch { }

            return lista;
        }

        public CategoriaMaterial ObtenerPorId(int id)
        {
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select TOP 1 * from Tbl_Categoria WHERE IdCategoria = @Id";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();

                        return new CategoriaMaterial
                        {
                            IdCategoria = dr["IdCategoria"] != DBNull.Value ? Convert.ToInt32(dr["IdCategoria"]) : 0,
                            Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : string.Empty,
                            Estado = dr["Estado"] != DBNull.Value ? Convert.ToInt32(dr["Estado"]) : 0,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Resultado GuardarOActualizar(CategoriaMaterial obj)
        {
            var guardado = false;
            var mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd;

                    if (obj.IdCategoria == 0)
                        cmd = new SqlCommand("[sp_CrearCategoria]", oconexion);
                    else
                    {
                        cmd = new SqlCommand("[sp_ActualizarCategoria]", oconexion);
                        cmd.Parameters.AddWithValue("IdCategoria", obj.IdCategoria);
                    }

                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    guardado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return new Resultado(guardado, mensaje);
        }

        public Resultado Desactivar(int id)
        {
            var desactivado = false;
            var mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd;

                    cmd = new SqlCommand("[sp_DesactivarCategoria]", oconexion);
                    cmd.Parameters.AddWithValue("IdCategoria", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    desactivado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }

            return new Resultado(desactivado, mensaje);
        }
    }
}

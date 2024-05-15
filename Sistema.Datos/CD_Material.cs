using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema.Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using Sistema.Entidades.Utils;

namespace Sistema.Datos
{
    public class CD_Material
    {
        public List<VMaterialesDetalle> Listar()
        { 
            List<VMaterialesDetalle> lista = new List<VMaterialesDetalle>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) {

                    string query = "select * from VMaterialesDetalle";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        
                        while (dr.Read())
                        {
                            lista.Add(new VMaterialesDetalle
                            {
                                IdMaterial = dr["IdMaterial"] != DBNull.Value ? Convert.ToInt32(dr["IdMaterial"]) : 0,
                                Nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : string.Empty,
                                Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : string.Empty,
                                Categoria = dr["Categoria"] != DBNull.Value ? dr["Categoria"].ToString() : string.Empty,
                                Estado = dr["Estado"] != DBNull.Value ? dr["Estado"].ToString() : string.Empty

                            });
                        }
                        
                    }
                }


            }
            catch (Exception)
            {

                lista = new List<VMaterialesDetalle>();
            }


            return lista;
        }

        public Material ObtenerPorId(int id)
        {
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select TOP 1 * from Tbl_Material WHERE IdMaterial = @IdMaterial";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.Add(new SqlParameter("@IdMaterial", id));
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();

                        return new Material
                        {
                            IdMaterial = dr["IdMaterial"] != DBNull.Value ? Convert.ToInt32(dr["IdMaterial"]) : 0,
                            Nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : string.Empty,
                            Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : string.Empty,
                            IdCategoria = dr["IdCategoria"] != DBNull.Value ? Convert.ToInt32(dr["IdCategoria"]) : 0,
                            IdEstadoMaterial = dr["IdEstadoMaterial"] != DBNull.Value ? Convert.ToInt32(dr["IdEstadoMaterial"]) : 0
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
                
            }
        }

        public Resultado GuardarOActualizar(Material obj)
        {
            var guardado = false;
            var mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd;

                    if (obj.IdMaterial == 0)
                        cmd = new SqlCommand("[sp_InsertarMaterial]", oconexion);
                    else
                    {
                        cmd = new SqlCommand("[sp_ActualizarMaterial]", oconexion);
                        cmd.Parameters.AddWithValue("IdMaterial", obj.IdMaterial);
                    }

                    cmd.Parameters.AddWithValue("IdEstadoMaterial", obj.IdEstadoMaterial);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.IdCategoria);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
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

                    cmd = new SqlCommand("sp_DesactivarMaterial", oconexion);
                    cmd.Parameters.AddWithValue("IdMaterial", id);
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

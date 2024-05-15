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
    public class CD_Inventario
    {
        public List<VInventario> Listar()
        { 
            List<VInventario> lista = new List<VInventario>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) {

                    string query = "select * from VInventario";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        
                        while (dr.Read())
                        {
                            lista.Add(new VInventario
                            {
                                IdInventario = dr["IdInventario"] != DBNull.Value ? Convert.ToInt32(dr["IdInventario"]) : 0,
                                IdMaterial = dr["IdMaterial"] != DBNull.Value ? Convert.ToInt32(dr["IdMaterial"]) : 0,
                                Material = dr["Material"] != DBNull.Value ? dr["Material"].ToString() : string.Empty,
                                Cantidad = dr["Cantidad"] != DBNull.Value ? Convert.ToInt32(dr["Cantidad"]) : 0,
                                PrecioUnitario = dr["PrecioUnitario"] != DBNull.Value ? Convert.ToDecimal(dr["PrecioUnitario"]) : 0,
                                PrecioTotal = dr["PrecioTotal"] != DBNull.Value ? Convert.ToDecimal(dr["PrecioTotal"]) : 0,
                                Moneda = dr["Moneda"] != DBNull.Value ? dr["Moneda"].ToString() : string.Empty,
                                UsuarioIngreso = dr["UsuarioIngreso"] != DBNull.Value ? dr["UsuarioIngreso"].ToString() : string.Empty,
                                Observacion = dr["Observacion"] != DBNull.Value ? dr["Observacion"].ToString() : string.Empty,
                                FechaIngreso = dr["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(dr["FechaIngreso"]) : DateTime.MinValue


                            });
                        }
                        
                    }
                }


            }
            catch (Exception)
            {
                lista = new List<VInventario>();
            }

            return lista;
        }

        public List<VInventarioTracking> ListarTransaccionesDeInventario(int idInventario)
        {
            var lista = new List<VInventarioTracking>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select * from VInventarioTracking WHERE IdInventario = @IdInventario";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@IdInventario", idInventario);

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lista.Add(new VInventarioTracking
                            {

                                IdTracking = dr["IdTracking"] != DBNull.Value ? Convert.ToInt32(dr["IdTracking"]) : 0,
                                Fecha = dr["Fecha"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha"]) : DateTime.MinValue,
                                Cantidad = dr["Cantidad"] != DBNull.Value ? Convert.ToInt32(dr["Cantidad"]) : 0,
                                Observacion = dr["Observacion"] != DBNull.Value ? dr["Observacion"].ToString() : string.Empty,
                                Usuario = dr["Usuario"] != DBNull.Value ? dr["Usuario"].ToString() : string.Empty,
                                DescripcionMaterial = dr["DescripcionMaterial"] != DBNull.Value ? dr["DescripcionMaterial"].ToString() : string.Empty,
                            });
                        }

                    }
                }
            }
            catch (Exception)
            {
                
            }

            return lista;
        }

        public Inventario ObtenerPorId(int id)
        {
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select TOP 1 * from Tbl_Inventario WHERE IdInventario = @IdInventario";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.Add(new SqlParameter("@IdInventario", id));
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();

                        return new Inventario
                        {
                            IdInventario = dr["IdInventario"] != DBNull.Value ? Convert.ToInt32(dr["IdInventario"]) : 0,
                            IdMaterial = dr["IdMaterial"] != DBNull.Value ? Convert.ToInt32(dr["IdMaterial"]) : 0,
                            IdUsuario = dr["IdUsuario"] != DBNull.Value ? Convert.ToInt32(dr["IdUsuario"]) : 0,
                            IdMoneda = dr["IdMoneda"] != DBNull.Value ? Convert.ToInt32(dr["IdMoneda"]) : 0,
                            FechaEntrada = dr["FechaEntrada"] != DBNull.Value ? Convert.ToDateTime(dr["FechaEntrada"]) : DateTime.MinValue,
                            Cantidad = dr["Cantidad"] != DBNull.Value ? Convert.ToInt32(dr["Cantidad"]) : 0,
                            PrecioUnitario = dr["PrecioUnitario"] != DBNull.Value ? Convert.ToInt32(dr["PrecioUnitario"]) : 0,
                            PrecioTotal = dr["PrecioTotal"] != DBNull.Value ? Convert.ToInt32(dr["PrecioTotal"]) : 0,
                            Observacion = dr["Observacion"] != DBNull.Value ? Convert.ToString(dr["Observacion"]) : string.Empty,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public Resultado GuardarOActualizar(Inventario obj)
        {
            var guardado = false;
            var mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd;

                    if (obj.IdInventario == 0)
                        cmd = new SqlCommand("[sp_InsertarInventario]", oconexion);
                    else
                    {
                        cmd = new SqlCommand("[sp_ActualizarInventario]", oconexion);
                        cmd.Parameters.AddWithValue("IdInventario", obj.IdInventario);
                    }

                    cmd.Parameters.AddWithValue("IdMaterial", obj.IdMaterial);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("IdMoneda", obj.IdMoneda);
                    cmd.Parameters.AddWithValue("FechaEntrada", obj.FechaEntrada);
                    cmd.Parameters.AddWithValue("Cantidad", obj.Cantidad);
                    cmd.Parameters.AddWithValue("PrecioUnitario", obj.PrecioUnitario);
                    cmd.Parameters.AddWithValue("PrecioTotal", obj.PrecioTotal);
                    cmd.Parameters.AddWithValue("Observacion", obj.Observacion ?? string.Empty);

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

                    cmd = new SqlCommand("[sp_EliminarInventario]", oconexion);
                    cmd.Parameters.AddWithValue("IdInventario", id);
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

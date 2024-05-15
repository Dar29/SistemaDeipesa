using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Sistema.Entidades; // Asumiendo que Reporte está definido aquí

namespace Sistema.Datos
{
    public class CD_Reporte
    {
        public List<CD_Reporte> ReporteInventarioGeneral(filtros obj)
        {
            List<CD_Reporte> lista = new List<CD_Reporte>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_ReporteInventarioGral", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdMaterial", obj.IdMaterial);
                    cmd.Parameters.AddWithValue("@IdCategoria", obj.IdCategoria);
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("@Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("@FechaIngreso", obj.FechaIngreso);
                    cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    oconexion.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        CD_Reporte reporte = new CD_Reporte();
                        {
                            IdInventario = dr["IdInventario"] != DBNull.Value ? Convert.ToInt32(dr["IdInventario"]) : 0,
	                        IdMaterial = dr["IdMaterial"] != DBNull.Value ? Convert.ToInt32(dr["IdMaterial"]) : 0,
	                        Nombre = dr["Nombre"] != DBNull.Value ? dr["Nombre"].ToString() : string.Empty, 
	                        Stock = dr["Stock"] != DBNull.Value ? Convert.ToInt32(dr["Stock"]) : 0,
                            PrecioUnitario = dr["PrecioUnitario"] != DBNull.Value ? Convert.ToDecimal(dr["PrecioUnitario"]) : 0,
                            PrecioTotal = dr["PrecioTotal"] != DBNull.Value ? Convert.ToDecimal(dr["PrecioTotal"]) : 0,
	                        Moneda = dr["Moneda"] != DBNull.Value ? dr["Moneda"].ToString() : string.Empty,
	                        UsuarioIngreso = dr["UsuarioIngreso"] != DBNull.Value ? dr["UsuarioIngreso"].ToString() : string.Empty,
	                        Observacion = dr["Observacion"] != DBNull.Value ? dr["Observacion"].ToString() : string.Empty,
	                        FechaIngreso = dr["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(dr["FechaIngreso"]) : DateTime.MinValue
                        };

                        lista.Add(reporte);
                    }

                    dr.Close();
                    oconexion.Close();

                    int resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    string mensaje = cmd.Parameters["@Mensaje"].Value.ToString();

                    if (resultado != 1)
                    {
                        throw new Exception("Error en el procedimiento almacenado: " + mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lista;
        }
    }
}

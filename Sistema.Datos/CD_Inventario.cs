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
    }
}

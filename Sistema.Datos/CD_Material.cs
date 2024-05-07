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
                                PrecioUnitario = dr["PrecioUnitario"] != DBNull.Value ? Convert.ToDecimal(dr["PrecioUnitario"]) : 0,
                                Stock = dr["Stock"] != DBNull.Value ? Convert.ToInt32(dr["Stock"]) : 0,
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
    }
}

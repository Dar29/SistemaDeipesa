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
    public class CD_Categoria
    {
        public List<CategoriaMaterial> Listar()
        { 
            List<CategoriaMaterial> lista = new List<CategoriaMaterial>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) {

                    string query = "select * from Tbl_Categoria";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        
                        while (dr.Read())
                        {
                            lista.Add(new CategoriaMaterial
                            {
                                IdCategoria = dr["IdCategoria"] != DBNull.Value ? Convert.ToInt32(dr["IdCategoria"]) : 0,
                                Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : string.Empty,

                            });
                        }
                        
                    }
                }


            }
            catch (Exception)
            {

                lista = new List<CategoriaMaterial>();
            }


            return lista;
        }
    }
}

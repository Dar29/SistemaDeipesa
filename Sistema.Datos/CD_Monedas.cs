using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
    public class CD_Monedas
    {
        public List<Moneda> Listar()
        {
            List<Moneda> lista = new List<Moneda>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "select * from Tbl_Moneda";
                    SqlCommand cmd = new SqlCommand(query, oconexion)
                    {
                        CommandType = CommandType.Text
                    };

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            lista.Add(new Moneda
                            {
                                IdMoneda = dr["IdMoneda"] != DBNull.Value ? Convert.ToInt32(dr["IdMoneda"]) : 0,
                                Descripcion = dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : string.Empty,
                            });
                        }

                    }
                }


            }
            catch (Exception)
            {
                lista = new List<Moneda>();
            }

            return lista;
        }

    }
}

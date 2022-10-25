using System.Data.SqlClient;

namespace ExpiClient.DataAccessLayer
{
    public class InExpi
    {
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection Conex = new SqlConnection("Data Source=localhost;Initial Catalog=EXPI;Integrated Security=SSPI;");
            Conex.Open();

            return Conex;
        }
    }
}

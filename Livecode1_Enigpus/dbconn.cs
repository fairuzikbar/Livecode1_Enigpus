using System.Data.SqlClient;

namespace Livecode1_Enigpus;

public class Dbconn
{
    public static SqlConnection GetConnection()
    {
        const string connectionString = "server=localhost,1433;user=sa;password=Password12345;database=enigpus";
        return new SqlConnection(connectionString);
    }
}
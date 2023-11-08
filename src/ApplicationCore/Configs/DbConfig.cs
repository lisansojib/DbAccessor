using Microsoft.Data.SqlClient;

namespace ApplicationCore.Configs
{
    public class DbConfig
    {
        public string ConnectionString { get; set; }
        public SqlConnection Connection => new(ConnectionString);
    }
}

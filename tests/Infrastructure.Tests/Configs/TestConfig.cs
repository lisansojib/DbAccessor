using ApplicationCore.Configs;

namespace Infrastructure.Tests.Configs
{
    public class TestConfig
    {
        public string DbConnection => "Data Source=SOJIB\\SQLEXPRESS;Initial Catalog=LeadScripts;Persist Security Info=True;User ID=sa;Password=Aa12345^;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";
        public DbConfig DbConfig => new() { ConnectionString = DbConnection };
    }
}

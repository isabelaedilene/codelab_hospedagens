using MySql.Data.MySqlClient;
using System.Data;

namespace CodelabHospedagens.Infra.DataBaseSettings
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession()
        {
            Connection = new MySqlConnection(Settings.ConnectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}


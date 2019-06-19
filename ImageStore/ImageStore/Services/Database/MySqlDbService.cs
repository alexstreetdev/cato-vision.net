using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ImageStore.Services.Database
{
    public class MySqlDbService
    {

        private readonly MySqlConnection _conn;

        public MySqlDbService(DbSettings settings)
        {
            string connString = settings.ConnString;
            _conn = new MySqlConnection(connString);
        }


        public ConnectionState DbConnectionState => _conn.State;

        public void Open()
        {
            _conn.Open();
        }

        public void Close()
        {
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        
        public MySqlDataReader ExecCommand(MySqlCommand cmd)
        {
            cmd.Connection = _conn;
            return cmd.ExecuteReader();
        }

        public async Task<DbDataReader> ExecCommandAsync(MySqlCommand cmd)
        {
            cmd.Connection = _conn;
            return await cmd.ExecuteReaderAsync();
        }

        public int ExecNonQuery(MySqlCommand cmd)
        {
            cmd.Connection = _conn;
            return cmd.ExecuteNonQuery();
        }

        public async Task<int> ExecuteNonQueryAsync(MySqlCommand cmd)
        {
            cmd.Connection = _conn;
            return await cmd.ExecuteNonQueryAsync();
        }

    }
}
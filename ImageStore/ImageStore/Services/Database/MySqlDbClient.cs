using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using ImageStore.Models;
using MySql.Data.MySqlClient;

namespace ImageStore.Services.Database
{
    public class MySqlDbClient : IDbClient, IDisposable
    {
        private readonly MySqlDbService _db;
        private readonly DbSettings _dbSettings;

        public MySqlDbClient(DbSettings settings)
        {
            _dbSettings = settings;
            _db = new MySqlDbService(_dbSettings);
        }

        public void InitialiseDatabase()
        {
            OpenDbConnection();
            var i = new MySqlDbInitialise(_db);
            i.Initialise();
            _db.Close();
        }

        public string AddImage(Image img)
        {
            var cmd = new MySqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = MySqlDbHelper.AddImageQuery(img)
            };
            OpenDbConnection();
            _db.ExecCommand(cmd);
            //_db.Close();
            return img.ImageId;
        }

        public async Task<string> AddImageAsync(Image img)
        {
            var cmd = MySqlDbHelper.AddImageCommand(img);

            OpenDbConnection();
            var rdr = await _db.ExecCommandAsync(cmd);
            rdr.Close();
            //_db.Close();
            return img.ImageId;
        }

        public async Task<ImageContent> AddImageContentAsync(ImageContent ic)
        {
            var cmd = MySqlDbHelper.AddImageContentCommand(ic);
            OpenDbConnection();
            DbDataReader r = await _db.ExecCommandAsync(cmd);
            ic.ContentId = (long)r[0];
            r.Close();
            //_db.Close();
            return ic;
        }

        public async Task<int> DeleteImageAsync(string imageid)
        {
            var cmd = MySqlDbHelper.DeleteImageCommand(imageid);
            OpenDbConnection();
            var x = _db.ExecNonQuery(cmd);
            return x;
        }

        private void OpenDbConnection()
        {
            if (_db.DbConnectionState != ConnectionState.Open)
            {
                _db.Open();
            }
        }

        private void CloseDbConnection()
        {
            if (_db.DbConnectionState == ConnectionState.Open)
            {
                _db.Close();
            }
        }

        public void Dispose()
        {
            CloseDbConnection();
        }
    }
}

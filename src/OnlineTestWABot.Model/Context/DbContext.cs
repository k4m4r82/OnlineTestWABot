using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;

namespace OnlineTestWABot.Model.Context
{
    public interface IDbContext : IDisposable
    {
        IDbConnection Db { get; }
    }

    public class DbContext : IDbContext
    {
        private IDbConnection _db;

        private readonly string _providerName;
        private readonly string _connectionString;

        public DbContext()
        {
            var dbName = Directory.GetCurrentDirectory() + @"database\DbTesOnline.db";

            _providerName = "System.Data.SQLite";
            _connectionString = "Data Source=" + dbName;
        }

        private IDbConnection GetOpenConnection(string providerName, string connectionString)
        {
            DbConnection conn = null;

            try
            {
                var provider = DbProviderFactories.GetFactory(providerName);
                conn = provider.CreateConnection();
                conn.ConnectionString = connectionString;
                conn.Open();
            }
            catch { }

            return conn;
        }

        public IDbConnection Db
        {
            get { return _db ?? (_db = GetOpenConnection(_providerName, _connectionString)); }
        }

        public void Dispose()
        {
            if (_db != null)
            {
                try
                {
                    if (_db.State != ConnectionState.Closed)
                        _db.Close();
                }
                finally
                {
                    _db.Dispose();
                }
            }

            GC.SuppressFinalize(this);
        }
    }
}

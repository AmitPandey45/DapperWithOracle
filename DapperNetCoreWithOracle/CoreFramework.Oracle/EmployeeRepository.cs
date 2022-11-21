using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CoreFramework.Oracle
{
    public class EmployeeRepository : IEmployeeRepository
    {
        IConfiguration configuration;
        public EmployeeRepository()
        {
        }

        public object GetGames()
        {
            object result = null;
            var conn = this.GetConnection();
            try
            {
                var dyParam = new OracleDynamicParameters();

                dyParam.Add("games", OracleDbType.RefCursor, ParameterDirection.Output);
                var query = "TEST_GETALLGAMES1";
                result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);

                ////if (conn.State == ConnectionState.Closed)
                ////{
                ////    conn.Open();
                ////}

                ////if (conn.State == ConnectionState.Open)
                ////{
                ////    var query = "TEST_GETALLGAMES1";

                ////    result = SqlMapper.Query(conn, query, param: dyParam, commandType: CommandType.StoredProcedure);
                ////}
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ////conn.Close();
            }

            return result;
        }

        public IDbConnection GetConnection()
        {
            var connn = "User ID={0};Password={1};Data Source={2};Pooling=false;";
            var connStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));User Id={3};Password={4};";
            ////var connectionString = configuration.GetSection("ConnectionStrings").GetSection("EmployeeConnection").Value;
            var conn = new OracleConnection(connStr);
            return conn;
        }

        public IDbConnection CreateConnection()
        {
            string password = "abc";
            string connectionString = "User ID={0};Password={1};Data Source={2};Pooling=false;";
            ////string providerName = "Oracle.ManagedDataAccess.Client";
            string providerName = "System.Data.OracleClient";
            connectionString = string.Format(connectionString, password);
            var factory = DbProviderFactories.GetFactory(providerName);

            var conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;

            return conn;
        }

        public IDbConnection CreateConnection1()
        {
            var connStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));User Id={3};Password={4};";

            // Register the factory
            ////DbProviderFactories.RegisterFactory("Oracle.ManagedDataAccess.Client", OracleClientFactory.Instance);
            DbProviderFactories.RegisterFactory("System.Data.OracleClient", OracleClientFactory.Instance);

            // Get the provider invariant names
            IEnumerable<string> invariants = DbProviderFactories.GetProviderInvariantNames(); // => 1 result; 'test'

            // Get a factory using that name
            DbProviderFactory factory = DbProviderFactories.GetFactory(invariants.FirstOrDefault());


            string password = "abc";
            string connectionString = "User ID={0};Password={1};Data Source={2};Pooling=false;";
            ////string providerName = "Oracle.ManagedDataAccess.Client";
            string providerName = "System.Data.OracleClient";
            connectionString = string.Format(connectionString, password);
            var factory1 = DbProviderFactories.GetFactory(providerName);

            var conn = factory.CreateConnection();
            conn.ConnectionString = connStr;

            return conn;
        }
    }
}

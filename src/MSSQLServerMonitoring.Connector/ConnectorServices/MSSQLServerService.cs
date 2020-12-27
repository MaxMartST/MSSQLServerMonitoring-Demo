using MSSQLServerMonitoring.Connector.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Connector.Services
{
    public class MSSQLServerService : IMSSQLServerService
    {
        private readonly string _baseUrl;
        public MSSQLServerService(ConfigureMSSQLServerConnectorComponent configuration)
        {
            _baseUrl = configuration.BaseApiUrl;
        }

        public List<EventMSSQLServer> GetNewQueryHistory()
        {
            List<DbParameter> parameterList = new List<DbParameter>();
            List<EventMSSQLServer> EventMSSQLServers = new List<EventMSSQLServer>();
            EventMSSQLServer EventMSSQLServerItem = null;

            using (DbDataReader dataReader = GetDataReader("ReaderStatemetMSSQLServer", parameterList, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        EventMSSQLServerItem = new EventMSSQLServer();

                        EventMSSQLServerItem.TimeStamp = (DateTime)dataReader["TimeStamp"];
                        EventMSSQLServerItem.EventName = (string)dataReader["EventName"];
                        EventMSSQLServerItem.PackageName = (string)dataReader["PackageName"];
                        EventMSSQLServerItem.ClientHn = (string)dataReader["ClientHn"];
                        EventMSSQLServerItem.ClientAppName = (string)dataReader["ClientAppName"];
                        EventMSSQLServerItem.NtUserName = (string)dataReader["NtUserName"];
                        EventMSSQLServerItem.DatabaseId = (int)dataReader["DatabaseId"];
                        EventMSSQLServerItem.DatabaseName = (string)dataReader["DatabaseName"];
                        EventMSSQLServerItem.Statement = (string)dataReader["Statement"];
                        EventMSSQLServerItem.Duration = (decimal)dataReader["Duration"];
                        EventMSSQLServerItem.CpuTime = (long)dataReader["CpuTime"];
                        EventMSSQLServerItem.PhysicalReads = (long)dataReader["PhysicalReads"];
                        EventMSSQLServerItem.LogicalReads = (long)dataReader["LogicalReads"];
                        EventMSSQLServerItem.Writes = (long)dataReader["Writes"];
                        EventMSSQLServerItem.Writes = (long)dataReader["RowCount"];
                        EventMSSQLServerItem.SqlText = (string)dataReader["SqlText"];

                        EventMSSQLServers.Add(EventMSSQLServerItem);
                    }
                }
            }
            return EventMSSQLServers;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(_baseUrl);
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        protected DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(commandText, connection as SqlConnection);
            command.CommandType = commandType;
            return command;
        }
        protected SqlParameter GetParameter(string parameter, object value)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, value != null ? value : DBNull.Value);
            parameterObject.Direction = ParameterDirection.Input;
            return parameterObject;
        }
        protected SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
        {
            SqlParameter parameterObject = new SqlParameter(parameter, type);

            if (type == SqlDbType.NVarChar || type == SqlDbType.VarChar || type == SqlDbType.NText || type == SqlDbType.Text)
            {
                parameterObject.Size = -1;
            }

            parameterObject.Direction = parameterDirection;

            if (value != null)
            {
                parameterObject.Value = value;
            }
            else
            {
                parameterObject.Value = DBNull.Value;
            }

            return parameterObject;
        }
        protected int ExecuteNonQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            int returnValue = -1;

            try
            {
                using (SqlConnection connection = this.GetConnection())
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, commandType);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //LogException("Failed to ExecuteNonQuery for " + procedureName, ex, parameters);
                throw;
            }

            return returnValue;
        }
        protected object ExecuteScalar(string procedureName, List<SqlParameter> parameters)
        {
            object returnValue = null;

            try
            {
                using (DbConnection connection = this.GetConnection())
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, CommandType.StoredProcedure);

                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    returnValue = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                //LogException("Failed to ExecuteScalar for " + procedureName, ex, parameters);
                throw;
            }

            return returnValue;
        }
        protected DbDataReader GetDataReader(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            DbDataReader ds;

            try
            {
                DbConnection connection = this.GetConnection();
                {
                    DbCommand cmd = this.GetCommand(connection, procedureName, commandType);
                    if (parameters != null && parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    ds = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception ex)
            {
                //LogException("Failed to GetDataReader for " + procedureName, ex, parameters);
                throw;
            }

            return ds;
        }
    }
}

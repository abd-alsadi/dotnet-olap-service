using KmnlkOLAPModel.Constants;
using KmnlkOLAPModel.Interfaces;
using KmnlkCommon.Shareds;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KmnlkCommon.Shareds.LoggerManagement;
using System.IO;
using Microsoft.AnalysisServices.AdomdClient;
using KmnlkOLAPModel.Models;

namespace KmnlkOLAPModel.Connections
{
    public class OracleConnectionManager :ConnectionManager
    {

        public OracleConnectionManager(string conn, ILog log)
        {
            typeDB = Enums.DB_TYPE.ORACLE;
            connectionString = conn;
            this.log = log;
        }
        public OracleConnectionManager(string ds, string db, ILog log,string user = "sa", string pass = "sa", bool auth = false, int time = 100000)
        {
            this.typeDB = Enums.DB_TYPE.ORACLE;
            this.userID = user;
            this.password = pass;
            this.dataSource = ds;
            this.database = db;
            this.timeOut = time;
            this.auth = auth;
            this.log = log;
            if(auth)
            connectionString = @"Provider=MSOLAP; Data Source=" + ds + "; Initial Catalog =" + db + ";" ;
            else
            connectionString = @"Provider=MSOLAP; Data Source=" + ds + "; Initial Catalog =" + db + ";";
        }

        public int Insert(string query, List<OracleParameter> parameters)
        {
  
            using (var conn = new AdomdConnection(connectionString))
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), query, ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    conn.Open();
                    var command = new AdomdCommand(query, conn);
                    command.CommandTimeout = this.timeOut;
                    int result = command.ExecuteNonQuery();
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return -2;
                }
                catch (Exception e)
                {
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.EXCEPTION, ENUM_TYPE_Block_LOGGER.END, e.Message);
                    conn.Close();
                }
                return -2;
            }
        }

        public int Update(string query, List<OracleParameter> parameters)
        {

            using (var conn = new AdomdConnection(connectionString))
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), query, ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    conn.Open();
                    var command = new AdomdCommand(query, conn);
                    command.CommandTimeout = this.timeOut;
                    int result = command.ExecuteNonQuery();
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return -2;
                }
                catch (Exception e)
                {
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.EXCEPTION, ENUM_TYPE_Block_LOGGER.END, e.Message);
                    conn.Close();
                }
                return -2;
            }
        }
        public int Delete(string query, List<OracleParameter> parameters)
        {

            using (var conn = new AdomdConnection(connectionString))
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), query, ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    conn.Open();
                    var command = new AdomdCommand(query, conn);
                    command.CommandTimeout = this.timeOut;
                    int result = command.ExecuteNonQuery();
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return -2;
                }
                catch (Exception e)
                {
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.EXCEPTION, ENUM_TYPE_Block_LOGGER.END, e.Message);
                    conn.Close();
                }
                return -2;
            }
        }

        public T Get<T>(string query, List<OracleParameter> parameters, IReaderOracleOperations IRSO,clsQuery req)
        {
            using (var conn = new AdomdConnection(connectionString))
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), query, ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    conn.Open();
                    var command = new AdomdCommand(query, conn);
                    command.CommandTimeout = this.timeOut;
                    AdomdDataReader reader = command.ExecuteReader();
                    T model = IRSO.ReadORACLE<T>(reader, req);
                    conn.Close();
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return model;
                }
                catch (Exception e)
                {
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.EXCEPTION, ENUM_TYPE_Block_LOGGER.END, e.Message);
                    conn.Close();
                    return default(T);
                }
            }
        }
        public T All<T>(string query, List<OracleParameter> parameters, IReaderOracleOperations IRSO,clsQuery req)
        {
            using (var conn = new AdomdConnection(connectionString))
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), query, ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    conn.Open();
                    var command = new AdomdCommand(query, conn);
                    command.CommandTimeout = this.timeOut;
                    AdomdDataReader reader = command.ExecuteReader();
                    T models = IRSO.ReadAllORACLE<T>(reader,req);
                    conn.Close();
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);

                    return models;
                }
                catch (Exception e)
                {
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.EXCEPTION, ENUM_TYPE_Block_LOGGER.END, e.Message);
                    conn.Close();
                    return default(T);
                }
            }
        }
        private List<OracleParameter> FixParameters(List<OracleParameter> parameters)
        {
            foreach (var param in parameters)
            {
                if (param.Value == null)
                    param.Value = DBNull.Value;
            }
            return parameters;
        }
        public override void execute(string query)
        {
            using (var conn = new AdomdConnection(connectionString))
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), query, ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    conn.Open();
                    var command = new AdomdCommand(query, conn);
                    command.CommandTimeout = this.timeOut;
                    int result = command.ExecuteNonQuery();
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);

                }
                catch (Exception e)
                {
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.EXCEPTION, ENUM_TYPE_Block_LOGGER.END, e.Message);
                    conn.Close();
                }
            }
        }
        public override void updateDB(string path)
        {
            log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), path, ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                StringBuilder st = new StringBuilder();
                st.Append(File.ReadAllText(path));
                execute(st.ToString());
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);

            }
            catch (Exception e)
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.EXCEPTION, ENUM_TYPE_Block_LOGGER.END, e.Message);
            }
        }
        public override void createDB(string path)
        {
            log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), path, ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                StringBuilder st = new StringBuilder();
                st.Append(File.ReadAllText(path));
                execute(st.ToString());
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);

            }
            catch (Exception e)
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.EXCEPTION, ENUM_TYPE_Block_LOGGER.END, e.Message);
            }
        }
    }
}

using KmnlkOLAPModel.Constants;
using KmnlkOLAPModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KmnlkOLAPModel.Connections;
using KmnlkOLAPModel.Models;
using KmnlkCommon.Shareds;
using System.Data.OracleClient;
using static KmnlkCommon.Shareds.LoggerManagement;
using KmnlkOLAPModel.Exceptions;
using Microsoft.AnalysisServices.AdomdClient;
using KmnlkDWHEngine.Helpers;

namespace KmnlkOLAPModel.Managers
{
    public class OlapManager : ModelManager, ICURDOperations, IReaderSqlOperations, IReaderOracleOperations, IValidationOperations
    {
        public ConnectionManager connectionManager;
        public ILog log;

        public OlapManager(ConnectionManager CM, ILog logger)
        {
            connectionManager = CM;
            log = logger;
        }

        public T All<T>(clsQuery model)
        {
            if (!isValid(model))
            {
                return default(T);
            }
            string query = QueryHeper.BuidMDXQueryString(model);
            clsQuery instance = (clsQuery)model;
            if (connectionManager.GetTypeDB() == Enums.DB_TYPE.SQL)
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    SqlConnectionManager manager = (SqlConnectionManager)connectionManager;
                    List<SqlParameter> ts = new List<SqlParameter>();
                    T result = manager.All<T>(query, ts, this,model);
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return default(T);
                }
            }
            else
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    OracleConnectionManager manager = (OracleConnectionManager)connectionManager;
                    List<OracleParameter> ts = new List<OracleParameter>();
                    T result = manager.All<T>(query, ts, this,model);
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return default(T);
                }
            }
        }

        public T Get<T>(clsQuery model)
        {
            if (!isValid(model))
            {
                return default(T);
            }
            T result=default(T);
            string query = QueryHeper.BuidMDXQueryString(model);
            clsQuery instance = (clsQuery)model;
            if (connectionManager.GetTypeDB() == Enums.DB_TYPE.SQL)
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    SqlConnectionManager manager = (SqlConnectionManager)connectionManager;
                    List<SqlParameter> ts = new List<SqlParameter>();
                    result = manager.Get<T>(query, ts, this, model);

                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return default(T);
                }
            }
            else
            {

                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    OracleConnectionManager manager = (OracleConnectionManager)connectionManager;
                    List<OracleParameter> ts = new List<OracleParameter>();

                    result = manager.Get<T>(query, ts, this,model);
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return default(T);
                }
            }
        }

        public int Delete(clsQuery model)
        {
            string query = "";
            if (!isValid(model))
            {
                return -1;
            }
            clsQuery instance = (clsQuery)model;
            if (connectionManager.GetTypeDB() == Enums.DB_TYPE.SQL)
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    SqlConnectionManager manager = (SqlConnectionManager)connectionManager;
                    List<SqlParameter> ts = new List<SqlParameter>();

                    int result = manager.Delete(query, ts);
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return -1;
                }
            }
            else
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    OracleConnectionManager manager = (OracleConnectionManager)connectionManager;
                    List<OracleParameter> ts = new List<OracleParameter>();

                    int result = manager.Delete(query, ts);
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return -1;
                }
            }
        }

        public int Insert(clsQuery model)
        {
            string query = "";
            if (!isValid(model))
            {
                return -1;
            }
            clsQuery instance = (clsQuery)model;
            if (connectionManager.GetTypeDB() == Enums.DB_TYPE.SQL)
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    SqlConnectionManager manager = (SqlConnectionManager)connectionManager;
                    List<SqlParameter> ts = new List<SqlParameter>();
                    int result = manager.Insert(query, ts);
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "{result=" + result.ToString() + "}", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return -1;
                }
            }
            else
            {

                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    OracleConnectionManager manager = (OracleConnectionManager)connectionManager;
                    List<OracleParameter> ts = new List<OracleParameter>();
                   
                    int result = manager.Insert(query, ts);
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "{result=" + result.ToString() + "}", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return -1;
                }
            }
        }

        public int Update(clsQuery model)
        {
            string query = "";
            if (!isValid(model))
            {
                return -1;
            }
            clsQuery instance = (clsQuery)model;
            if (connectionManager.GetTypeDB() == Enums.DB_TYPE.SQL)
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    SqlConnectionManager manager = (SqlConnectionManager)connectionManager;
                    List<SqlParameter> ts = new List<SqlParameter>();
                   
                    int result = manager.Update(query, ts);
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "{result=" + result.ToString() + "}", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return -1;
                }
            }
            else
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    OracleConnectionManager manager = (OracleConnectionManager)connectionManager;
                    List<OracleParameter> ts = new List<OracleParameter>();

                    int result = manager.Update(query, ts);
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "{result=" + result.ToString() + "}", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return -1;
                }
            }
        }

        public T Find<T>(int size, int page, clsQuery model)
        {
            string query = "";
            if (!isValid(model))
            {
                return default(T);
            }
            clsQuery instance = (clsQuery)model;
            if (connectionManager.GetTypeDB() == Enums.DB_TYPE.SQL)
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    SqlConnectionManager manager = (SqlConnectionManager)connectionManager;
                    List<SqlParameter> ts = new List<SqlParameter>();

                    T result = manager.All<T>(query, ts, this, model);
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return default(T);
                }
            }
            else
            {
                log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
                try
                {
                    OracleConnectionManager manager = (OracleConnectionManager)connectionManager;
                    List<OracleParameter> ts = new List<OracleParameter>();

                    T result = manager.All<T>(query, ts, this, model);
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
                catch (Exception e)
                {
                    new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    return default(T);
                }
            }
        }

        public T ReadAllSQL<T>(AdomdDataReader reader, clsQuery request)
        {
            T result=default(T);

            log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (reader != null && request!=null)
                {
                    if (typeof(T) == typeof(List<kmnlkOLAPModel>))
                    {
                        result = (T)Convert.ChangeType(FillDataHelper.FillDataObject(reader, request), typeof(T));
                    }
                    else
                    {
                        result = (T)Convert.ChangeType(FillDataHelper.FillDataString(reader, request), typeof(T));
                    }
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
            }
            catch (Exception e)
            {
                new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
            }
            return result;
        }

        public T ReadSQL<T>(AdomdDataReader reader, clsQuery request)
        {
            T result = default(T);

            log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (reader != null && request != null)
                {
                    if (typeof(T) == typeof(List<kmnlkOLAPModel>))
                    {
                        result = (T)Convert.ChangeType(FillDataHelper.FillDataObject(reader, request), typeof(T));
                    }
                    else
                    {
                        result=(T)Convert.ChangeType(FillDataHelper.FillDataString(reader, request), typeof(T));
                    }
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
            }
            catch (Exception e)
            {
                new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
            }
            return result;
        }

        public T ReadAllORACLE<T>(AdomdDataReader reader, clsQuery request)
        {
            T result = default(T);

            log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (reader != null && request != null)
                {
                    if (typeof(T) == typeof(List<kmnlkOLAPModel>))
                    {
                        result = (T)Convert.ChangeType(FillDataHelper.FillDataObject(reader, request), typeof(T));
                    }
                    else
                    {
                        result = (T)Convert.ChangeType(FillDataHelper.FillDataString(reader, request), typeof(T));
                    }
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
            }
            catch (Exception e)
            {
                new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
            }
            return result;
        }

        public T ReadORACLE<T>(AdomdDataReader reader, clsQuery request)
        {
            T result = default(T);

            log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (reader != null && request != null)
                {
                    if (typeof(T) == typeof(List<kmnlkOLAPModel>))
                    {
                        result = (T)Convert.ChangeType(FillDataHelper.FillDataObject(reader, request), typeof(T));
                    }
                    else
                    {
                        result = (T)Convert.ChangeType(FillDataHelper.FillDataString(reader, request), typeof(T));
                    }
                    log.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                    return result;
                }
            }
            catch (Exception e)
            {
                new EngineException(log, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
            }
            return result;
        }
        public bool isValid(clsQuery model)
        {
            //throw new NotImplementedException();
            if (model.source == "")
                return false;

            if (model.measures == null)
                return false;

            if (model.measures.Count == 0)
                return false;

            if (model.diminsions != null && model.diminsions.Count > 0)
            {
                bool ok = true;
                foreach (clsDiminsion dim in model.diminsions)
                {
                    if (dim.name == null || dim.name == "" || dim.keys == null || dim.keys.Count == 0)
                        return false;
                    foreach (clsKey key in dim.keys)
                    {
                        if (key.name == null || key.name == "")
                            return false;

                        if (key.isFilter && key.filter == null)
                        {
                            return false;
                        }
                        else if (key.isFilter && key.filter != null)
                        {
                            if (key.filter.value == "" || key.filter.value == null || key.filter.operation == "" || key.filter.operation == null)
                            {
                                return false;
                            }
                        }

                    }
                }
            }

            return true;
        }


    }
}

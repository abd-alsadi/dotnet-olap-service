
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static KmnlkCommon.Shareds.LoggerManagement;
using KmnlkCommon.Shareds;
using KmnlkOLAPDll.Interfaces;
using KmnlkOLAPModel.Connections;
using KmnlkOLAPModel;
using KmnlkOLAPDll.Exceptions;
using static KmnlkOLAPDll.Constants.Enums;
using KmnlkDWHDll.Management;
using KmnlkOLAPModel.Managers;
using KmnlkOLAPDll.Constants;
using KmnlkOLAPModel.Models;

namespace KmnlkOLAPDll.Managment
{
    public class OlapManagement : ModelManagement, ICURDOperations, IValidationOperations
    {
        private OlapManager manager;
        private ILog logger;
        public OlapManagement(ConnectionManager conn, ILog logger)
        {
            this.logger = logger;
            manager = new OlapManager(conn, logger);
        }
        public object All(clsQuery model,int type,ref string msgError)
        {
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (!isValid(model))
                {
                    msgError = modConstant.MSG_NOT_VALID_MODEL;
                    return null;
                }
                switch ((Enum_ResultType)type)
                {
                    case Enum_ResultType.STRING:
                        var result1 = JsonManagement.toObject<object>(manager.All<string>(model));
                        logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                        return result1;
                    case Enum_ResultType.OBJECT:
                        var result2= manager.All<List<kmnlkOLAPModel>>(model);
                        logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                        return result2;
                    default:
                        var result3 = JsonManagement.toObject<object>(manager.All<string>(model));
                        logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                        return result3;
                }

            }
            catch (Exception e)
            {
                new DllException(logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                return null;
            }
        }

        public object Find(int size,int page,clsQuery model, int type, ref string msgError)
        {
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (!isValid(model))
                {
                    msgError = modConstant.MSG_NOT_VALID_MODEL;
                    return null;
                }
                switch ((Enum_ResultType)type)
                {
                    case Enum_ResultType.STRING:
                        var result1 = JsonManagement.toObject<object>(manager.Find<string>(size,page,model));
                        logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                        return result1;
                    case Enum_ResultType.OBJECT:
                        var result2 = manager.Find<List<kmnlkOLAPModel>>(size, page, model);
                        logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                        return result2;
                    default:
                        var result3 = JsonManagement.toObject<object>(manager.Find<string>(size, page, model));
                        logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                        return result3;
                }

            }
            catch (Exception e)
            {
                new DllException(logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                return null;
            }
        }

        public Enum_CURD_Result Delete(clsQuery model, ref string msgError)
        {
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);

            try
            {
                if (!isValid(model))
                {
                    msgError = modConstant.MSG_NOT_VALID_MODEL;
                    return Enum_CURD_Result.ERROR_PARAMETERS;
                }
                int result = manager.Delete(model);
                logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                if (result == -1)
                    return Enum_CURD_Result.NOT_SUCCESS;
                else
                    return Enum_CURD_Result.SUCCESS;
            }
            catch (Exception e)
            {
                new DllException(logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                return Enum_CURD_Result.NOT_SUCCESS;
            }
        }
         
        public object Get(clsQuery model, int type, ref string msgError)
        {
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);
            try
            {
                if (!isValid(model))
                {
                    msgError = modConstant.MSG_NOT_VALID_MODEL;
                    return null;
                }
                switch ((Enum_ResultType)type)
                {
                    case Enum_ResultType.STRING:
                        var result1 = JsonManagement.toObject<object>(manager.Get<string>(model));
                        logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                        return result1;
                    case Enum_ResultType.OBJECT:
                        var result2 = manager.Get<List<kmnlkOLAPModel>>(model);
                        logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                        return result2;
                    default:
                        var result3 = JsonManagement.toObject<object>(manager.Get<string>(model));
                        logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                        return result3;
                }

            }
            catch (Exception e)
            {
                new DllException(logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                return null;
            }
        }

        public Enum_CURD_Result Insert(clsQuery model, ref string msgError)
        {
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);

            try
            {
                if (!isValid(model))
                {
                    msgError = modConstant.MSG_NOT_VALID_MODEL;
                    return Enum_CURD_Result.ERROR_PARAMETERS;
                }
                int result = manager.Insert(model);
                logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                if (result == -1)
                    return Enum_CURD_Result.NOT_SUCCESS;
                else
                    return Enum_CURD_Result.SUCCESS;
            }
            catch (Exception e)
            {
                new DllException(logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                return Enum_CURD_Result.NOT_SUCCESS;
            }
        }


        
        public bool isValid(clsQuery model)
        {
            //throw new NotImplementedException();
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);

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
                    if (dim.name==null || dim.name == "" || dim.keys == null || dim.keys.Count == 0)
                        return false;
                    foreach(clsKey key in dim.keys)
                    {
                        if (key.name == null || key.name == "")
                            return false;

                        if (key.isFilter && key.filter==null)
                        {
                            return false;
                        }else if (key.isFilter && key.filter != null)
                        {
                            if(key.filter.value=="" || key.filter.value == null || key.filter.operation == "" || key.filter.operation == null)
                            {
                                return false;
                            }
                        }

                    }
                }
            }
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);

            return true;
        }

        public Enum_CURD_Result Update(clsQuery model, ref string msgError)
        {
            logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstant.MSG_SUCCESS);

            try
            {
                if (!isValid(model))
                {
                    msgError = modConstant.MSG_NOT_VALID_MODEL;
                    return Enum_CURD_Result.ERROR_PARAMETERS;
                }
                int result = manager.Update(model);
                logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstant.MSG_SUCCESS);
                if (result == -1)
                    return Enum_CURD_Result.NOT_SUCCESS;
                else
                    return Enum_CURD_Result.SUCCESS;
            }
            catch (Exception e)
            {
                new DllException(logger, "", EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                return Enum_CURD_Result.NOT_SUCCESS;
            }
        }

      
    }
}

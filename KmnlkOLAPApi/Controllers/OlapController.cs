using KmnlkOLAPApi.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;
using KmnlkCommon.Shareds;
using static KmnlkCommon.Shareds.LoggerManagement;
using KmnlkUMSApi.Constants;
using KmnlkOLAPApi.Exceptions;
using System.Net;
using KmnlkOLAPApi.Models;
using KmnlkOLAPModel.Models;
using static KmnlkOLAPDll.Constants.Enums;

namespace KmnlkOLAPApi.Controllers
{
        public class OlapController : ApiController
        {
        private PackageManagement package = null;

        public OlapController(PackageManagement repo)
        {
            package = repo;
        }
            [HttpPost]
            [ActionName("GetOlap")]
            public HttpResponseMessage GetOlap([FromBody]clsQuery req,[FromUri]int type=0)
            {
                package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstants.MSG_SUCCESS);
                string startTime = DateTime.Now.ToString("hh:mm:ss");
                string endTime = "";
            string msgError = "";
            var result = new object();
                HttpResponseMessage res;
                try
                {
                        if (isValid(req, ref msgError))
                        {
                            result = package.context.instanceOlapBussinesManagement().Get(req, type, ref msgError);
                        }
                        if (msgError != "")
                        {
                            result = msgError;
                        }
                        endTime = DateTime.Now.ToString("hh:mm:ss");
                        package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstants.MSG_SUCCESS);
                    var response = new ResponseModel(result, modConstants.MSG_SUCCESS, HttpStatusCode.OK, startTime, endTime);
                
                    res = Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
                    return res;
                }
                catch (Exception e)
                {
                    new ApiException(package.logger, modConstants.MSG_SUCCESS, EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                    var response = new ResponseModel(null, e.Message, HttpStatusCode.BadRequest, startTime, endTime);
                    return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
                }

            }

        [HttpPost]
        [ActionName("AllOlap")]
        public HttpResponseMessage AllOlap([FromBody]clsQuery req, [FromUri]int type = 0)
        {
            package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.START, modConstants.MSG_SUCCESS);
            string startTime = DateTime.Now.ToString("hh:mm:ss");
            string endTime = "";
            string msgError = "";
            var result = new object();
            HttpResponseMessage res;
            try
            {
                if (isValid(req,ref msgError))
                {
                    result = package.context.instanceOlapBussinesManagement().All(req, type, ref msgError);
                }
               if (msgError != "")
                {
                    result = msgError;
                }
                endTime = DateTime.Now.ToString("hh:mm:ss");
                package.logger.WriteToLog(EnvironmentManagement.getCurrentMethodName(this.GetType()), "", ENUM_TYPE_MSG_LOGGER.INFO, ENUM_TYPE_Block_LOGGER.END, modConstants.MSG_SUCCESS);
                var response = new ResponseModel(result, modConstants.MSG_SUCCESS, HttpStatusCode.OK, startTime, endTime);
                res = Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
                return res;
            }
            catch (Exception e)
            {
                new ApiException(package.logger, modConstants.MSG_SUCCESS, EnvironmentManagement.getCurrentMethodName(this.GetType()), e.Message);
                var response = new ResponseModel(null, e.Message, HttpStatusCode.BadRequest, startTime, endTime);
                return Request.CreateResponse<ResponseModel>(HttpStatusCode.OK, response);
            }

        }


        [NonAction]
        public bool isValid(clsQuery query,ref string msgError)
        {
            object sourceValue = "";
            if (SettingsManagement.settings_structure != null)
            {
                if (query.source == "" || query.source == null)
                {
                    msgError =String.Format(modConstants.MSG_NOT_VALID_MODEL_STRUCTURE,"source",query.source);
                    return false;
                }
                object qry = new clsQuery();
                SettingsManagement.settings_structure.TryGetValue(query.source, out qry);
                if (qry == null)
                {
                    msgError = String.Format(modConstants.MSG_NOT_VALID_MODEL_STRUCTURE, "source", query.source);
                    return false;
                }


                if (query.measures==null  || query.measures.Count==0)
                {
                    msgError = String.Format(modConstants.MSG_NOT_VALID_MODEL_STRUCTURE, "measures", "");
                    return false;
                }

                foreach(var measure in query.measures)
                {
                    bool ok = false;
                    clsQuery QUERY = (clsQuery)qry;
                    if(QUERY.measures==null || QUERY.measures.Count == 0)
                    {
                        msgError = String.Format(modConstants.MSG_NOT_VALID_MODEL_STRUCTURE, "measures", "");
                        return false;
                    }

                    foreach(var me in QUERY.measures)
                    {
                        if (me.name == measure.name)
                        {
                            ok = true;
                            break;
                        }
                    }

                    if (ok == false)
                    {
                        msgError = String.Format(modConstants.MSG_NOT_VALID_MODEL_STRUCTURE, "measure",measure.name);
                        return false;
                    }
                }

                if (query.diminsions != null)
                {
                    foreach (var diminsion in query.diminsions)
                    {
                        bool ok = false;
                        clsQuery QUERY = (clsQuery)qry;
                        if (QUERY.diminsions == null || QUERY.diminsions.Count == 0)
                        {
                            msgError = String.Format(modConstants.MSG_NOT_VALID_MODEL_STRUCTURE, "diminsions","");
                            return false;
                        }

                        foreach (var de in QUERY.diminsions)
                        {
                            if (de.name == diminsion.name)
                            {
                                ok = true;
                                if (diminsion.keys == null || diminsion.keys.Count == 0)
                                {
                                    msgError = String.Format(modConstants.MSG_NOT_VALID_MODEL_STRUCTURE, "keys","");
                                    return false;
                                }

                                foreach (var key in diminsion.keys)
                                {
                                    bool okKey = false;
                                    foreach (var ky in de.keys)
                                    {
                                        if (key.name == ky.name)
                                        {
                                            okKey = true;
                                            break;
                                        }
                                    }
                                    if (okKey == false)
                                    {
                                        msgError = String.Format(modConstants.MSG_NOT_VALID_MODEL_STRUCTURE, "key", key.name);
                                        return false;
                                    }
                                }
                            }
                        }

                        if (ok == false)
                        {
                            msgError = String.Format(modConstants.MSG_NOT_VALID_MODEL_STRUCTURE, "diminsion", diminsion.name);
                            return false;
                        }
                    }
                }
            }

            return true;
        }
 }
}

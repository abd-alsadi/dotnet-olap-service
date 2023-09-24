
using KmnlkOLAPDll.Managment;
using KmnlkOLAPModel.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static KmnlkCommon.Shareds.LoggerManagement;

namespace KmnlkOLAPApi.Management
{
    public class ContextManagement
    {

        public ILog logger;
        public ConnectionManager connectionManager;

        public ContextManagement(ConnectionManager connectionManager, ILog logger)
        {
            this.connectionManager = connectionManager;
            this.logger = logger;
        }



        private OlapManagement olapBussinesManagement;
        public OlapManagement instanceOlapBussinesManagement()
        {
            if (olapBussinesManagement == null)
            {
                olapBussinesManagement = new OlapManagement(connectionManager, logger);

            }
            return olapBussinesManagement;
        }

    }
}
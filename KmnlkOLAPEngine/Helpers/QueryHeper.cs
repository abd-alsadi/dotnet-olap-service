using KmnlkOLAPModel.Constants;
using KmnlkOLAPModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkDWHEngine.Helpers
{
    public class QueryHeper
    {
        public static string BuidMDXQueryString(clsQuery request)
        {
            string query= "";
            string temp = "";
            int count_all_keys = 0;
            int count_visible_keys = 0;


             string build_dims = "";
             if (request.diminsions!=null)
            {
                if (request.diminsions.Count > 0)
                {
                    temp = PROCEDURES.QueryRows;
                }
                foreach (clsDiminsion dim in request.diminsions)
                {
                    foreach (clsKey key in dim.keys)
                    {
                        count_all_keys++;
                        if (key.visible || key.isFilter==false)
                        {
                            count_visible_keys++;
                            if (key.isFilter)
                            {
                                build_dims += "filter ([" + dim.name + "]" + "." + "[" + key.name + "].children ," +
                                    "[" + dim.name + "]" + "." + "[" + key.name + "].currentmember.memberValue " + key.filter.operation + "'" +
                                    key.filter.value + "' ) '";
                            }
                            else
                            {
                                build_dims += "( [" + dim.name + "]" + "." + "[" + key.name + "].children )'";
                            }
                        }
                    }
                }
                if (count_visible_keys > 0){
                        build_dims = build_dims.Substring(0, build_dims.Length - 1);
                        temp = String.Format(temp, build_dims);
                 }
                 else
                 {
                        temp = "";
                  }
            }

             string build_mes = "";
             if (request.measures != null)
                {
                    foreach(clsMeasure me in request.measures)
                    {
                     build_mes += String.Format(PROCEDURES.QueryMeasure, me.name);
                    }

                    if (request.measures.Count > 0)
                    {
                        build_mes = build_mes.Substring(0, build_mes.Length - 2);
                    }
                }

            string build_conds = "";
            if (request.diminsions != null)
            {
                if ((count_all_keys-count_visible_keys) >0)
                {
                    build_conds += PROCEDURES.QueryWhere;
                }
                string build = "";
                foreach(clsDiminsion dim in request.diminsions)
                {
                    foreach(clsKey key in dim.keys)
                    {
                        if( key.isFilter)
                        {
                            build += String.Format(PROCEDURES.QueryFilter, dim.name, key.name, key.filter.operation, key.filter.value);
                            build += ",";
                        }
                    }
                }

                if ((count_all_keys - count_visible_keys) > 0 && build!="")
                {
                    build = build.Substring(0, build.Length - 1);
                    build_conds = String.Format(build_conds, build);
                }
            }

            temp = temp.Replace("$O", "{").Replace("$C", "}");
            query = String.Format(PROCEDURES.QueryGetConstant, request.source, build_mes, temp, build_conds);
            query = query.Replace("$O", "{").Replace("$C", "}");

            return query;
        }
    }
}

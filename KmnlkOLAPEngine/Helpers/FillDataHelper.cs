using KmnlkOLAPModel;
using KmnlkOLAPModel.Constants;
using KmnlkOLAPModel.Models;
using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkDWHEngine.Helpers
{
    public class FillDataHelper
    {
        public static object FillDataString(AdomdDataReader reader, clsQuery request)
        {
            StringBuilder result = new StringBuilder();
            int index = -1;
            result.Append("[\n"); //begin array
            if (reader != null && request != null)
            {
                while (reader.Read())
                {
                    result.Append("{\n"); // begin row
                    index = 0;
                    if (request.diminsions != null)
                    {
                        foreach(clsDiminsion dim in request.diminsions)
                        {
                            foreach(clsKey key in dim.keys)
                            {
                                if (key.visible || key.isFilter==false)
                                {
                                    string keyName = dim.name + "." + key.name;
                                    string keyValue = reader[index].ToString();
                                    string rowString = String.Format(PROCEDURES.rowString, keyName, keyValue)+",";
                                    result.Append(rowString);
                                    index++;
                                }
                            }
                        }
                        //if (index > 0)
                        //{
                        //    result.Remove(result.Length - 1, 1);
                        //}
                    }

                    if (request.measures != null)
                    {
                        string temp = "";
                        foreach (clsMeasure me in request.measures)
                        {
                            string keyName = me.name ;
                            string keyValue = reader[index].ToString();
                            string rowString = String.Format(PROCEDURES.rowString, keyName, keyValue)+",";
                            temp += rowString;
                            index++;
                         }
                        result.Append(temp);
                    }

                    if (request.diminsions != null || request.measures != null)
                    {
                        result.Remove(result.Length - 1, 1);
                    }

                    result.Append("\n},\n"); // end row
                    
                }
                if(index!=-1)
                    result.Remove(result.Length - 2, 1);

                result.Append("]"); //end array
                return result.ToString();
            }

            return "";
        }
        public static List<kmnlkOLAPModel> FillDataObject(AdomdDataReader reader, clsQuery request)
        {
            List<kmnlkOLAPModel> result = new List<kmnlkOLAPModel>();
                if (reader != null)
                {
                    int index = 0;
                    while (reader.Read())
                    {
                        index = 0;
                        List<clsDiminsion> new_dims = new List<clsDiminsion>();
                        List<clsMeasure> new_mes = new List<clsMeasure>();
                        List<clsKey> keys = new List<clsKey>();
                        if (request.diminsions != null)
                        {
                            foreach (clsDiminsion dim in request.diminsions)
                            {
                                clsDiminsion new_dim = new clsDiminsion() { name = dim.name };
                                new_dim.keys = new List<clsKey>();
                                foreach (clsKey key in dim.keys)
                                {
                                    if (key.visible)
                                    {
                                        string new_key_name = new_dim.name + "." + key.name;
                                        clsKey new_key = new clsKey() { name = new_key_name, value = reader[index].ToString() };
                                        keys.Add(new_key);
                                        index++;
                                    }
                                }
                            }
                        }
                        if (request.measures != null)
                        {
                            foreach (clsMeasure me in request.measures)
                            {
                                clsMeasure new_me = new clsMeasure() { name = me.name, value = reader[index].ToString() };
                                new_mes.Add(new_me);
                                index++;
                            }
                        }
                        clsData data = new clsData() { keys = keys, measures = new_mes };
                        result.Add(data);
                    }

                    return result;
                }
           
            return result;
        }
    }
}

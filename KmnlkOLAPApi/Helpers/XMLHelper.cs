using KmnlkOLAPModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace KmnlkOLAPApi.Helpers
{
    public class XMLHelper
    {
        public static Dictionary<object, object> readStructure(string pathSettingsStructure,string KEY_Name,string KEY_Measures,string KEY_Diminsions,string KEY_Keys)
        {
            Dictionary<object, object> dic = new Dictionary<object, object>();
            try
            {
                XDocument xml = XDocument.Load(pathSettingsStructure);
                var nodes = xml.Descendants("Source").ToList();
                foreach (XElement node in nodes)
                {
                    clsQuery query = new clsQuery();
                    string sourceName = node.Attribute(XName.Get(KEY_Name)).Value;
                    query.source = sourceName;
                    XElement measuresE = node.Element(XName.Get(KEY_Measures));
                    query.measures = new List<clsMeasure>();
                    foreach (var measure in measuresE.Elements())
                    {
                        string measureName = measure.Attribute(XName.Get(KEY_Name)).Value;
                        clsMeasure me = new clsMeasure() { name = measureName };
                        query.measures.Add(me);
                    }

                    XElement diminsionsE = node.Element(XName.Get(KEY_Diminsions));
                    query.diminsions = new List<clsDiminsion>();
                    foreach (var dim in diminsionsE.Elements())
                    {
                        string dimName = dim.Attribute(XName.Get(KEY_Name)).Value;
                        clsDiminsion de = new clsDiminsion();
                        de.name = dimName;
                        XElement keysE = dim.Element(XName.Get(KEY_Keys));
                        List<clsKey> ks = new List<clsKey>();
                        foreach (var key in keysE.Elements())
                        {
                            string keyName = key.Attribute(XName.Get(KEY_Name)).Value;
                            ks.Add(new clsKey() { name = keyName });
                        }
                        de.keys = ks;
                        query.diminsions.Add(de);
                    }

                    dic.Add(query.source, query);
                }
            }
            catch (Exception e)
            {
                return dic;
            }
            return dic;
        }
    }
}
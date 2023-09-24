using KmnlkCommon.Shareds;
using KmnlkOLAPApi.Helpers;
using KmnlkOLAPModel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using static KmnlkCommon.Shareds.RWFManagement;

namespace KmnlkOLAPApi.Management
{
    public class SettingsManagement
    {
        private static Dictionary<object, object> settings;
        public static Dictionary<object, object> settings_structure;
        private static string pathSettings = "";
        private static string pathSettingsStructure = "";
        private static string folderSettings = "Settings";

        public static string KEY_ConnectionString = "ConnectionString";
        public static string KEY_PublicKey = "PublicKey";
        public static string KEY_DBPassword = "DBPassword";
        public static string KEY_EnableLog = "EnableLog";
        public static string KEY_TypeLog = "TypeLog";
        public static string KEY_GlobalTypeLog = "GlobalTypeLog";
        public static string KEY_PathLog = "PathLog";
        public static string KEY_GlobalPathLog = "GlobalPathLog";
        public static string KEY_DataFolder = "DataFolder";
        public static string KEY_UploadFilesMaximumFileSize = "UploadFilesMaximumFileSize";
        public static string KEY_OCRServiceUrl = "OCRServiceUrl";
        public static string KEY_DBType = "DBType";
        private static void loadSettings()
        {
            XMLFileObject XML = new XMLFileObject(pathSettings);
            settings=XML.ReadAll();
        }

        public static string KEY_Sources = "Sources";
        public static string KEY_Source = "Source";
        public static string KEY_Measures = "Measures";
        public static string KEY_Measure = "Measure";
        public static string KEY_Diminsions = "Diminsions";
        public static string KEY_Diminsion = "Diminsion";
        public static string KEY_Keys = "Keys";
        public static string KEY_Key = "Key";
        public static string KEY_Name = "Name";
        public static void loadStructure()
        {
            settings_structure = XMLHelper.readStructure(pathSettingsStructure, KEY_Name, KEY_Measures, KEY_Diminsions, KEY_Keys);
        }
        public static object getSetting(string key,string defaultValue="")
        {
            if (settings == null )
            {
                settings = new Dictionary<object, object>();
                pathSettings = EnvironmentManagement.getRootPath();
                pathSettings = Path.Combine(pathSettings, folderSettings);
                pathSettings = Path.Combine(pathSettings, "config.xml");

                settings_structure = new Dictionary<object, object>();
                pathSettingsStructure = EnvironmentManagement.getRootPath();
                pathSettingsStructure = Path.Combine(pathSettingsStructure, folderSettings);
                pathSettingsStructure = Path.Combine(pathSettingsStructure, "structure_config.xml");

                loadSettings();
                loadStructure();
            }

            object value = null;
            settings.TryGetValue(key, out value);
            if (value==null )
            {
                return defaultValue;
            }
            return value;
        }
    }
}
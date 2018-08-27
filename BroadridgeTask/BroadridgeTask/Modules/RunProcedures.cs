using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BroadridgeTask.Modules
{
    internal static class RunProcedures
    {
        private const string sourceTypeData = "data";
        private const string procLoadName = "ProcLoadName";
        private const string procSaveName = "ProcSaveName";
        private const string procDeleteName = "ProcDeleteName";
        private const string conStrName = "broadridge";

        internal static object RunProcLoad<T>(T model) where T : class
        {
            string procName;
            string sourceType = sourceTypeData;
            Dictionary<string, object> modelParams;
            List<Dictionary<string, object>> jsonData;

            ModelInfo.GetModelData(procLoadName, model, out procName, out modelParams);
            jsonData = RunProc(procName, modelParams);

            return new { jsonData, sourceType };
        }

        internal static object RunProcSave<T>(T model) where T : class
        {
            string procName;
            string sourceType = sourceTypeData;
            Dictionary<string, object> modelParams;
            List<Dictionary<string, object>> jsonData;

            ModelInfo.GetModelData(procSaveName, model, out procName, out modelParams);
            jsonData = RunProc(procName, modelParams);
            return new { jsonData, sourceType };
        }       

        internal static object RunProcDelete<T>(T model) where T : class
        {
            string procName;
            string sourceType = sourceTypeData;
            Dictionary<string, object> modelParams;
            List<Dictionary<string, object>> jsonData;

            ModelInfo.GetModelData(procDeleteName, model, out procName, out modelParams);
            jsonData = RunProc(procName, modelParams);
            return new { jsonData, sourceType };
        }

        public static List<Dictionary<string, object>> RunProc(string procName, Dictionary<string, object> procParams)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            string connectionString = ConfigurationManager.ConnectionStrings[conStrName].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = procName;
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var procParam in procParams)
                {
                    cmd.Parameters.Add(new SqlParameter(procParam.Key, procParam.Value));
                }

                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        row = new Dictionary<string, object>();

                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader[i]);
                        }
                        result.Add(row);
                    }
                }
            }            
            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using BroadridgeTask.Modules.CustomAttributes;

namespace BroadridgeTask.Modules
{
    internal static class ModelInfo
    {
        internal static void GetModelData<T>(string typeProc, T model, out string procName, out Dictionary<string, object> modelParams) where T : class
        {
            modelParams = GetModelParams(model);
            procName = GetProcName(typeProc, typeof(T));
        }

        internal static string GetModelName<T>(T model) where T : class
        {
            return model.GetType().Name;
        }

        private static Dictionary<string, object> GetModelParams<T>(T model) where T : class
        {
            Type typeModel; List<PropertyInfo> props;
            Dictionary<string, object> result = new Dictionary<string, object>();

            if (model != null)
            {
                typeModel = typeof(T);
                props = typeModel.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();

                    foreach (PropertyInfo prop in props)
                    {
                        if (prop.GetValue(model) != null)
                        {
                            result.Add(prop.Name, prop.GetValue(model));
                        }
                    }
                }

            return result;
        }

        private static string GetProcName(string procType, Type typeModel)
        {
            return GetFormExtraProp(procType, typeModel);
        }

        private static string GetFormExtraProp(string propType, Type typeModel)
        {
            object getFormExtraProp = null;

            FormExtraInfoAttribute attr = Attribute.GetCustomAttribute(typeModel, typeof(FormExtraInfoAttribute)) as FormExtraInfoAttribute;

            if (attr != null)
            {
                var attrProp = attr.GetType().GetProperties().Where(x => x.Name == propType).FirstOrDefault();

                if (attrProp != null)
                {
                    getFormExtraProp = attrProp.GetValue(attr);
                }
            }

            return getFormExtraProp != null ? getFormExtraProp.ToString() : null;
        }
    }

    internal static class ModelStateErrors
    {
        private const string scrTypeVld = "validation";
        internal static object GetModelStateErrors(ModelStateDictionary modelState)
        {
            string sourceType = scrTypeVld;
            List<object> jsonData = new List<object>();

            List<ModelError> modelErrors = modelState.Values.SelectMany(v => v.Errors).ToList();

            for (int i = 0; modelErrors.Count() > i; i++)
            {
                jsonData.Add(new { ErrMsg = modelErrors[i].ErrorMessage });
            }

            return new { jsonData, sourceType };
        }
    }
}

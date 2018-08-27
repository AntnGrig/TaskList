using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;

namespace BroadridgeTask.Modules
{
    internal static class CallProcedures
    {
        internal static object CallProcLoad<T>(T model, ModelStateDictionary modelState) where T : class
        {
            return modelState.IsValid ? RunProcedures.RunProcLoad(model) : ModelStateErrors.GetModelStateErrors(modelState);
        }

        internal static object CallProcSave<T>(T model, ModelStateDictionary modelState) where T : class
        {
            return modelState.IsValid ? RunProcedures.RunProcSave(model) : ModelStateErrors.GetModelStateErrors(modelState);
        }

        internal static object CallProcDelete<T>(T model, ModelStateDictionary modelState) where T : class
        {
            return modelState.IsValid ? RunProcedures.RunProcDelete(model) : ModelStateErrors.GetModelStateErrors(modelState);
        }
    }
}
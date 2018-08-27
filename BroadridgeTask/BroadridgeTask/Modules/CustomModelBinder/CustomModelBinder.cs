using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BroadridgeTask.Modules.CustomModelBinder
{
    public class CustomModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Type type = bindingContext.ModelMetadata.ModelType.UnderlyingSystemType;
            object bindModel = null;
            object val = null;

            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueProviderResult };

            if (valueProviderResult != null)
            {
                val = valueProviderResult.AttemptedValue;

                if (!(string.IsNullOrEmpty(val.ToString())))
                {
                    try
                    {
                        if (type == typeof(Nullable<decimal>))
                        {
                            bindModel = Convert.ToDecimal(valueProviderResult.AttemptedValue, CultureInfo.CurrentCulture);
                        }

                        if (type == typeof(Nullable<DateTime>))
                        {
                            bindModel = Convert.ToDateTime(valueProviderResult.AttemptedValue, CultureInfo.CurrentCulture);
                        }
                    }
                    catch (FormatException ex)
                    {
                        modelState.Errors.Add(ex);
                    }
                }
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            return bindModel;
        }
    }
}
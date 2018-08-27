using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security;
using System.Net;
using System.IO;
using System.Dynamic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using BroadridgeTask.Models;
using BroadridgeTask.Modules;

namespace BroadridgeTask.Controllers
{
    public class SPAController: BaseController
    {
        public JsonResult RunLoadJson(TaskModelLoad model)
        {
            var jsonResult = Json(CallProcedures.CallProcLoad(model, ModelState), JsonRequestBehavior.DenyGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult RunSaveJson(TaskModelSave model)
        {
            model.UserCode = UserInfo.GetUser();
            return Json(CallProcedures.CallProcSave(model, ModelState), JsonRequestBehavior.DenyGet);
        }

        public JsonResult RunDeleteJson(TaskModelDelete model)
        {
            model.UserCode = UserInfo.GetUser();
            return Json(CallProcedures.CallProcDelete(model, ModelState), JsonRequestBehavior.DenyGet);
        }
    }
}
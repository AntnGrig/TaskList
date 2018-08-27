using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BroadridgeTask.Modules.CustomAttributes;


namespace BroadridgeTask.Models
{
    [FormExtraInfo(ProcLoadName = "dbo.Task_Load")]
    public class TaskModelLoad
    {
        public string UserCode { get; set; }
    }
}
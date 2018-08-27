using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BroadridgeTask.Modules.CustomAttributes;


namespace BroadridgeTask.Models
{
    [FormExtraInfo(ProcDeleteName = "dbo.Task_Delete")]
    public class TaskModelDelete
    {
        [Required(ErrorMessage = "Row ID is required.")]
        public string ID { get; set; }
        public string UserCode { get; set; }
    }
}
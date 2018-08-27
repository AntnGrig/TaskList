using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BroadridgeTask.Modules.CustomAttributes;


namespace BroadridgeTask.Models
{
    [FormExtraInfo(ProcSaveName = "dbo.Task_Save")]
    public class TaskModelSave
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "Task name is required.")]
        [StringLength(255, ErrorMessage = "Lenght of task name has to have been less or equal 255 symbols.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Task description is required.")]
        [StringLength(2000, ErrorMessage = "Lenght of task description has to have been less or equal 2000 symbols.")]
        public string TaskDescription { get; set; }
        [Required(ErrorMessage = "Task priority is required.")]
        [RegularExpression("^([1-9]|1[0])$", ErrorMessage = "Enter correct value of task priority (1-10).")]
        public string TaskPriority { get; set; }
        public DateTime? Added { get; set; }
        [Required(ErrorMessage = "Task time to complete is required.")]
        public DateTime? DateToComplete { get; set; }
        [Required(ErrorMessage = "Task status is required.")]
        public string TaskStatus { get; set; }
        public string UserCode { get; set; }
    }
}
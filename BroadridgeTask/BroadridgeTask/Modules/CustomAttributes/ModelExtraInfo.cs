using System;

namespace BroadridgeTask.Modules.CustomAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class FormExtraInfoAttribute : Attribute
    {
        public string ProcListName { get; set; }
        public string ProcLoadName { get; set; }
        public string ProcSaveName { get; set; }
        public string ProcDeleteName { get; set; }
    }
}

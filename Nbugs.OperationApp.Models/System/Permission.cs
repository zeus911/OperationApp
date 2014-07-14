namespace Nbugs.OperationApp.Models.System
{
    public class Permission
    {
        public string KeyCode { get; set; }

        public bool IsValid { get; set; }

        public string Controller { get; set; }
    }
}
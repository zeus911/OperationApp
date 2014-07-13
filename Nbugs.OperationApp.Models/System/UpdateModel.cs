namespace Nbugs.OperationApp.Models.System
{
    public class UpdateModel
    {
        public int RoleId { get; set; }

        public int ModuleId { get; set; }

        public string KeyCode { get; set; }

        public string Name { get; set; }

        public bool IsValid { get; set; }
    }
}
namespace Nbugs.OperationApp.Models.System
{
    public class RoleModuleAction
    {
        public int Id { get; set; }

        public int RoleModuleId { get; set; }

        public string KeyCode { get; set; }

        public bool IsValid { get; set; }

        public virtual RoleModule RoleModule { get; set; }
    }
}
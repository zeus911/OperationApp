namespace Nbugs.OperationApp.Models.System
{
    public class Query
    {
        public int page { get; set; }

        public int rows { get; set; }

        public int total { get; set; }

        public string sort { get; set; }

        public string order { get; set; }
    }
}
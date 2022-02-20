namespace ProjectManagerAPI.ViewModels.HoursLogs
{
    public class ReportVM
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public List<int?> Users { get; set; }
    }
}

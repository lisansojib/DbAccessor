namespace ApplicationCore.Models
{
    public class UserStatistics
    {
        public int Id { get; set; } 
        public int UserId { get; set; }
        public int TeamId { get; set; }

        /// <summary>
        /// Get name from <see cref="UserStatisticsTypes"/> enum.
        /// </summary>
        public string StatisticsType { get; set; }
        public int Count { get; set; }
        public int Quota { get; set; }
        public DateTime LastUpdateOn { get; set; } = DateTime.Now;
        public DateTime? LastMonthlyCycleStart { get; set; }
        public DateTime? LastMonthlyCycleEnd { get; set; }
    }
}

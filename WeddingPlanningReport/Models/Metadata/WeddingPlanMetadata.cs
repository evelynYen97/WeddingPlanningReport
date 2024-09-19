namespace WeddingPlanningReport.Models.Metadata
{
    internal class WeddingPlanMetadata
    {
        public int CaseId { get; set; }

        public int MemberId { get; set; }

        public string? WeddingName { get; set; }

        public string? Introduction { get; set; }

        public DateTime? WeddingTime { get; set; }

        public string? WeddingLocation { get; set; }
    }
}
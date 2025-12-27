namespace Agrc.Api.Entities
{
    public class RbacComplianceResult
    {
        public bool Pass { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public int AdminCount { get; set; }
        public DateTime EvidenceGeneratedAt { get; set; }
        public object Assignments { get; set; } = default!;
    }

}

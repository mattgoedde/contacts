namespace Contacts.Domain.Models;

public record Audit
{
    public int Id { get; set; }
    public DateTime EventTime { get; set; }
    public string? User { get; set; }
    public string? AuditType { get; set; }
    public string? TableName { get; set; }
    public Dictionary<string, object?>? OldRecord { get; set; }
    public Dictionary<string, object?>? NewRecord { get; set; }
}
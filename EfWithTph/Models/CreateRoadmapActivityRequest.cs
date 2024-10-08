namespace EfWithTph.Models;

public sealed record CreateRoadmapActivityRequest
{
    public int? ParentActivityId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
}

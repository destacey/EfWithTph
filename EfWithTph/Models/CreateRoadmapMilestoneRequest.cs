namespace EfWithTph.Models;

public sealed record CreateRoadmapMilestoneRequest
{
    public int? ParentActivityId { get; init; }
    public string Name { get; init; } = default!;
    public string? Description { get; init; }
    public DateTime Date { get; init; }
}

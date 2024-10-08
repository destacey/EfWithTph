using EfWithTph.Enums;
using EfWithTph.Interfaces;

namespace EfWithTph.Models;

public abstract class BaseRoadmapItem : IRoadmapItem
{
    public int Id { get; init; }
    public int RoadmapId { get; init; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public RoadmapItemType Type { get; set; }
    public int? ParentId { get; set; }
    public RoadmapActivity? Parent { get; set; }
}

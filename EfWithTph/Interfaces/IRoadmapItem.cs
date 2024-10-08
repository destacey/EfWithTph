using EfWithTph.Enums;
using EfWithTph.Models;

namespace EfWithTph.Interfaces;

public interface IRoadmapItem
{
    int Id { get; }
    int RoadmapId { get; }
    string Name { get; }
    string? Description { get; }
    RoadmapItemType Type { get; }
    int? ParentId { get; }
    RoadmapActivity? Parent { get; }
}

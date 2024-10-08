using EfWithTph.Enums;

namespace EfWithTph.Models;

public class RoadmapMilestone : BaseRoadmapItem
{
    private RoadmapMilestone() { }

    public RoadmapMilestone(int roadmapId, int? parentId, string name, string? description, DateTime date)
    {
        RoadmapId = roadmapId;
        ParentId = parentId;
        Name = name;
        Description = description;
        Date = date;
        Type = RoadmapItemType.Milestone;
    }

    public DateTime Date { get; set; }
}

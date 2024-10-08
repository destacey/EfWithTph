using EfWithTph.Enums;

namespace EfWithTph.Models;

public class RoadmapActivity : BaseRoadmapItem
{
    private readonly List<BaseRoadmapItem> _children = [];

    private RoadmapActivity() { }

    public RoadmapActivity(int roadmapId, int? parentId, string name, string? description, DateTime start, DateTime end)
    {
        RoadmapId = roadmapId;
        ParentId = parentId;
        Name = name;
        Description = description;
        Start = start;
        End = end;
        Type = RoadmapItemType.Activity;
    }

    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public IReadOnlyList<BaseRoadmapItem> Children => _children.AsReadOnly();

    public RoadmapActivity CreateChildActivity(string name, string? description, DateTime start, DateTime end)
    {
        var activity = new RoadmapActivity(RoadmapId, Id, name, description, start, end);
        _children.Add(activity);

        return activity;
    }

    public RoadmapMilestone CreateChildMilestone(string name, string? description, DateTime date)
    {
        var milestone = new RoadmapMilestone(RoadmapId, Id, name, description, date);
        _children.Add(milestone);

        return milestone;
    }

}

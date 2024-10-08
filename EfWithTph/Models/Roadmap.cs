namespace EfWithTph.Models;

public class Roadmap
{
    private readonly List<BaseRoadmapItem> _items = [];

    public int Id { get; set; }
    public required string Name { get; set; }
    public IReadOnlyList<BaseRoadmapItem> Items => _items.AsReadOnly();

    public RoadmapActivity CreateActivity(int? parentActivityId, string name, string? description, DateTime start, DateTime end)
    {
        RoadmapActivity activity;
        if (parentActivityId is not null)
        {
            var parentActivity = _items.OfType<RoadmapActivity>().FirstOrDefault(a => a.Id == parentActivityId) 
                ?? throw new InvalidOperationException("Parent activity not found");

            activity = parentActivity.CreateChildActivity(name, description, start, end);
        }
        else
        {
            activity = new RoadmapActivity(Id, null, name, description, start, end);
        }

        _items.Add(activity);

        return activity;
    }

    public RoadmapMilestone CreateMilestone(int? parentActivityId, string name, string? description, DateTime date)
    {
        RoadmapMilestone milestone;
        if (parentActivityId is not null)
        {
            var parentActivity = _items.OfType<RoadmapActivity>().FirstOrDefault(a => a.Id == parentActivityId) 
                ?? throw new InvalidOperationException("Parent activity not found");

            milestone = parentActivity.CreateChildMilestone(name, description, date);
        }
        else
        {
            milestone = new RoadmapMilestone(Id, null, name, description, date);
        }

        _items.Add(milestone);

        return milestone;
    }
}

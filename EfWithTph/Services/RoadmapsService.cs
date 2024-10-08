using EfWithTph.Data;
using EfWithTph.Models;
using Microsoft.EntityFrameworkCore;

namespace EfWithTph.Services;

public class RoadmapsService
{
    private readonly AppDbContext _appDbContext;

    public RoadmapsService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Roadmap>> GetRoadmaps()
    {
        return await _appDbContext.Roadmaps.ToListAsync();
    }

    public async Task<Roadmap?> GetRoadmap(int id)
    {
        return await _appDbContext.Roadmaps
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IReadOnlyList<BaseRoadmapItem>?> GetRoadmapItems(int id)
    {
        var roadmap = await _appDbContext.Roadmaps
            .Include(e => e.Items)
            .FirstOrDefaultAsync(e => e.Id == id);

        return roadmap?.Items.Where(i => i.ParentId == null).ToList();
    }

    public async Task<Roadmap> CreateRoadmap(string name)
    {
        var roadmap = new Roadmap
        {
            Name = name
        };

        _appDbContext.Roadmaps.Add(roadmap);
        await _appDbContext.SaveChangesAsync();

        return roadmap;
    }

    public async Task<Roadmap> UpdateRoadmap(int id, string name)
    {
        var roadmap = await _appDbContext.Roadmaps
            .FirstOrDefaultAsync(e => e.Id == id);

        if (roadmap is null)
        {
            throw new InvalidOperationException("Roadmap not found");
        }

        roadmap.Name = name;

        await _appDbContext.SaveChangesAsync();

        return roadmap;
    }

    public async Task DeleteRoadmap(int id)
    {
        var roadmap = await _appDbContext.Roadmaps
            .FirstOrDefaultAsync(e => e.Id == id);

        if (roadmap is null)
        {
            throw new InvalidOperationException("Roadmap not found");
        }

        _appDbContext.Roadmaps.Remove(roadmap);

        await _appDbContext.SaveChangesAsync();
    }


    #region Roadmap Activities

    public async Task<RoadmapActivity> GetRoadmapActivity(int roadmapId, int activityId)
    {
        var roadmap = await _appDbContext.Roadmaps
            .Include(e => e.Items)
            .FirstOrDefaultAsync(e => e.Id == roadmapId);

        if (roadmap is null)
        {
            throw new InvalidOperationException("Roadmap not found");
        }

        var activity = roadmap.Items.OfType<RoadmapActivity>()
            .FirstOrDefault(e => e.Id == activityId);

        if (activity is null)
        {
            throw new InvalidOperationException("Activity not found");
        }

        return activity;
    }

    public async Task<RoadmapActivity> CreateRoadmapActivity(int roadmapId, int? parentActivityId, string name, DateTime start, DateTime end)
    {
        var roadmap = await _appDbContext.Roadmaps
            .Include(e => e.Items)
            .FirstOrDefaultAsync(e => e.Id == roadmapId);

        if (roadmap is null)
        {
            throw new InvalidOperationException("Roadmap not found");
        }

        var activity = roadmap.CreateActivity(parentActivityId, name, null, start, end);

        await _appDbContext.SaveChangesAsync();

        return activity;
    }

    #endregion Roadmap Activities

    #region Roadmap Milestones

    public async Task<RoadmapMilestone> GetRoadmapMilestone(int roadmapId, int milestoneId)
    {
        var roadmap = await _appDbContext.Roadmaps
            .Include(e => e.Items)
            .FirstOrDefaultAsync(e => e.Id == roadmapId);

        if (roadmap is null)
        {
            throw new InvalidOperationException("Roadmap not found");
        }

        var milestone = roadmap.Items.OfType<RoadmapMilestone>()
            .FirstOrDefault(e => e.Id == milestoneId);

        if (milestone is null)
        {
            throw new InvalidOperationException("Milestone not found");
        }

        return milestone;
    }

    public async Task<RoadmapMilestone> CreateRoadmapMilestone(int roadmapId, int? parentActivityId, string name, DateTime date)
    {
        var roadmap = await _appDbContext.Roadmaps
            .Include(e => e.Items)
            .FirstOrDefaultAsync(e => e.Id == roadmapId);

        if (roadmap is null)
        {
            throw new InvalidOperationException("Roadmap not found");
        }

        var milestone = roadmap.CreateMilestone(parentActivityId, name, null, date);

        await _appDbContext.SaveChangesAsync();

        return milestone;
    }

    #endregion Roadmap Milestones
}

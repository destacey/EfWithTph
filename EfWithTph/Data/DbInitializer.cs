using EfWithTph.Models;

namespace EfWithTph.Data;

public class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        // https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure.databasefacade.ensurecreated?view=efcore-8.0
        context.Database.EnsureCreated();

        Seed(context);
    }

    public static void Seed(AppDbContext context)
    {
        if (context.Roadmaps.Any())
        {
            return;
        }

        SeedRoadmap(context);
    }

    private static void SeedRoadmap(AppDbContext context)
    {
        var roadmap = new Roadmap
        {
            Name = "My Roadmap"
        };

        context.Roadmaps.Add(roadmap);
        context.SaveChanges();

        SeedRoadmapItems(context, roadmap);
    }



    private static void SeedRoadmapItems(AppDbContext context, Roadmap roadmap)
    {
        // LEVEL 1
        var activity1 = roadmap.CreateActivity(null, "Activity 1", "Description 1", DateTime.Now, DateTime.Now.AddDays(5));
        var activity2 = roadmap.CreateActivity(null, "Activity 2", null, DateTime.Now.AddDays(3), DateTime.Now.AddDays(13));
        var activity3 = roadmap.CreateActivity(null, "Activity 3", null, DateTime.Now.AddDays(7), DateTime.Now.AddDays(20));
        var milestone1 = roadmap.CreateMilestone(null, "Milestone 1", "Description 2", DateTime.Now.AddDays(2));

        context.SaveChanges();

        // LEVEL 2
        var activity1_1 = roadmap.CreateActivity(activity1.Id, "Activity 1.1", "Description 3", DateTime.Now, DateTime.Now.AddDays(5));
        var activity1_2 = roadmap.CreateActivity(activity1.Id, "Activity 1.2", null, DateTime.Now.AddDays(3), DateTime.Now.AddDays(13));
        var milestone1_1 = roadmap.CreateMilestone(activity1.Id, "Milestone 1.1", "Description 4", DateTime.Now.AddDays(2));

        var activity2_1 = roadmap.CreateActivity(activity2.Id, "Activity 2.1", "Description 3", DateTime.Now.AddDays(3), DateTime.Now.AddDays(25));

        context.SaveChanges();
    }
}

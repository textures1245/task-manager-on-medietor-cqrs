using MediatrCqrs.Application.Domain.Entities.Task;
using MediatrCqrs.Application.Domain.ValueObjects.Colour;

namespace MediatrCqrs.Application.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        if (!context.TaskLists.Any())
        {
            var taskList = new TaskList
            {
                Title = "What needs to be done?",
                Colour = Colour.Yellow,
            };

            taskList.AddItems(Enumerable.Range(1, 10).Select(i => new TaskItem
            {
                Title = $"Task {i}",
                Note = $"Description for Task {i}"
            }));

            context.TaskLists.Add(taskList);

            await context.SaveChangesAsync();
        }
    }
}

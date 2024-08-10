using Cocona;
using GitRepoManager;
using Microsoft.Extensions.DependencyInjection;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddTransient<IService, Service>();

var app = builder.Build();

app.AddCommand("current", (IService sevice) =>
{
    var repo = sevice.GetCurrentRepo();
    Console.WriteLine(repo ?? "none");
});

app.AddCommand("list", (IService sevice) =>
{
    var list = sevice.List();
    var current = sevice.GetCurrentRepo();
    list.ForEach(x => { Console.WriteLine((x == current ? ">" : " ") + x); });
});

app.AddCommand("use", (IService sevice, [Argument(Description = "fw or dk or none")] string repo) =>
{
    repo.Guard("fw", "dk", "none");
    sevice.Use(repo);
    var current = sevice.GetCurrentRepo();
    Console.WriteLine(!string.IsNullOrEmpty(current) ? "Now using " + current : "cleared");
});

app.Run();
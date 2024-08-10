using LibGit2Sharp;

namespace GitRepoManager;

public interface IService
{
    List<string> List();
    string? GetCurrentRepo();
    void Use(string repo);
}

public class Service : IService
{
    public List<string> List()
    {
        var gitFolders =
            Directory.GetDirectories(Directory.GetCurrentDirectory(), "*.git", SearchOption.TopDirectoryOnly);
        var list = new List<string>();
        foreach (var folder in gitFolders)
        {
            var repo = GetRepo(folder);
            if (!string.IsNullOrEmpty(repo))
            {
                list.Add(repo);
            }
        }

        return list;
    }

    public string? GetCurrentRepo()
    {
        return GetRepo(".git");
    }

    private static string? GetRepo(string path)
    {
        try
        {
            using var repo = new Repository(path);
            var url = repo.Config.Get<string>("remote.origin.url").Value;

            if (url is null)
            {
                return null;
            }

            var map = new Dictionary<string, string>()
            {
                { "fw-next", "fw" },
                { "dk-lab", "dk" }
            };

            return (from kv in map where url.Contains(kv.Key) select kv.Value).FirstOrDefault();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void Use(string repo)
    {
        var current = GetCurrentRepo();
        if (repo == current)
        {
            return;
        }

        if (!string.IsNullOrEmpty(current))
        {
            Directory.Move(".git", current + ".git");
        }

        if (repo == "none")
        {
            return;
        }

        Directory.Move(repo + ".git", ".git");
    }
}
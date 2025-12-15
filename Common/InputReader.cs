namespace Common;

public static class InputReader
{
    public static string[] ReadLines(string day, string filename)
    {
        var path = GetInputPath(day, filename);
        return File.ReadAllLines(path);
    }

    private static string GetInputPath(string day, string filename)
    {
        var baseDir = AppContext.BaseDirectory;
        var solutionRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));
        return Path.Combine(solutionRoot, day, filename);
    }
}

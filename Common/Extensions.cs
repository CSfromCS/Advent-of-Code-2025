namespace Common;

public static class Extensions
{
    /// <summary>
    /// Parses each line as an integer.
    /// </summary>
    public static IEnumerable<int> AsInts(this string[] lines) =>
        lines.Select(int.Parse);

    /// <summary>
    /// Parses each line as a long.
    /// </summary>
    public static IEnumerable<long> AsLongs(this string[] lines) =>
        lines.Select(long.Parse);

    /// <summary>
    /// Splits input into groups separated by blank lines.
    /// </summary>
    public static IEnumerable<string[]> SplitByBlankLines(this string[] lines)
    {
        var group = new List<string>();
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                if (group.Count > 0)
                {
                    yield return group.ToArray();
                    group.Clear();
                }
            }
            else
            {
                group.Add(line);
            }
        }
        if (group.Count > 0)
        {
            yield return group.ToArray();
        }
    }

    /// <summary>
    /// Converts lines to a 2D char grid.
    /// </summary>
    public static char[,] ToGrid(this string[] lines)
    {
        int rows = lines.Length;
        int cols = lines[0].Length;
        var grid = new char[rows, cols];
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                grid[r, c] = lines[r][c];
            }
        }
        return grid;
    }
}

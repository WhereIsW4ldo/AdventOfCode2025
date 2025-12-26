using Domain.Interfaces;

namespace Domain.Implementations;

public class Day4 : IDay4
{
    private readonly bool[][] _grid;
    private int _amountOfRemovedRolls;

    public Day4()
    {
        _grid = [];
    }

    public Day4(string inputFilePath)
    {
        var input = File.ReadAllText(inputFilePath);
        _grid = ParseInput(input);
    }

    public async Task Solve(string inputFilePath)
    {
        var input = await File.ReadAllTextAsync(inputFilePath);
        var accessiblePaperRollCount = GetAccessiblePaperRollCount(input);
        Console.WriteLine($"Day 4: There are {accessiblePaperRollCount} accessible paper rolls.");

        RemoveAccessiblePaperRolls();
        Console.WriteLine($"Day 4: After removing all accessible paper rolls, there are {_amountOfRemovedRolls} paper rolls left.");
    }

    public bool[][] ParseInput(string input)
    {
        var lines = input
            .Split("\n")
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToArray();

        var rows = lines.Length;

        var grid = new bool[rows][];

        for (var i = 0; i < rows; i++)
        {
            grid[i] = ParseRow(lines[i]);
        }

        return grid;
    }

    public AccessiblePaperRoll IsAccessiblePaperRoll(bool[][] grid, int row, int column)
    {
        if (!grid[row][column]) return AccessiblePaperRoll.NotAPaperRoll;

        var surrounding = new List<bool>();

        // not top edge
        if (row > 0)
            surrounding.Add(grid[row - 1][column]);

        // not bottom edge
        if (row < grid.Length - 1)
            surrounding.Add(grid[row + 1][column]);

        // not left edge
        if (column > 0)
            surrounding.Add(grid[row][column - 1]);

        // not right edge
        if (column < grid[row].Length - 1)
            surrounding.Add(grid[row][column + 1]);

        // not top left corner
        if (row > 0 && column > 0)
            surrounding.Add(grid[row - 1][column - 1]);

        // not top right corner
        if (row > 0 && column < grid[row].Length - 1)
            surrounding.Add(grid[row - 1][column + 1]);

        // not bottom left corner
        if (row < grid.Length - 1 && column > 0)
            surrounding.Add(grid[row + 1][column - 1]);

        // not bottom right corner
        if (row < grid.Length - 1 && column < grid[row].Length - 1)
            surrounding.Add(grid[row + 1][column + 1]);

        return surrounding.Count(b => b) < 4
            ? AccessiblePaperRoll.Accessible
            : AccessiblePaperRoll.Inaccessible;
    }

    public int GetAccessiblePaperRollCount(string input)
    {
        var grid = ParseInput(input);
        var count = 0;

        for (var row = 0; row < grid.Length; row++)
        {
            for (var column = 0; column < grid[row].Length; column++)
            {
                if (IsAccessiblePaperRoll(grid, row, column) is AccessiblePaperRoll.Accessible)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public void RemoveAccessiblePaperRolls()
    {
        bool removedRollsThisIteration;

        do
        {
            removedRollsThisIteration = false;
            for (var row = 0; row < _grid.Length; row++)
            {
                for (var column = 0; column < _grid[row].Length; column++)
                {
                    if (IsAccessiblePaperRoll(_grid, row, column) is AccessiblePaperRoll.Accessible)
                    {
                        removedRollsThisIteration = true;
                        _grid[row][column] = false;
                        _amountOfRemovedRolls++;
                    }
                }
            }
        } while (removedRollsThisIteration);
    }

    public int GetAmountOfRemovedRolls()
    {
        return _amountOfRemovedRolls;
    }

    private static bool[] ParseRow(string row)
    {
        return [.. row
            .ToCharArray()
            .Select(c => c == '@')];
    }
}

using Domain.Interfaces;
using System.Text;

namespace Domain.Implementations;

public class Day7 : IDay7
{
    public long ParseAndProcess(string input)
    {
        var lines = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        var previousLineBeams = new List<Ray>();
        var totalNumberOfSplits = 0;

        foreach (var line in lines)
        {
            var lineLength = line.Length;

            var nonEmptyIndexes = line
                .Select((c, index) => new { c, index })
                .Where(x => x.c != '.')
                .Select(x => x.index)
                .ToList();

            var (toRemove, toAdd, numberOfSplits) = CalculateSplitLine(previousLineBeams, line, nonEmptyIndexes);

            foreach (var rayToAdd in toAdd)
            {
                var presentBeam = previousLineBeams.FirstOrDefault(r => r.Index == rayToAdd.Index);
                if (presentBeam is null)
                {
                    previousLineBeams.Add(rayToAdd);
                }
                else
                {
                    presentBeam.Weight += rayToAdd.Weight;
                }
            }

            foreach (var index in toRemove)
            {
                previousLineBeams.RemoveAll(r => toRemove.Contains(r.Index));
            }

            totalNumberOfSplits += numberOfSplits;
        }


        return totalNumberOfSplits;
    }

    private static void PrintLine(
            List<Ray> previousLineBeams,
            List<int> nonEmptyIndexes,
            int lineLength
            )
    {
        var tempBuilder = new StringBuilder();

        for (int i = 0; i < lineLength; i++)
        {
            char c;
            if (nonEmptyIndexes.Contains(i))
            {
                c = '^';
            }
            else
            {
                var beam = previousLineBeams.FirstOrDefault(r => r.Index == i);
                if (beam is not null)
                    c = Convert.ToChar(beam.Weight);
                else
                    c = '.';
            }
            tempBuilder.Append(c);
        }

        Console.WriteLine(tempBuilder.ToString());
    }

    private static (List<int> toRemove, List<Ray> toAdd, int numberOfSplits) CalculateSplitLine(
            List<Ray> previousLineBeams,
            string line,
            List<int> nonEmptyIndexes
            )
    {
        var indexesToRemove = new List<int>();
        var indexesToAdd = new List<Ray>();
        var numberOfSplits = 0;

        foreach (var index in nonEmptyIndexes)
        {
            var nonEmptyChar = line[index];

            var previousLineBeam = previousLineBeams.FirstOrDefault(b => b.Index == index);
            if (nonEmptyChar == '^' && previousLineBeam is not null)
            {
                var amountOfBeamsOnIdex = previousLineBeam.Weight;

                if (index > 0)
                {
                    indexesToAdd.Add(new Ray { Index = index - 1, Weight = previousLineBeam.Weight });
                }
                if (index < line.Length)
                {
                    indexesToAdd.Add(new Ray { Index = index + 1, Weight = previousLineBeam.Weight });
                }

                numberOfSplits++;
                indexesToRemove.Add(index);
            }
            else if (nonEmptyChar == 'S')
            {
                indexesToAdd.Add(new Ray { Index = index, Weight = 1 });
            }
        }

        return (indexesToRemove, indexesToAdd, numberOfSplits);
    }

    public async Task Solve(string inputFilePath)
    {
        var input = await File.ReadAllTextAsync(inputFilePath);

        var result = ParseAndProcess(input);

        Console.WriteLine($"output: {result}");
    }

    public async Task SolvePart2(string inputFilePath)
    {
        var input = await File.ReadAllTextAsync(inputFilePath);

        var result = ParseAndProcessPart2(input);

        Console.WriteLine($"output: {result}");
    }

    public long ParseAndProcessPart2(string input)
    {
        var lines = input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        var previousLineBeams = new List<Ray>();

        foreach (var line in lines)
        {
            var lineLength = line.Length;

            var nonEmptyIndexes = line
                .Select((c, index) => new { c, index })
                .Where(x => x.c != '.')
                .Select(x => x.index)
                .ToList();

            var (toRemove, toAdd, numberOfSplits) = CalculateSplitLine(previousLineBeams, line, nonEmptyIndexes);

            foreach (var rayToAdd in toAdd)
            {
                var presentBeam = previousLineBeams.FirstOrDefault(r => r.Index == rayToAdd.Index);
                if (presentBeam is null)
                {
                    previousLineBeams.Add(rayToAdd);
                }
                else
                {
                    presentBeam.Weight += rayToAdd.Weight;
                }
            }

            foreach (var index in toRemove)
            {
                previousLineBeams.RemoveAll(r => toRemove.Contains(r.Index));
            }
        }

        var total = 0L;
        foreach (var item in previousLineBeams)
        {
            total += item.Weight;
        }

        return total;
    }

    private record Ray
    {
        public int Index { get; set; }
        public long Weight { get; set; }
    }
}
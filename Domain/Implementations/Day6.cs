using Domain.Interfaces;

namespace Domain.Implementations;

public class Day6 : IDay6
{
    public Problem[] Parse(string input)
    {
        var lines = input.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

        var (numberLines, methods) = GetNumbersAndMethods(lines);

        if (numberLines.Count == 0) return [];

        var problemAmount = numberLines[0].Length;

        var numbersArray = numberLines.ToArray();

        return CreateProblems(methods, problemAmount, numbersArray);
    }

    private static Problem[] CreateProblems(List<Method> methods, int problemAmount, long[][] numbersArray)
    {
        var problems = new List<Problem>();

        for (int i = 0; i < problemAmount; i++)
        {
            var numbersForProblem = new List<long>();
            var innerLength = numbersArray.Length;
            for (int j = 0; j < innerLength; j++)
            {
                numbersForProblem.Add(numbersArray[j][i]);
            }

            var method = methods[i];

            problems.Add(new Problem { Method = method, Numbers = [.. numbersForProblem] });
        }

        return [.. problems];
    }

    private static (List<long[]> NumberLines, List<Method> Methods) GetNumbersAndMethods(string[] lines)
    {
        var numberLines = new List<long[]>();
        var methods = new List<Method>();

        foreach (var line in lines)
        {
            var elements = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length == 0) continue;

            var isNumberLine = elements[0]
                .All(s => long.TryParse(s.ToString(), out var _));

            if (isNumberLine)
            {
                var numbers = elements
                    .Select(long.Parse)
                    .ToArray();

                numberLines.Add(numbers);
            }
            else
            {
                methods = [.. elements.Select(s => s == "*" ? Method.Product : Method.Sum)];
            }
        }

        return (numberLines, methods);
    }

    public long Calculate(Problem[] problems)
    {
        var total = 0L;

        foreach (var problem in problems)
        {
            switch (problem.Method)
            {
                case Method.Sum:
                    total += problem.Numbers.Sum();
                    break;
                case Method.Product:
                    var subTotal = 1L;

                    foreach (var number in problem.Numbers)
                    {
                        subTotal *= number;
                    }

                    total += subTotal;
                    break;
            }
        }

        return total;
    }

    public async Task Solve(string inputFilePath)
    {
        var input = await File.ReadAllTextAsync(inputFilePath);

        var problems = Parse(input);

        var total = Calculate(problems);

        Console.WriteLine($"Total of problems is: {total}");
    }

    public Problem[] ParsePart2(string input)
    {
        var problems = new List<Problem>();

        var charArray = input
            .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.ToCharArray())
            .ToArray();

        var height = charArray.Length;
        var length = charArray[0].Length;

        var problemLengthStartIndexes = GetStartOfProblems(charArray, height, length);

        foreach (var problemText in GetTextPerProblem(charArray, height, length, problemLengthStartIndexes))
        {
            problems.AddRange(Parse(problemText));
        }

        return [.. problems];
    }

    private static List<string> GetTextPerProblem(char[][] charArray, int height, int length, List<int> problemLengthStartIndexes)
    {
        var textPerProblem = new List<string>();

        for (int i = 0; i < problemLengthStartIndexes.Count; i++)
        {
            var startIndex = problemLengthStartIndexes[i];
            var endIndex = problemLengthStartIndexes.ElementAtOrDefault(i + 1);

            if (endIndex == 0)
                endIndex = length;

            var problemText = new List<string>();
            var oper = ' ';
            for (int l = endIndex - 1; l >= startIndex; l--)
            {
                var lineText = new List<char>();
                for (int h = 0; h < height; h++)
                {
                    var c = charArray[h][l];

                    if (c == '*' || c == '+')
                    {
                        oper = c;
                        c = ' ';
                    }
                    lineText.Add(c);
                }

                var s = new string([.. lineText]);

                if (!string.IsNullOrWhiteSpace(s))
                    problemText.Add(new string([.. lineText]));
            }

            problemText.Add(oper.ToString());
            textPerProblem.Add(string.Join('\n', problemText));
        }

        return textPerProblem;
    }

    private static List<int> GetStartOfProblems(char[][] charArray, int height, int length)
    {
        var problemLengthStartIndexes = new List<int> { 0 };

        for (int l = 0; l < length; l++)
        {
            var c = charArray[0][l];
            if (c != ' ') continue;

            var onlySpaces = true;

            for (int h = 0; h < height; h++)
            {
                var c2 = charArray[h][l];
                if (c2 == ' ')
                    continue;

                onlySpaces = false;
            }

            if (onlySpaces)
                problemLengthStartIndexes.Add(l);
        }

        return problemLengthStartIndexes;
    }

    public async Task SolvePart2(string inputFilePath)
    {
        var input = await File.ReadAllTextAsync(inputFilePath);

        var problems = ParsePart2(input);

        var total = Calculate(problems);

        Console.WriteLine($"Total of problems is: {total}");
    }
}
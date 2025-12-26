using Domain.Implementations;
using Domain.Interfaces;

IDay4 day4 = new Day4("Input/day_4.txt");
day4.RemoveAccessiblePaperRolls();

Console.WriteLine($"Amount of removed rolls: {day4.GetAmountOfRemovedRolls()}");

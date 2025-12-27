namespace Domain.Interfaces;

public interface IDay4 : IDay
{
    bool[][] ParseInput(string input);

    AccessiblePaperRoll IsAccessiblePaperRoll(bool[][] grid, int row, int column);

    int GetAccessiblePaperRollCount(string input);

    void RemoveAccessiblePaperRolls();
    int GetAmountOfRemovedRolls();
}

public enum AccessiblePaperRoll
{
    NotAPaperRoll,
    Accessible,
    Inaccessible
}
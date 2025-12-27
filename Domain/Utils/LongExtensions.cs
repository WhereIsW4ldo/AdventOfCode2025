namespace Domain.Utils;

public static class LongExtensions
{
    extension(long l)
    {
        public bool IsBetween(long lower, long upper)
          => lower <= l && l <= upper;
    }
}
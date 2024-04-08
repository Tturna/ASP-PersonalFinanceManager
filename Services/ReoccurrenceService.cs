using PersonalFinances.Models.Enums;

namespace PersonalFinances.Services;

public class ReoccurrenceService
{
    public DateOnly GetNextReoccurrenceDate(DateOnly date, Reoccurrence reoccurrence)
    {
        return reoccurrence switch
        {
            Reoccurrence.None => date,
            Reoccurrence.Daily => date.AddDays(1),
            Reoccurrence.Weekly => date.AddDays(7),
            Reoccurrence.Monthly => date.AddMonths(1),
            Reoccurrence.Annually => date.AddYears(1),
            _ => throw new ArgumentOutOfRangeException(nameof(reoccurrence), reoccurrence, null)
        };
    }
}
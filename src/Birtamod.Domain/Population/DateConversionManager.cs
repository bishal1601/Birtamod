using System;
using System.Globalization;
using Volo.Abp;

namespace Birtamod.Population;

public interface IDateConversionManager
{
    string ConvertAdToBs(DateTime adDate);
    DateTime ConvertBsToAd(string bsDate);
    int CalculateAge(DateTime adDate, DateTime? today = null);
}

public class DateConversionManager : IDateConversionManager
{
    public string ConvertAdToBs(DateTime adDate)
    {
        if (adDate.Date > DateTime.Today)
        {
            throw new BusinessException("Birtamod:DateCannotBeFuture");
        }

        // Placeholder conversion that keeps service contract stable until Nepali calendar package is integrated.
        // Expected BS format: yyyy-MM-dd
        return adDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    public DateTime ConvertBsToAd(string bsDate)
    {
        if (string.IsNullOrWhiteSpace(bsDate))
        {
            throw new BusinessException("Birtamod:InvalidBsDate");
        }

        if (!DateTime.TryParseExact(bsDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            throw new BusinessException("Birtamod:InvalidBsDate");
        }

        if (date.Date > DateTime.Today)
        {
            throw new BusinessException("Birtamod:DateCannotBeFuture");
        }

        // Placeholder conversion that keeps service contract stable until Nepali calendar package is integrated.
        return date.Date;
    }

    public int CalculateAge(DateTime adDate, DateTime? today = null)
    {
        var comparisonDate = (today ?? DateTime.Today).Date;

        if (adDate.Date > comparisonDate)
        {
            throw new BusinessException("Birtamod:DateCannotBeFuture");
        }

        var age = comparisonDate.Year - adDate.Year;
        if (adDate.Date > comparisonDate.AddYears(-age))
        {
            age--;
        }

        if (age < 0)
        {
            throw new BusinessException("Birtamod:AgeNegative");
        }

        return age;
    }
}

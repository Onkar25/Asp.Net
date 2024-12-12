namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly date)
    {
        var todayDate = DateOnly.FromDateTime(DateTime.Now);

        var age = todayDate.Year - date.Year;

        if(date < todayDate) 
        age--;

        return age;
    } 
}

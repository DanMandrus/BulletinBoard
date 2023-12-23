namespace BulletinBoard.BL.Helpers;

public static class AgeHelper
{
    public static int GetAge(DateTime birthdayDateTime)
    {
        var now = DateTime.UtcNow;
        int age = now.Year - birthdayDateTime.Year;
        if (birthdayDateTime > now.AddYears(-age)) age--;
        return age;
    }
}
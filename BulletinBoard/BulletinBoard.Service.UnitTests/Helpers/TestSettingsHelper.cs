using BulletinBoard.Service.Settings;

namespace BulletinBoard.Service.UnitTests.Helpers;

public static class TestSettingsHelper
{
    public static BulletinBoardSettings GetSettings()
    {
        return BulletinBoardSettingsReader.Read(ConfigurationHelper.GetConfiguration());
    }
}
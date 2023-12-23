namespace BulletinBoard.Service.Settings
{
    public static class BulletinBoardSettingsReader
    {
        public static BulletinBoardSettings Read(IConfiguration configuration)
        {
            return new BulletinBoardSettings()
            {
                ServiceUri = configuration.GetValue<Uri>("Uri"),
                BulletinBoardDbContextConnectionString = configuration.GetValue<string>("BulletinBoardDbContext"),
                IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri"),
                ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
                ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret"),
            };
        }
    }
}

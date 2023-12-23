namespace BulletinBoard.Service.Settings
{
    public class BulletinBoardSettings
    {
        public Uri ServiceUri { get; set; }
        public string BulletinBoardDbContextConnectionString { get; set; }
        public int MinimumUserAge { get; set; } = 18;
        public string IdentityServerUri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}

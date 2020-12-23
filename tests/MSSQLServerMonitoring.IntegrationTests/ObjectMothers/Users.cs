namespace MSSQLServerMonitoring.IntegrationTests.ObjectMothers
{
    public static class Users
    {
        public class MotherObject
        {
            public string Username;
        }

        public static readonly MotherObject UserVasya = new MotherObject
        {
            Username = "Вася"
        };
    }
}

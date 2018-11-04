namespace Tests.XPack.Security
{
	public class ShieldInformation
	{
		public static Credentials Admin => new Credentials { Username = "es_admin", Role = "admin" };

		public static Credentials[] AllUsers { get; } = new[] { Admin, User };
		public static Credentials User => new Credentials { Username = "es_user", Role = "user" };

		public class Credentials
		{
			public string Password => Username;
			public string Role { get; set; }
			public string Username { get; set; }
		}
	}
}

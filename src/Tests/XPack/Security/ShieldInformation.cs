namespace Tests.XPack.Security
{
	public class ShieldInformation
	{
		public class Credentials
		{
			public string Username { get; set; }
			public string Role { get; set; }
			public string Password => Username;
		}

		public static Credentials Admin => new Credentials { Username = "es_admin", Role = "admin" };
		public static Credentials User => new Credentials { Username = "es_user", Role = "user" };

		public static Credentials[] AllUsers { get; } = new[] { Admin, User };
	}
}

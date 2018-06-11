using Bogus;

namespace Tests.Framework.MockData
{
	public class SimpleGeoPoint
	{
		public double Lat { get; set; }
		public double Lon { get; set; }

		public static Faker<SimpleGeoPoint> Generator { get; } =
			new Faker<SimpleGeoPoint>()
				.UseSeed(TestClient.Configuration.Seed)
				.RuleFor(p => p.Lat, f => f.Address.Latitude())
				.RuleFor(p => p.Lon, f => f.Address.Longitude())
			.Clone();
	}
}

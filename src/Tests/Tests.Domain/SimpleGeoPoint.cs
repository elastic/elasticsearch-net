using Bogus;
using Tests.Configuration;

namespace Tests.Domain
{
	public class SimpleGeoPoint
	{
		public double Lat { get; set; }
		public double Lon { get; set; }

		public static Faker<SimpleGeoPoint> Generator { get; } =
			new Faker<SimpleGeoPoint>()
				.UseSeed(TestConfiguration.Instance.Seed)
				.RuleFor(p => p.Lat, f => f.Address.Latitude())
				.RuleFor(p => p.Lon, f => f.Address.Longitude())
			.Clone();
	}
}

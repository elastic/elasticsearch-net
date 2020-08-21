// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Bogus;
using Tests.Configuration;

namespace Tests.Domain
{
	public class SimpleGeoPoint
	{
		public static Faker<SimpleGeoPoint> Generator { get; } =
			new Faker<SimpleGeoPoint>()
				.UseSeed(TestConfiguration.Instance.Seed)
				.RuleFor(p => p.Lat, f => f.Address.Latitude())
				.RuleFor(p => p.Lon, f => f.Address.Longitude())
				.Clone();

		public double Lat { get; set; }
		public double Lon { get; set; }
	}
}

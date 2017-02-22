using Nest_5_2_0;

namespace Tests.Framework.MockData
{
	public class GeoIp
	{
		public string ContinentName { get; set; }

		public string CityName { get; set; }

		public string CountryIsoCode { get; set; }

		public string RegionName { get; set; }

		public GeoLocation Location { get; set; }
	}
}
// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Domain
{
	public class GeoIp
	{
		public string CityName { get; set; }
		public string ContinentName { get; set; }

		public string CountryIsoCode { get; set; }

		public GeoLocation Location { get; set; }

		public string RegionName { get; set; }
	}
}

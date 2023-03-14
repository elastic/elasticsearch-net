// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Serialization;
using VerifyXunit;

namespace Tests.Types;

[UsesVerify]
public class GeoLocationSerializationTests : SerializerTestBase
{
	[U]
	public async Task RoundTripSerialize_GeoLocation_WithText()
	{
		const string textValue = "text-value";

		var geoLocation = GeoLocation.Text(textValue);

		var result = await RoundTripAndVerifyJsonAsync(geoLocation);

		result.TryGetText(out var text).Should().BeTrue();

		text.Should().Be(textValue);
	}

	[U]
	public async Task RoundTripSerialize_GeoLocation_WithLatLonGeoLocation()
	{
		var geoLocation = GeoLocation.LatitudeLongitude(new() { Lat = 55.5, Lon = -77.7 });

		var result = await RoundTripAndVerifyJsonAsync(geoLocation);

		result.TryGetLatitudeLongitude(out var latLon).Should().BeTrue();

		latLon.Lat.Should().Be(55.5);
		latLon.Lon.Should().Be(-77.7);
	}

	[U]
	public async Task RoundTripSerialize_GeoLocation_WithGeoHashLocation()
	{
		const string hash = "dr5r9ydj2y73";

		var geoLocation = GeoLocation.Geohash(new() { Geohash = hash });

		var result = await RoundTripAndVerifyJsonAsync(geoLocation);

		result.TryGetGeohash(out var geoHash).Should().BeTrue();

		geoHash.Geohash.Should().Be(hash);
	}

	[U]
	public async Task RoundTripSerialize_GeoLocation_WithCoordinates()
	{
		var coordinates = new double[] { -74.1, 40.73 };

		var geoLocation = GeoLocation.Coordinates(coordinates);

		var result = await RoundTripAndVerifyJsonAsync(geoLocation);

		result.TryGetCoordinates(out var coords).Should().BeTrue();

		coords.Should().BeEquivalentTo(coordinates);
	}
}

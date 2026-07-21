// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Serialization;
using VerifyXunit;

namespace Tests.Types;

[UsesVerify]
public class GeoBoundsSerializationTests : SerializerTestBase
{
	[U]
	public async Task RoundTripSerialize_GeoBounds_WithCoordsGeoBounds()
	{
		var geoBounds = GeoBounds.Coordinates(new CoordsGeoBounds { Top = 40.73, Left = -74.1, Bottom = 40.01, Right = -71.12 });

		var result = await RoundTripAndVerifyJsonAsync(geoBounds);

		result.TryGetCoordinates(out var coordinates).Should().BeTrue();

		coordinates.Top.Should().Be(40.73);
		coordinates.Left.Should().Be(-74.1);
		coordinates.Bottom.Should().Be(40.01);
		coordinates.Right.Should().Be(-71.12);
	}

	[U]
	public async Task RoundTripSerialize_GeoBounds_WithWktGeoBounds()
	{
		const string wktData = "BBOX (-74.1, -71.12, 40.73, 40.01)";

		var geoBounds = GeoBounds.Wkt(new WktGeoBounds { Wkt = wktData });

		var result = await RoundTripAndVerifyJsonAsync(geoBounds);

		result.TryGetWkt(out var wkt).Should().BeTrue();

		wkt.Wkt.Should().Be(wktData);
	}

	[U]
	public async Task RoundTripSerialize_GeoBounds_WithTrbl()
	{
		var geoBounds = GeoBounds.TopRightBottomLeft(new TopRightBottomLeftGeoBounds
		{
			TopRight = GeoLocation.LatitudeLongitude(new LatLonGeoLocation() { Lat = 10, Lon = 20 }),
			BottomLeft = GeoLocation.LatitudeLongitude(new LatLonGeoLocation() { Lat = 30, Lon = 40 })
		});

		var result = await RoundTripAndVerifyJsonAsync(geoBounds);

		result.TryGetTopRightBottomLeft(out var trbl).Should().BeTrue();

		trbl.TopRight.TryGetLatitudeLongitude(out var trLatLon).Should().BeTrue();
		trbl.BottomLeft.TryGetLatitudeLongitude(out var blLatLon).Should().BeTrue();

		trLatLon.Lat.Should().Be(10);
		trLatLon.Lon.Should().Be(20);
		blLatLon.Lat.Should().Be(30);
		blLatLon.Lon.Should().Be(40);
	}

	[U]
	public async Task RoundTripSerialize_GeoBounds_WithTlbr()
	{
		var geoBounds = GeoBounds.TopLeftBottomRight(new TopLeftBottomRightGeoBounds
		{
			TopLeft = GeoLocation.LatitudeLongitude(new LatLonGeoLocation() { Lat = 10, Lon = 20 }),
			BottomRight = GeoLocation.LatitudeLongitude(new LatLonGeoLocation() { Lat = 30, Lon = 40 })
		});

		var result = await RoundTripAndVerifyJsonAsync(geoBounds);

		result.TryGetTopLeftBottomRight(out var tlbr).Should().BeTrue();

		tlbr.TopLeft.TryGetLatitudeLongitude(out var tlLatLon).Should().BeTrue();
		tlbr.BottomRight.TryGetLatitudeLongitude(out var brLatLon).Should().BeTrue();

		tlLatLon.Lat.Should().Be(10);
		tlLatLon.Lon.Should().Be(20);
		brLatLon.Lat.Should().Be(30);
		brLatLon.Lon.Should().Be(40);
	}
}

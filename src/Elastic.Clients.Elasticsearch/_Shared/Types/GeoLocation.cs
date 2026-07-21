// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(Json.GeoLocationConverter))]
public partial class GeoLocation
{
	public static bool IsValidLatitude(double latitude) => latitude >= -90 && latitude <= 90;

	public static bool IsValidLongitude(double longitude) => longitude >= -180 && longitude <= 180;
}

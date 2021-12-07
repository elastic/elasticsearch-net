// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch
{
	public sealed class GeoPoints : List<GeoPoint>
	{

	}

	public sealed class GeoPoint
	{
		private readonly double _latitude;
		private readonly double _longitude;
		private readonly string _value;

		public GeoPoint(GeoLocation geoLocation)
		{
			_latitude = geoLocation.Latitude;
			_longitude = geoLocation.Longitude;
		}

		public GeoPoint(double latitude, double longitude)
		{
			_latitude = latitude;
			_longitude = longitude;
		}

		public GeoPoint(string geoPointValue) => _value = geoPointValue;
	}

	internal sealed class GeoPointConverter
	{
		// TODO
	}

	public sealed class GeoLocation
	{
		public GeoLocation(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		public GeoLocation(IEnumerable<double> latLonArray)
		{
			var count = 0;
			foreach (var value in latLonArray)
			{
				count++;

				if (count > 2)
					throw new ArgumentException("Unexpected number of values.", nameof(latLonArray));

				if (count == 1)
					Latitude = value;
				else if (count == 2)
					Longitude = value;
			}
		}

		public double Latitude { get; }

		public double Longitude { get; }

		// TODO
	}

	internal sealed class GeoLocationConverter
	{
		// TODO
	}

	public sealed class GeoBoundingBox
	{

	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(GeoShapeFormatter<ICircleGeoShape>))]
	public interface ICircleGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		GeoCoordinate Coordinates { get; set; }

		[DataMember(Name ="radius")]
		string Radius { get; set; }
	}

	[JsonFormatter(typeof(GeoShapeFormatter<CircleGeoShape>))]
	public class CircleGeoShape : GeoShapeBase, ICircleGeoShape
	{
		internal CircleGeoShape() : base("circle") { }

		public CircleGeoShape(GeoCoordinate coordinates, string radius) : this()
		{
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
			if (radius == null) throw new ArgumentNullException(nameof(radius));
			if (radius.Length == 0) throw new ArgumentException("cannot be empty", nameof(radius));

			Radius = radius;
		}

		public GeoCoordinate Coordinates { get; set; }

		public string Radius { get; set; }
	}
}

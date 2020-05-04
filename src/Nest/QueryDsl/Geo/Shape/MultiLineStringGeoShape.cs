// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(GeoShapeFormatter<IMultiLineStringGeoShape>))]
	public interface IMultiLineStringGeoShape : IGeoShape
	{
		[DataMember(Name ="coordinates")]
		IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}

	[JsonFormatter(typeof(GeoShapeFormatter<MultiLineStringGeoShape>))]
	public class MultiLineStringGeoShape : GeoShapeBase, IMultiLineStringGeoShape
	{
		internal MultiLineStringGeoShape() : base("multilinestring") { }

		public MultiLineStringGeoShape(IEnumerable<IEnumerable<GeoCoordinate>> coordinates) : this() =>
			Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

		public IEnumerable<IEnumerable<GeoCoordinate>> Coordinates { get; set; }
	}
}

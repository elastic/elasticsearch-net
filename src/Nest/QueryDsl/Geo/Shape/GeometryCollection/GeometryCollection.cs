using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A geo shape representing a collection of <see cref="IGeoShape" /> geometries
	/// </summary>
	[ContractJsonConverter(typeof(GeoShapeConverter))]
	public interface IGeometryCollection
	{
		/// <summary>
		/// A collection of <see cref="IGeoShape" /> geometries
		/// </summary>
		[JsonProperty("geometries")]
		IEnumerable<IGeoShape> Geometries { get; set; }

		/// <summary>
		/// The type of geo shape
		/// </summary>
		[JsonProperty("type")]
		string Type { get; }
	}

	// TODO: IGeometryCollection should implement IGeoShape
	/// <inheritdoc cref="IGeometryCollection" />
	public class GeometryCollection : IGeometryCollection, IGeoShape
	{
		/// <inheritdoc />
		public IEnumerable<IGeoShape> Geometries { get; set; }

		/// <inheritdoc />
		[Obsolete("Removed in NEST 7.x. Use IgnoreUnmapped on IGeoShapeQuery")]
		public bool? IgnoreUnmapped { get; set; }

		/// <inheritdoc />
		public string Type => "geometrycollection";

		internal GeoShapeFormat Format { get; set; }

		/// <inheritdoc />
		string IGeoShape.Type => Type;
	}
}

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

	/// <inheritdoc cref="IGeometryCollection" />
	public class GeometryCollection : IGeometryCollection, IGeoShape
	{
		/// <inheritdoc />
		public IEnumerable<IGeoShape> Geometries { get; set; }

		/// <inheritdoc />
		public string Type => "geometrycollection";

		/// <inheritdoc />
		string IGeoShape.Type => Type;
	}
}

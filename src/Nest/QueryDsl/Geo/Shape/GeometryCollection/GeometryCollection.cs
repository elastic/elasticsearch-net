using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A geo shape representing a collection of <see cref="IGeoShape"/> geometries
	/// </summary>
	[ContractJsonConverter(typeof(GeoShapeConverter))]
	public interface IGeometryCollection
	{
		/// <summary>
		/// The type of geo shape
		/// </summary>
		[JsonProperty("type")]
		string Type { get; }

		/// <summary>
		/// A collection of <see cref="IGeoShape"/> geometries
		/// </summary>
		[JsonProperty("geometries")]
		IEnumerable<IGeoShape> Geometries { get; set; }
	}

	// TODO: IGeometryCollection should implement IGeoShape
	/// <inheritdoc cref="IGeometryCollection"/>
	public class GeometryCollection : IGeometryCollection, IGeoShape
	{
		/// <inheritdoc />
		public string Type => "geometrycollection";

		/// <inheritdoc />
		string IGeoShape.Type => this.Type;

		/// <inheritdoc />
		public bool? IgnoreUnmapped { get; set; }

		/// <inheritdoc />
		public IEnumerable<IGeoShape> Geometries { get; set; }
	}
}

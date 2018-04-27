using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A geo shape representing a collection of <see cref="IGeoShape"/> geometries
	/// </summary>
	public interface IGeometryCollection : IGeoShape
	{
		/// <summary>
		/// A collection of <see cref="IGeoShape"/> geometries
		/// </summary>
		[JsonProperty("geometries")]
		IEnumerable<IGeoShape> Geometries { get; set; }
	}

	/// <inheritdoc cref="IGeometryCollection"/>
	public class GeometryCollection : GeoShapeBase, IGeometryCollection
	{
		public GeometryCollection(IEnumerable<IGeoShape> geometries) : this() =>
			this.Geometries = geometries ?? throw new ArgumentNullException(nameof(geometries));

		internal GeometryCollection() : base("geometrycollection") { }

		/// <inheritdoc />
		public IEnumerable<IGeoShape> Geometries { get; set; }
	}
}

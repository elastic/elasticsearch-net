using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// A geo shape representing a collection of <see cref="IGeoShape" /> geometries
	/// </summary>
	[JsonFormatter(typeof(GeoShapeFormatter<IGeometryCollection>))]
	public interface IGeometryCollection : IGeoShape
	{
		/// <summary>
		/// A collection of <see cref="IGeoShape" /> geometries
		/// </summary>
		[DataMember(Name = "geometries")]
		IEnumerable<IGeoShape> Geometries { get; set; }
	}

	/// <inheritdoc cref="IGeometryCollection" />
	[JsonFormatter(typeof(GeoShapeFormatter<GeometryCollection>))]
	public class GeometryCollection : GeoShapeBase, IGeometryCollection
	{
		public GeometryCollection(IEnumerable<IGeoShape> geometries) : this() =>
			Geometries = geometries ?? throw new ArgumentNullException(nameof(geometries));

		internal GeometryCollection() : base("geometrycollection") { }

		/// <inheritdoc />
		public IEnumerable<IGeoShape> Geometries { get; set; }
	}
}

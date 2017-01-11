using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGeometryCollection
	{
		[JsonProperty("type")]
		string Type { get; }

		[JsonProperty("geometries")]
		IEnumerable<IGeoShape> Geometries { get; set; }
	}

	public class GeometryCollection : IGeometryCollection
	{
		public string Type => "geometrycollection";

		public IEnumerable<IGeoShape> Geometries { get; set; }
	}
}

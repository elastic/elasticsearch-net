using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
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

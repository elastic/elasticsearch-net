using Newtonsoft.Json;

namespace Nest
{
	public interface IGeoShape
	{
		[JsonProperty("type")]
		string Type { get; }
	}

	public abstract class GeoShape
	{
		public GeoShape(string type)
		{
			this.Type = type;
		}

		public string Type { get; protected set; }
	}
}

using Newtonsoft.Json;

namespace Nest
{
	public interface IGeoShape
	{
		[JsonProperty("type")]
		string Type { get; }

		[JsonProperty("ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }
	}

	public abstract class GeoShapeBase : IGeoShape
	{
	    protected GeoShapeBase(string type)
		{
			this.Type = type;
		}

		public string Type { get; protected set; }

		/// <summary>
		/// Will ignore an unmapped field and will not match any documents for this query.
		/// This can be useful when querying multiple indexes which might have different mappings.
		/// </summary>
		[JsonProperty("ignore_unmapped")]
		public bool? IgnoreUnmapped { get; set; }
	}
}

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IGeoLocationSuggestContext : ISuggestContext
	{
		[JsonProperty("precision")]
		IEnumerable<string> Precision { get; set; }

		[JsonProperty("neighbors")]
		bool Neighbors { get; set; }

		[JsonProperty("default")]
		GeoLocation Default { get; set; }
	}

	[JsonObject]
	public class GeoLocationSuggestContext : SuggestContextBase, IGeoLocationSuggestContext
	{
		public override string Type => "geo";

		public IEnumerable<string> Precision { get; set; }

		public bool Neighbors { get; set; }

		public GeoLocation Default { get; set; }
	}

	public class GeoLocationSuggestContextDescriptor<T> : SuggestContextDescriptorBase<GeoLocationSuggestContextDescriptor<T>, IGeoLocationSuggestContext, T>, IGeoLocationSuggestContext
		where T : class
	{
		protected override string Type => "geo";
		IEnumerable<string> IGeoLocationSuggestContext.Precision { get; set; }
		bool IGeoLocationSuggestContext.Neighbors { get; set; }
		GeoLocation IGeoLocationSuggestContext.Default { get; set; }

		public GeoLocationSuggestContextDescriptor<T> Precision(params string[] precisions) => Assign(a => a.Precision = precisions);

		public GeoLocationSuggestContextDescriptor<T> Neighbors(bool neighbors = true) => Assign(a => a.Neighbors = neighbors);

		public GeoLocationSuggestContextDescriptor<T> Default(GeoLocation geoPoint) => Assign(a => a.Default = geoPoint);

	}
}

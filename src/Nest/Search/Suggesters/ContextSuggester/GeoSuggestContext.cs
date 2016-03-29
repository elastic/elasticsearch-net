using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public interface IGeoSuggestContext : ISuggestContext
	{
		[JsonProperty("precision")]
		IEnumerable<string> Precision { get; set; }

		[JsonProperty("neighbors")]
		bool Neighbors { get; set; }
	}

	[JsonObject]
	public class GeoSuggestContext : SuggestContextBase, IGeoSuggestContext
	{
		public override string Type => "geo";

		public IEnumerable<string> Precision { get; set; }

		public bool Neighbors { get; set; }

	}

	public class GeoSuggestContextDescriptor<T> : SuggestContextDescriptorBase<GeoSuggestContextDescriptor<T>, IGeoSuggestContext, T>, IGeoSuggestContext
		where T : class
	{
		protected override string Type => "geo";
		IEnumerable<string> IGeoSuggestContext.Precision { get; set; }
		bool IGeoSuggestContext.Neighbors { get; set; }

		public GeoSuggestContextDescriptor<T> Precision(params string[] precisions) => Assign(a => a.Precision = precisions);

		public GeoSuggestContextDescriptor<T> Neighbors(bool neighbors = true) => Assign(a => a.Neighbors = neighbors);
	}
}

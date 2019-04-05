using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IndicesOptions>))]
	public interface IIndicesOptions
	{
		[JsonProperty("allow_no_indices")]
		bool? AllowNoIndices { get; set; }

		[JsonProperty("expand_wildcards")]
		[JsonConverter(typeof(StringEnumConverter))]
		ExpandWildcards? ExpandWildcards { get; set; }

		[JsonProperty("ignore_unavailable")]
		bool? IgnoreUnavailable { get; set; }
	}

	[JsonObject]
	public class IndicesOptions : IIndicesOptions
	{
		public bool? AllowNoIndices { get; set; }
		public ExpandWildcards? ExpandWildcards { get; set; }

		public bool? IgnoreUnavailable { get; set; }
	}

	public class IndicesOptionsDescriptor : DescriptorBase<IndicesOptionsDescriptor, IIndicesOptions>, IIndicesOptions
	{
		bool? IIndicesOptions.AllowNoIndices { get; set; }
		ExpandWildcards? IIndicesOptions.ExpandWildcards { get; set; }
		bool? IIndicesOptions.IgnoreUnavailable { get; set; }

		public IndicesOptionsDescriptor ExpandWildcards(ExpandWildcards? expandWildcards) =>
			Assign(expandWildcards, (a, v) => a.ExpandWildcards = v);

		public IndicesOptionsDescriptor IgnoreUnavailable(bool? ignoreUnavailable = true) =>
			Assign(ignoreUnavailable, (a, v) => a.IgnoreUnavailable = v);

		public IndicesOptionsDescriptor AllowNoIndices(bool? allowNoIndices = true) =>
			Assign(allowNoIndices, (a, v) => a.AllowNoIndices = v);
	}
}

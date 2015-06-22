using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class MultiTermVectorDocument
	{
		[JsonProperty("_index")]
		public IndexNameMarker Index { get; set; }
		[JsonProperty("_type")]
		public TypeNameMarker Type { get; set; }
		[JsonProperty("_id")]
		public string Id { get; set; }
		[JsonProperty("doc")]
		public object Document { get; set; }
		[JsonProperty("fields")]
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
		[JsonProperty("offsets")]
		public bool? Offsets { get; set; }
		[JsonProperty("payloads")]
		public bool? Payloads { get; set; }
		[JsonProperty("positions")]
		public bool? Positions { get; set; }
		[JsonProperty("term_statistics")]
		public bool? TermStatistics { get; set; }
		[JsonProperty("field_statistics")]
		public bool? FieldStatistics { get; set; }
	}
}
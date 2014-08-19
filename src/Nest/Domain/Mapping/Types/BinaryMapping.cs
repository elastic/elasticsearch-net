using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class BinaryMapping 
		: CoreMappingBase, IElasticType
	{

		public PropertyNameMarker Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return new TypeNameMarker { Name = "binary" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("doc_values")]
		public bool? DocValues { get; set; }

		[JsonProperty("store")]
		public bool? Store { get; set; }

		[JsonProperty("compress")]
		public bool? Compress { get; set; }

		[JsonProperty("compress_threshold")]
		public string CompressThreshold { get; set; }

		[JsonProperty("copy_to")]
		public IEnumerable<PropertyPathMarker> CopyTo { get; set; }
	}
}
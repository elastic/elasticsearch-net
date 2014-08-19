using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class BinaryMapping : MultiFieldMapping, IElasticType, IElasticCoreType
	{
		public BinaryMapping()
			: base("binary")
		{
		}

		/// <summary>
		/// The name of the field that will be stored in the index. Defaults to the property/field name.
		/// </summary>
		[JsonProperty("index_name")]
		public string IndexName { get; set; }

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
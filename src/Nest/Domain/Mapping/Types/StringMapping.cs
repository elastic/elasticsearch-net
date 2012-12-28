using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public class StringMapping : Attribute, IElasticType, IElasticCoreType
	{
		[JsonIgnore]
		public string Name { get; set; }

		[JsonProperty("type")]
		public virtual string Type { get { return "string"; } }

		/// <summary>
		/// The name of the field that will be stored in the index. Defaults to the property/field name.
		/// </summary>
		[JsonProperty("index_name")]
		public string IndexName { get; set; }

		[JsonProperty("store"), JsonConverter(typeof(YesNoBoolConverter))]
		public bool? Store { get; set; }

		[JsonProperty("index"), JsonConverter(typeof(StringEnumConverter))]
		public FieldIndexOption? Index { get; set; }

		[JsonProperty("term_vector"), JsonConverter(typeof(StringEnumConverter))]
		public TermVectorOption? TermVector { get; set; }

		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("null_value")]
		public string NullValue { get; set; }

		[JsonProperty("omit_norms")]
		public string OmitNorms { get; set; }

		[JsonProperty("omit_term_freq_and_positions")]
		public bool? OmitTermFrequencyAndPositions { get; set; }

		[JsonProperty("index_options"), JsonConverter(typeof(StringEnumConverter))]
		public IndexOptions? IndexOptons { get; set; }

		[JsonProperty("index_analyzer")]
		public string IndexAnalyzer { get; set; }

		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("position_offset_gap	")]
		public int? PositionOffsetGap { get; set; }

	}
}
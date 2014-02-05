using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

namespace Nest
{
	public class StringMapping : IElasticType, IElasticCoreType
	{
		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return new TypeNameMarker { Name = "string" }; } }

		[JsonProperty(PropertyName = "name")]
		public PropertyNameMarker Name { get; set; }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		/// <summary>
		/// The name of the field that will be stored in the index. Defaults to the property/field name.
		/// </summary>
		[JsonProperty("index_name")]
		public string IndexName { get; set; }

		[JsonProperty("analyzer")]
		public string Analyzer { get; set; }

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
		public bool? OmitNorms { get; set; }

		[JsonProperty("omit_term_freq_and_positions")]
		public bool? OmitTermFrequencyAndPositions { get; set; }

		[JsonProperty("index_options"), JsonConverter(typeof(StringEnumConverter))]
		public IndexOptions? IndexOptions { get; set; }

		[JsonProperty("index_analyzer")]
		public string IndexAnalyzer { get; set; }

		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("position_offset_gap")]
		public int? PositionOffsetGap { get; set; }

	}
}
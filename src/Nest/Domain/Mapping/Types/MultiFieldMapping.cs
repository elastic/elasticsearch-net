using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MultiFieldMapping : IElasticType
	{	
		public MultiFieldMapping()
		{
			this.Fields = new Dictionary<PropertyNameMarker, IElasticCoreType>();
		}

		public PropertyNameMarker Name { get; set; }

		private TypeNameMarker _typeOverride;

		[JsonProperty("type")]
		public virtual TypeNameMarker Type
		{
			get { return _typeOverride ?? new TypeNameMarker { Name = "multi_field" }; }
			set { _typeOverride = value; }
		}

        [JsonProperty("path")]
        public string Path { get; set; }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("fields"), JsonConverter(typeof(ElasticCoreTypeConverter))]
		public IDictionary<PropertyNameMarker, IElasticCoreType> Fields { get; set; }

		/// <summary>
		/// The name of the field that will be stored in the index. Defaults to the property/field name.
		/// </summary>
		[JsonProperty("index_name")]
		public string IndexName { get; set; }

		[JsonProperty("analyzer")]
		public string Analyzer { get; set; }

		[JsonProperty("store")]
		public bool? Store { get; set; }

		[JsonProperty("index"), JsonConverter(typeof(StringEnumConverter))]
		public FieldIndexOption? Index { get; set; }

		[JsonProperty("term_vector"), JsonConverter(typeof(StringEnumConverter))]
		public TermVectorOption? TermVector { get; set; }

		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("null_value")]
		public string NullValue { get; set; }

		[JsonProperty("norms")]
		public NormsMapping Norms { get; set; }

		[JsonProperty("omit_norms")]
		public bool? OmitNorms { get; set; }

		[JsonProperty("index_options"), JsonConverter(typeof(StringEnumConverter))]
		public IndexOptions? IndexOptions { get; set; }

		[JsonProperty("index_analyzer")]
		public string IndexAnalyzer { get; set; }

		[JsonProperty("ignore_above")]
		public int? IgnoreAbove { get; set; }

		[JsonProperty("search_analyzer")]
		public string SearchAnalyzer { get; set; }

		[JsonProperty("doc_values")]
		public bool? DocValues { get; set; }

		[JsonProperty("position_offset_gap")]
		public int? PositionOffsetGap { get; set; }

		[JsonProperty("precision_step")]
		public int? PrecisionStep { get; set; }

		[JsonProperty("ignore_malformed")]
		public bool? IgnoreMalformed { get; set; }

		[JsonProperty("coerce")]
		public bool? Coerce { get; set; }

		[JsonProperty("compress")]
		public bool? Compress { get; set; }

		[JsonProperty("compress_threshold")]
		public string CompressThreshold { get; set; }

		[JsonProperty("copy_to")]
		public IEnumerable<PropertyPathMarker> CopyTo { get; set; }
	}

    public class MultiFieldMappingPath
    {
        public MultiFieldMappingPath()
        {

        }

        public MultiFieldMappingPath(string customValue)
        {
            Value = customValue;
        }

        public string Value { get; private set; }

        public static MultiFieldMappingPath Full
        {
            get
            {
                return new MultiFieldMappingPath("full");
            }
        }

        public static MultiFieldMappingPath JustName
        {
            get
            {
                return new MultiFieldMappingPath("just_name");
            }
        }
    }
}
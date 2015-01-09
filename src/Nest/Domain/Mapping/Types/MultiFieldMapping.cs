using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MultiFieldMapping : IElasticType
	{
		private readonly TypeNameMarker _defaultType;

		public MultiFieldMapping() 
		{
			this.Fields = new Dictionary<PropertyNameMarker, IElasticCoreType>();
			_defaultType = "multi_field";
		}

		protected MultiFieldMapping(TypeNameMarker defaultType) : this()
		{
			_defaultType = defaultType;
		}

		public PropertyNameMarker Name { get; set; }

		private TypeNameMarker _typeOverride;

		[JsonProperty("type")]
		public virtual TypeNameMarker Type
		{
			get { return _typeOverride ?? _defaultType; }
			set { _typeOverride = value; }
		}

        [JsonProperty("path")]
        public string Path { get; set; }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("fields", DefaultValueHandling = DefaultValueHandling.Ignore), JsonConverter(typeof(ElasticCoreTypeConverter))]
		public IDictionary<PropertyNameMarker, IElasticCoreType> Fields { get; set; }
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
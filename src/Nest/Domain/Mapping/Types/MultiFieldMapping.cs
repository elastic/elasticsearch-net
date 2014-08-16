using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MultiFieldMapping : IElasticType
	{
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

		[JsonProperty("fields", DefaultValueHandling = DefaultValueHandling.Ignore), JsonConverter(typeof(ElasticCoreTypeConverter))]
		public IDictionary<PropertyNameMarker, IElasticCoreType> Fields { get; set; }


		public MultiFieldMapping()
		{
			this.Fields = new Dictionary<PropertyNameMarker, IElasticCoreType>();
		}
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
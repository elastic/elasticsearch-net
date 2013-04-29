using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Nest.Resolvers;

namespace Nest
{
	public class MultiFieldMapping : IElasticType
	{
		[JsonIgnore]
		public TypeNameMarker TypeNameMarker { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return new TypeNameMarker { Name = "multi_field" }; } }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("fields"), JsonConverter(typeof(ElasticCoreTypeConverter))]
		public IDictionary<string, IElasticCoreType> Fields { get; set; }


		public MultiFieldMapping()
		{
			this.Fields = new Dictionary<string, IElasticCoreType>();
		}
	}
}
using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Nest.Resolvers;

namespace Nest
{
	public class MultiFieldMapping : IElasticType
	{
		[JsonProperty(PropertyName = "name")]
		public PropertyNameMarker Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return new TypeNameMarker { Name = "multi_field" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("fields"), JsonConverter(typeof(ElasticCoreTypeConverter))]
		public IDictionary<PropertyNameMarker, IElasticCoreType> Fields { get; set; }


		public MultiFieldMapping()
		{
			this.Fields = new Dictionary<PropertyNameMarker, IElasticCoreType>();
		}
	}
}
using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class ObjectMapping : IElasticType
	{
		public PropertyNameMarker Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type
		{
			get { return new TypeNameMarker { Name = "object" }; }
		}

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("dynamic")]
		[JsonConverter(typeof(DynamicMappingOptionConverter))]
		public DynamicMappingOption? Dynamic { get; set; }

		[JsonProperty("enabled")]
		public bool? Enabled { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("path")]
		public string Path { get; set; }

		[JsonProperty("properties", TypeNameHandling = TypeNameHandling.None)]
		[JsonConverter(typeof(ElasticTypesConverter))]
		public IDictionary<PropertyNameMarker, IElasticType> Properties { get; set; }

	}
}
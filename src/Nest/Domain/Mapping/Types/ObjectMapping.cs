using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Nest.Resolvers;

namespace Nest
{
	public class ObjectMapping :  IElasticType
	{
		[JsonIgnore]
		public TypeNameMarker TypeNameMarker { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type {
			get { return new TypeNameMarker {Name = "object"}; }
		}

		[JsonProperty("dynamic")]
		public bool? Dynamic { get; set; }

		[JsonProperty("enabled")]
		public bool? Enabled { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("path")]
		public string Path { get; set; }

		[JsonProperty("properties", TypeNameHandling = TypeNameHandling.None)]
		[JsonConverter(typeof(ElasticTypesConverter))]
		public IDictionary<string, IElasticType> Properties { get; set; }

	}
}
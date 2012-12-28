using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public class ObjectMapping : Attribute, IElasticType
	{
		[JsonIgnore]
		public string Name { get; set; }

		[JsonProperty("type")]
		public virtual string Type { get { return "object"; } }

		//TODO START: Still expose this on (Root)ObjectMappingDescriptor

		[JsonProperty("dynamic")]
		public bool? Dynamic { get; set; }

		[JsonProperty("enabled")]
		public bool? Enabled { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("path")]
		public string Path { get; set; }

		//TODO END


		

		[JsonProperty("properties")]
		public IDictionary<string, IElasticType> Properties { get; set; }


		public ObjectMapping()
		{
			this.Properties = new Dictionary<string, IElasticType>();
		}
	}
}
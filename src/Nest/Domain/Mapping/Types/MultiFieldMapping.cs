using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public class MultiFieldMapping : Attribute, IElasticType
	{
		[JsonIgnore]
		public string Name { get; set; }

		[JsonProperty("type")]
		public virtual string Type { get { return "multi_field"; } }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

		[JsonProperty("fields")]
		public IDictionary<string, IElasticCoreType> Fields { get; set; }


		public MultiFieldMapping()
		{
			this.Fields = new Dictionary<string, IElasticCoreType>();
		}
	}
}
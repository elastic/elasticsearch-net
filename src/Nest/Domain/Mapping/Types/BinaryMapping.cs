using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public class BinaryMapping : IElasticType, IElasticCoreType
	{
		[JsonIgnore]
		public string Name { get; set; }

		[JsonProperty("type")]
		public virtual string Type { get { return "binary"; } }

    [JsonProperty("similarity")]
    public string Similarity { get; set; }

		/// <summary>
		/// The name of the field that will be stored in the index. Defaults to the property/field name.
		/// </summary>
		[JsonProperty("index_name")]
		public string IndexName { get; set; }
	}
}
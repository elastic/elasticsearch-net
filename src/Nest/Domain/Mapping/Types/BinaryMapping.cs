using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

namespace Nest
{
	public class BinaryMapping : IElasticType, IElasticCoreType
	{

		[JsonProperty(PropertyName = "name")]
		public PropertyNameMarker Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return new TypeNameMarker { Name = "binary" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		/// <summary>
		/// The name of the field that will be stored in the index. Defaults to the property/field name.
		/// </summary>
		[JsonProperty("index_name")]
		public string IndexName { get; set; }
	}
}
using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

namespace Nest
{
	public class AttachmentMapping : IElasticType
	{
		[JsonProperty(PropertyName = "name")]
		public PropertyNameMarker Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return new TypeNameMarker { Name = "attachment" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("fields"), JsonConverter(typeof(ElasticCoreTypeConverter))]
		public IDictionary<PropertyNameMarker, IElasticCoreType> Fields { get; set; }

		public AttachmentMapping()
		{
			this.Fields = new Dictionary<PropertyNameMarker, IElasticCoreType>();
		}

	}
}
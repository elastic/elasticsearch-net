using System.Collections.Generic;
using System.Runtime.InteropServices;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Nest.Resolvers;

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
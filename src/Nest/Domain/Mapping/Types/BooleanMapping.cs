using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

namespace Nest
{
	public class BooleanMapping : IElasticType, IElasticCoreType
	{
		[JsonIgnore]
		public TypeNameMarker TypeNameMarker { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return new TypeNameMarker { Name = "boolean" }; } }

		/// <summary>
		/// The name of the field that will be stored in the index. Defaults to the property/field name.
		/// </summary>
		[JsonProperty("index_name")]
		public string IndexName { get; set; }

		[JsonProperty("store"), JsonConverter(typeof(YesNoBoolConverter))]
		public bool? Store { get; set; }

		[JsonProperty("index"), JsonConverter(typeof(StringEnumConverter))]
		public NonStringIndexOption? Index { get; set; }

		[JsonProperty("boost")]
		public double? Boost { get; set; }

		[JsonProperty("null_value")]
		public bool? NullValue { get; set; }

		[JsonProperty("include_in_all")]
		public bool? IncludeInAll { get; set; }

	}
}
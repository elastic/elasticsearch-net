using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	//	[JsonConverter(typeof(TemplateMappingConverter))]
	//	[JsonObject(MemberSerialization.OptIn)]
	public class WarmerMappingWrapper
	{

		[JsonProperty("types")]
		public IEnumerable<string> Types { get; set; }

		[JsonProperty("source")]
		public WarmerMapping Source { get; set; }

		internal WarmerMapping Unwrap(string name)
		{
			Source.Name = name;
			Source.Types = Types;
			return Source;
		}
	}
}
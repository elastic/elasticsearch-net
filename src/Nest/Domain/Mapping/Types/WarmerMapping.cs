using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	//	[JsonConverter(typeof(TemplateMappingConverter))]
	[JsonObject(MemberSerialization.OptIn)]
	public class WarmerMapping
	{

		public WarmerMapping()
		{
		}

		public IEnumerable<string> Types { get; internal set; }
		public string Name { get; internal set; }
	}
}
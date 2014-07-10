using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{

	[JsonObject]
	[JsonConverter(typeof(WarmerMappingConverter))]
	public class WarmerMapping
	{

		public string Name { get; internal set; }

		public IEnumerable<TypeNameMarker> Types { get; internal set; }

		public ISearchRequest Source { get; internal set; }

	}
}
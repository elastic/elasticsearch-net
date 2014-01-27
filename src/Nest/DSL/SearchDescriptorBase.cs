using System;
using System.Collections.Generic;
using Nest.Resolvers;

namespace Nest
{
	public abstract class SearchDescriptorBase
	{
		internal abstract Type _ClrType { get; }
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		internal IEnumerable<TypeNameMarker> _Types { get; set; }
		internal string _Routing { get; set; }
		internal SearchType? _SearchType { get; set; }
		internal bool? _AllIndices { get; set; }
		internal bool? _AllTypes { get; set; }

		//[JsonProperty(PropertyName = "preference")]
		internal string _Preference { get; set; }

		internal Func<dynamic, Hit<dynamic>, Type> _ConcreteTypeSelector;
	}
}
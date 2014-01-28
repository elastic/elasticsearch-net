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
		internal abstract string _Preference { get; }
		internal abstract string _Routing { get; }
		internal abstract SearchTypeOptions? _SearchType { get;  }
		internal bool? _AllIndices { get; set; }
		internal bool? _AllTypes { get; set; }

		internal Func<dynamic, Hit<dynamic>, Type> _ConcreteTypeSelector;
	}
}
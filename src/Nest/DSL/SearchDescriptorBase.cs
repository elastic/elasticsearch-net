using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	public abstract class SearchDescriptorBase
	{
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		internal IEnumerable<TypeNameMarker> _Types { get; set; }
		
		internal bool? _AllIndices { get; set; }
		internal bool? _AllTypes { get; set; }

	}
}
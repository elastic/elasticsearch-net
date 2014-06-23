using System;
using System.Collections.Generic;
using Nest.Resolvers;

namespace Nest
{
	public interface ISimpleGetDescriptor
	{
		IndexNameMarker _Index { get; set; }
		TypeNameMarker _Type { get; set; }
		string _Id { get; set; }
		IList<PropertyPathMarker> _Fields { get; set; }
		string _Routing { get; set; }
		Type _ClrType { get; }
	}
}
using System;
using System.Collections.Generic;
using Nest.Resolvers;

namespace Nest
{
	public abstract class BaseSimpleGetDescriptor
	{

		internal virtual string _Index { get; set; }
		internal virtual TypeNameMarker _Type { get; set; }
		internal virtual string _Id { get; set; }
		internal virtual IList<string> _Fields { get; set; }
		internal virtual string _Routing { get; set; }

		internal virtual Type _ClrType { get; set; }
	}
}
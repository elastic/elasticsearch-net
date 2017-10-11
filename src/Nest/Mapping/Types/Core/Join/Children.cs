using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonConverter(typeof(ChildrenJsonConverter))]
	public class Children : List<TypeName>
	{
		public static implicit operator Children(TypeName[] types)
		{
			if (types == null) return null;
			var children = new Children();
			children.AddRange(types);
			return children;
		}

		public static implicit operator Children(Types types)
		{
			return types?.Match(a => null, m =>
			{
				var children = new Children();
				children.AddRange(m.Types);
				return children;
			});
		}

		public static implicit operator Children(TypeName type) => type == null ? null : new Children { type };
		public static implicit operator Children(Type type) => type == null ? null : new Children { type };
		public static implicit operator Children(string type) => type == null ? null : new Children { type };
	}

}

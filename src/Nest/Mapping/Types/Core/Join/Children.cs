using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonConverter(typeof(ChildrenJsonConverter))]
	public class Children : List<RelationName>
	{
		public Children() { }

		public Children(RelationName child, params RelationName[] moreChildren)
		{
			if (child != null) this.Add(child);
			if (moreChildren == null || moreChildren.Length == 0) return;
			this.AddRange(moreChildren);
		}

		public static implicit operator Children(RelationName child)
		{
			if (child == null) return null;
			var children = new Children{child};
			return children;
		}

		public static implicit operator Children(Type type) => type == null ? null : new Children { type };
		public static implicit operator Children(string type) => type == null ? null : new Children { type };
	}

}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(ChildrenFormatter))]
	public class Children : List<RelationName>
	{
		public Children() { }

		public Children(RelationName child, params RelationName[] moreChildren)
		{
			if (child != null) Add(child);
			if (moreChildren == null || moreChildren.Length == 0) return;

			AddRange(moreChildren);
		}

		public static implicit operator Children(RelationName child)
		{
			if (child == null) return null;

			var children = new Children { child };
			return children;
		}

		public static implicit operator Children(Type type) => type == null ? null : new Children { type };

		public static implicit operator Children(string type) => type == null ? null : new Children { type };
	}
}

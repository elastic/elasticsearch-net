// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;

#pragma warning disable 3015

namespace Nest
{
	public class JoinAttribute : ElasticsearchPropertyAttributeBase, IJoinProperty
	{
		public JoinAttribute(RelationName parent, RelationName child, params RelationName[] moreChildren) : base(FieldType.Join) =>
			Self.Relations = new Relations { { parent, new Children(child, moreChildren) } };

		public JoinAttribute(Type parent, Type child, params Type[] moreChildren) : base(FieldType.Join)
		{
			var children = new Children { child };
			children.AddRange(moreChildren.Select(c => (RelationName)c));
			Self.Relations = new Relations { { parent, children } };
		}

		public JoinAttribute(string parent, string child, params string[] moreChildren) : base(FieldType.Join)
		{
			var children = new Children { child };
			children.AddRange(moreChildren.Select(c => (RelationName)c));
			Self.Relations = new Relations { { parent, children } };
		}

		IRelations IJoinProperty.Relations { get; set; }

		private IJoinProperty Self => this;
	}
}

using System;
using System.Linq;

#pragma warning disable 3015

namespace Nest
{
	public class JoinAttribute : ElasticsearchPropertyAttributeBase, IJoinProperty
	{
		public JoinAttribute(RelationName parent, RelationName child, params RelationName[] moreChildren) : base(FieldType.Join)
		{
			Self.Relations = new Relations {{ parent, new Children(child, moreChildren) }};
		}

		public JoinAttribute(Type parent, Type child, params Type[] moreChildren) : base(FieldType.Join)
		{
			var children = new Children { child };
			children.AddRange(moreChildren.Select(c => (RelationName) c));
			Self.Relations = new Relations {{ parent, children}};
		}

		public JoinAttribute(string parent, string child, params string[] moreChildren) : base(FieldType.Join)
		{
			var children = new Children{ child };
			children.AddRange(moreChildren.Select(c => (RelationName) c));
			Self.Relations = new Relations {{ parent, children}};
		}

		private IJoinProperty Self => this;

		IRelations IJoinProperty.Relations { get; set; }
	}
}

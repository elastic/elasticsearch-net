using System;
using System.Linq;

#pragma warning disable 3015

namespace Nest
{
	public class JoinAttribute : ElasticsearchPropertyAttributeBase, IJoinProperty
	{
		public JoinAttribute(TypeName parent, TypeName child, params TypeName[] moreChildren) : base(FieldType.Join)
		{
			Self.Relations = new Relations {{ parent, child.And(moreChildren) }};
		}

		public JoinAttribute(Type parent, Type child, params Type[] moreChildren) : base(FieldType.Join)
		{
			var children = new Children { child };
			children.AddRange(moreChildren.Select(c => (TypeName) c));
			Self.Relations = new Relations {{ parent, children}};
		}

		public JoinAttribute(string parent, string child, params string[] moreChildren) : base(FieldType.Join)
		{
			var children = new Children{ child };
			children.AddRange(moreChildren.Select(c => (TypeName) c));
			Self.Relations = new Relations {{ parent, children}};
		}

		private IJoinProperty Self => this;

		IRelations IJoinProperty.Relations { get; set; }
	}
}

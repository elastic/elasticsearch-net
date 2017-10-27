using System;
using System;
using System.CodeDom;
using Newtonsoft.Json;

namespace Nest
{
	/// This does not extend from Union because its intended to be used on folk's _source's
	/// And the union serialization will bleed into their own JSON.NET serializer should they
	/// have one configured and then it will blow up because their contractresolver do not extend ours
	/// from which we can snoop ConnectionSettings.
	///
	/// ContractJsonResolverAttribute works as well but I rather keep this class contained as much as possible
	///
	/// </summary>
	[ContractJsonConverter(typeof(JoinFieldJsonConverter))]
	public class JoinField
	{
		internal readonly Parent _parent;
		internal readonly Child _child;
		internal readonly int _tag;

		public JoinField(Parent parentName)
		{
			this._parent = parentName;
			this._tag = 0;
		}

		public JoinField(Child child)
		{
			this._child = child;
			this._tag = 1;
		}

		public static JoinField Root<TParent>() => new Parent(typeof(TParent));
		public static JoinField Root(RelationName parent) => new Parent(parent);

		public static JoinField Link(RelationName child, Id parentId) => new Child(child, parentId);
		public static JoinField Link<TChild, TParentDocument>(TParentDocument parent) where TParentDocument : class =>
			new Child(typeof(TChild), Id.From<TParentDocument>(parent));
		public static JoinField Link<TChild>(Id parentId) => new Child(typeof(TChild), parentId);

		public static implicit operator JoinField(Parent parent) => new JoinField(parent);
		public static implicit operator JoinField(string parentName) => new JoinField(new Parent(parentName));
		public static implicit operator JoinField(Child child) => new JoinField(child);

		public void Match(Action<Parent> first, Action<Child> second)
		{
			switch (_tag)
			{
				case 0:
					first(this._parent);
					break;
				case 1:
					second(this._child);
					break;
				default: throw new Exception($"Unrecognized tag value: {_tag}");
			}
		}

        public class Parent
        {
            public RelationName Name { get; }

            public Parent(RelationName name)
            {
                Name = name;
            }
        }

        public class Child
        {
            public Id Parent { get; }
            public RelationName Name { get; }

            public Child(RelationName name, Id parent)
            {
                Name = name;
                Parent = parent;
            }
        }
	}
}

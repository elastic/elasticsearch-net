using System.CodeDom;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(JoinFieldJsonConverter))]
	public class JoinField : Union<JoinField.Parent, JoinField.Child>
	{
		public JoinField(Parent parent) : base(parent) { }

		public JoinField(Child child) : base(child) { }

		public static JoinField Root<TParent>() => new Parent(typeof(TParent));
		public static JoinField Root(RelationName parent) => new Parent(parent);

		public static JoinField Link(RelationName child, Id parentId) => new Child(child, parentId);
		public static JoinField Link<TChild, TParentDocument>(TParentDocument parent) where TParentDocument : class =>
			new Child(typeof(TChild), Id.From<TParentDocument>(parent));
		public static JoinField Link<TChild>(Id parentId) => new Child(typeof(TChild), parentId);

		public static implicit operator JoinField(Parent parent) => new JoinField(parent);
		public static implicit operator JoinField(string parentName) => new JoinField(new Parent(parentName));
		public static implicit operator JoinField(Child child) => new JoinField(child);

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

using System.CodeDom;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(JoinFieldJsonConverter))]
	public class JoinField : Union<JoinField.Parent, JoinField.Child>
	{
		public JoinField(Parent parentName) : base(parentName) { }

		public JoinField(Child child) : base(child) { }

		public static JoinField Root<TParent>() => new Parent(typeof(TParent));
		public static JoinField Root(TypeName parent) => new Parent(parent);

		public static JoinField Link(TypeName childName, Id parentId) => new Child(childName, parentId);
		public static JoinField Link<TChild, TParentDocument>(TParentDocument parent) where TParentDocument : class =>
			new Child(typeof(TChild), Id.From<TParentDocument>(parent));
		public static JoinField Link<TChild>(Id parentId) => new Child(typeof(TChild), parentId);

		public static implicit operator JoinField(Parent parent) => new JoinField(parent);
		public static implicit operator JoinField(string parentName) => new JoinField(new Parent(parentName));
		public static implicit operator JoinField(Child child) => new JoinField(child);

        public class Parent
        {
            internal TypeName Name { get; }

            public Parent(TypeName name)
            {
                Name = name;
            }
        }
        public class Child
        {
            internal Id Parent { get; }
            internal TypeName Name { get; }

            public Child(TypeName name, Id parent)
            {
                Name = name;
                Parent = parent;
            }
        }
	}
}

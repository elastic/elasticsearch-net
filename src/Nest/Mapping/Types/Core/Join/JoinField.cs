using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(JoinFieldJsonConverter))]
	public class JoinField : Union<ParentJoinField, ChildJoinField>
	{
		public JoinField(ParentJoinField parentName) : base(parentName) { }

		public JoinField(ChildJoinField child) : base(child) { }

		public static JoinField OfParent(TypeName parent) => new ParentJoinField(parent);
		public static JoinField OfChild(TypeName childName, Id parentId) => new ChildJoinField(childName, parentId);
		public static JoinField OfChild<TDocument>(TypeName childName, TDocument parent) where TDocument : class =>
			new ChildJoinField(childName, Id.From<TDocument>(parent));

		public static implicit operator JoinField(ParentJoinField parent) => new JoinField(parent);
		public static implicit operator JoinField(string parentName) => new ParentJoinField(parentName);
		public static implicit operator JoinField(ChildJoinField child) => new JoinField(child);

	}
	public class ParentJoinField
	{
		internal TypeName Name { get; }

		public ParentJoinField(TypeName name)
		{
			Name = name;
		}
	}
	public class ChildJoinField
	{
		internal Id Parent { get; }
		internal TypeName Name { get; }

		public ChildJoinField(TypeName name, Id parent)
		{
			Name = name;
			Parent = parent;
		}
	}
}

using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface INestedProperty : IObjectProperty
	{
		[JsonProperty("include_in_parent")]
		bool? IncludeInParent { get; set; }

		[JsonProperty("include_in_root")]
		bool? IncludeInRoot { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class NestedProperty : ObjectProperty, INestedProperty
	{
		public NestedProperty() : base(FieldType.Nested) { }

		public bool? IncludeInParent { get; set; }
		public bool? IncludeInRoot { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class NestedPropertyDescriptor<TParent, TChild>
		: ObjectPropertyDescriptorBase<NestedPropertyDescriptor<TParent, TChild>, INestedProperty, TParent, TChild>
			, INestedProperty
		where TParent : class
		where TChild : class
	{
		public NestedPropertyDescriptor() : base(FieldType.Nested) { }

		bool? INestedProperty.IncludeInParent { get; set; }
		bool? INestedProperty.IncludeInRoot { get; set; }

		public NestedPropertyDescriptor<TParent, TChild> IncludeInParent(bool? includeInParent = true) =>
			Assign(includeInParent, (a, v) => a.IncludeInParent = v);

		public NestedPropertyDescriptor<TParent, TChild> IncludeInRoot(bool? includeInRoot = true) =>
			Assign(includeInRoot, (a, v) => a.IncludeInRoot = v);
	}
}

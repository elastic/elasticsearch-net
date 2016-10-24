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

	public class NestedProperty : ObjectProperty, INestedProperty
	{
		public NestedProperty() : base("nested") { }

		public bool? IncludeInParent { get; set; }
		public bool? IncludeInRoot { get; set; }
	}

	public class NestedPropertyDescriptor<TParent, TChild>
		: ObjectPropertyDescriptorBase<NestedPropertyDescriptor<TParent, TChild>, INestedProperty, TParent, TChild>
		, INestedProperty
		where TParent : class
		where TChild : class
	{
		bool? INestedProperty.IncludeInParent { get; set; }
		bool? INestedProperty.IncludeInRoot { get; set; }

		public NestedPropertyDescriptor() : base("nested") { }

		public NestedPropertyDescriptor<TParent, TChild> IncludeInParent(bool includeInParent = true) =>
			Assign(a => a.IncludeInParent = includeInParent);

		public NestedPropertyDescriptor<TParent, TChild> IncludeInRoot(bool includeInRoot = true) =>
			Assign(a => a.IncludeInRoot = includeInRoot);
	}
}

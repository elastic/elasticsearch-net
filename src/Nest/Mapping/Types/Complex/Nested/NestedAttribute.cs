namespace Nest
{
	public class NestedAttribute : ObjectAttribute, INestedProperty
	{
		private INestedProperty Self => this;

		bool? INestedProperty.IncludeInParent { get; set; }
	 	bool? INestedProperty.IncludeInRoot { get; set; }

		public bool IncludeInParent { get => Self.IncludeInParent.GetValueOrDefault(); set => Self.IncludeInParent = value; }
		public bool IncludeInRoot { get => Self.IncludeInRoot.GetValueOrDefault(); set => Self.IncludeInRoot = value; }

		public NestedAttribute() : base(FieldType.Nested) { }
	}
}

namespace Nest
{
	public class NestedAttribute : ObjectAttribute, INestedProperty
	{
		INestedProperty Self => this;

		bool? INestedProperty.IncludeInParent { get; set; }
	 	bool? INestedProperty.IncludeInRoot { get; set; }

		public bool IncludeInParent { get { return Self.IncludeInParent.GetValueOrDefault(); } set { Self.IncludeInParent = value; } }
		public bool IncludeInRoot { get { return Self.IncludeInRoot.GetValueOrDefault(); } set { Self.IncludeInRoot = value; } }

		public NestedAttribute() : base("nested") { }
	}
}

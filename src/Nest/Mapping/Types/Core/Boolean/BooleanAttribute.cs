namespace Nest
{
	public class BooleanAttribute : ElasticsearchPropertyAttributeBase, IBooleanProperty
	{
		IBooleanProperty Self => this;

		NonStringIndexOption? IBooleanProperty.Index { get; set; }
		double? IBooleanProperty.Boost { get; set; }
		bool? IBooleanProperty.NullValue { get; set; }
		INumericFielddata IBooleanProperty.Fielddata { get; set; }

		public NonStringIndexOption Index { get { return Self.Index.GetValueOrDefault(); } set { Self.Index = value; } }
		public double Boost { get { return Self.Boost.GetValueOrDefault(); } set { Self.Boost = value; } }
		public bool NullValue { get { return Self.NullValue.GetValueOrDefault(); } set { Self.NullValue = value; } }

		public BooleanAttribute() : base("boolean") { }
	}	
}

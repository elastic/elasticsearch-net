using Nest;

namespace Tests.Mapping.Types.Shape
{
	public class ShapeTest
	{
		[Shape(
			Orientation = ShapeOrientation.ClockWise,
			Coerce = true)]
		public object Full { get; set; }

		[Shape]
		public object Minimal { get; set; }
	}

	public class ShapeAttributeTests : AttributeTestsBase<ShapeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "shape",
					orientation = "clockwise",
					coerce = true
				},
				minimal = new
				{
					type = "shape"
				}
			}
		};
	}
}

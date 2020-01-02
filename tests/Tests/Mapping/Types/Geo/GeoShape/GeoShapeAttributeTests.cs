using Nest;

namespace Tests.Mapping.Types.Geo.GeoShape
{
	public class GeoShapeTest
	{
		[GeoShape(
			Orientation = GeoOrientation.ClockWise,
			Strategy = GeoStrategy.Recursive,
			Coerce = true)]
		public object Full { get; set; }

		[GeoShape]
		public object Minimal { get; set; }
	}

	public class GeoShapeAttributeTests : AttributeTestsBase<GeoShapeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "geo_shape",
					orientation = "cw",
					strategy = "recursive",
					coerce = true
				},
				minimal = new
				{
					type = "geo_shape"
				}
			}
		};
	}
}

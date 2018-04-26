using Tests.Framework.MockData;

namespace Tests.Framework
{
	public class GeoShapeSerializationTests : SerializationTestBase
	{
		[U]
		public void CanSerializeShapes()
		{
			var shape = Shape.Generator.Generate();
			this.AssertSerializesAndRoundTrips(shape);
		}
	}
}

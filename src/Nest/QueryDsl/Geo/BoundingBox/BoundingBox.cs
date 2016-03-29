using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<BoundingBox>))]
	public interface IBoundingBox
	{
		[JsonProperty("top_left")]
		GeoLocation TopLeft { get; set; }

		[JsonProperty("bottom_right")]
		GeoLocation BottomRight { get; set; }
	}

	public class BoundingBox : IBoundingBox
	{
		public GeoLocation TopLeft { get; set; }
		public GeoLocation BottomRight { get; set; }
	}

	public class BoundingBoxDescriptor : DescriptorBase<BoundingBoxDescriptor, IBoundingBox>, IBoundingBox
	{
		GeoLocation IBoundingBox.TopLeft { get; set; }
		GeoLocation IBoundingBox.BottomRight { get; set; }

		
		public BoundingBoxDescriptor TopLeft(GeoLocation topLeft) => Assign(a => a.TopLeft = topLeft);
		public BoundingBoxDescriptor TopLeft(double lat, double lon) => Assign(a => a.TopLeft = new GeoLocation(lat,lon));

		public BoundingBoxDescriptor BottomRight(GeoLocation bottomRight) => Assign(a => a.BottomRight = bottomRight);
		public BoundingBoxDescriptor BottomRight(double lat, double lon) => Assign(a => a.BottomRight = new GeoLocation(lat, lon));

		
	}
}
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<BoundingBox>))]
	public interface IBoundingBox
	{
		[JsonProperty("bottom_right")]
		GeoLocation BottomRight { get; set; }

		[JsonProperty("top_left")]
		GeoLocation TopLeft { get; set; }

		[JsonProperty("wkt")]
		string WellKnownText { get; set; }
	}

	public class BoundingBox : IBoundingBox
	{
		public GeoLocation BottomRight { get; set; }
		public GeoLocation TopLeft { get; set; }
		public string WellKnownText { get; set; }
	}

	public class BoundingBoxDescriptor : DescriptorBase<BoundingBoxDescriptor, IBoundingBox>, IBoundingBox
	{
		GeoLocation IBoundingBox.BottomRight { get; set; }
		GeoLocation IBoundingBox.TopLeft { get; set; }
		string IBoundingBox.WellKnownText { get; set; }

		public BoundingBoxDescriptor BottomRight(GeoLocation bottomRight) => Assign(a => a.BottomRight = bottomRight);

		public BoundingBoxDescriptor BottomRight(double lat, double lon) => Assign(a => a.BottomRight = new GeoLocation(lat, lon));

		public BoundingBoxDescriptor TopLeft(GeoLocation topLeft) => Assign(a => a.TopLeft = topLeft);

		public BoundingBoxDescriptor TopLeft(double lat, double lon) => Assign(a => a.TopLeft = new GeoLocation(lat, lon));

		public BoundingBoxDescriptor WellKnownText(string wkt) => Assign(a => a.WellKnownText = wkt);
	}
}

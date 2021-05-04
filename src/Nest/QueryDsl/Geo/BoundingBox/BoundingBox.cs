// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(BoundingBox))]
	public interface IBoundingBox
	{
		[DataMember(Name = "bottom_right")]
		GeoLocation BottomRight { get; set; }

		[DataMember(Name = "top_left")]
		GeoLocation TopLeft { get; set; }

		[DataMember(Name = "wkt")]
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

		public BoundingBoxDescriptor TopLeft(GeoLocation topLeft) => Assign(topLeft, (a, v) => a.TopLeft = v);

		public BoundingBoxDescriptor TopLeft(double lat, double lon) => Assign(new GeoLocation(lat, lon), (a, v) => a.TopLeft = v);

		public BoundingBoxDescriptor BottomRight(GeoLocation bottomRight) => Assign(bottomRight, (a, v) => a.BottomRight = v);

		public BoundingBoxDescriptor BottomRight(double lat, double lon) => Assign(new GeoLocation(lat, lon), (a, v) => a.BottomRight = v);

		public BoundingBoxDescriptor WellKnownText(string wkt) => Assign(wkt, (a, v) => a.WellKnownText = v);
	}
}

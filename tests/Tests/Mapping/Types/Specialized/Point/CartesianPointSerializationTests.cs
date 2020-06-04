// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.Serialization;

namespace Tests.Mapping.Types.Specialized.Point
{
	public class CartesianPointSerializationTests
	{
		[U]
		public void CanSerializeCartesianPointFromCoordinates()
		{
			var point = new PointDocument { Point = CartesianPoint.FromCoordinates("90.0,-90.0") };
			SerializationTester.Default.AssertRoundTrip(point, new { point = "90.0,-90.0" });
		}

		[U]
		public void CanSerializeCartesianPointFromWellKnownText()
		{
			var point = new PointDocument { Point = CartesianPoint.FromWellKnownText("POINT (90.0 -90.0)") };
			SerializationTester.Default.AssertRoundTrip(point, new { point = "POINT (90.0 -90.0)" });
		}

		[U]
		public void CanSerializeCartesianPoint()
		{
			var point = new PointDocument { Point = new CartesianPoint(90, -90) };
			SerializationTester.Default.AssertRoundTrip(point, new { point = new { x = 90f, y = -90f } });
		}

		[U]
		public void CanDeserializeCartesianPointFromArray()
		{
			var point = new CartesianPoint(90, -90);

			CartesianPoint deserialized = null;
			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("[90, -90]")))
				deserialized = TestClient.DefaultInMemoryClient.SourceSerializer.Deserialize<CartesianPoint>(stream);

			deserialized.Should().Be(point);
		}

		private class PointDocument
		{
			public CartesianPoint Point { get; set; }
		}
	}
}

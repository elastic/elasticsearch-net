/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

		[U]
		public void CanDeserializeCartesianPointFromObject()
		{
			var point = new CartesianPoint(90, -90);

			CartesianPoint deserialized = null;
			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("{ \"x\": 90, \"y\": -90 }")))
				deserialized = TestClient.DefaultInMemoryClient.SourceSerializer.Deserialize<CartesianPoint>(stream);

			deserialized.Should().Be(point);
		}

		private class PointDocument
		{
			// ReSharper disable once UnusedAutoPropertyAccessor.Local
			public CartesianPoint Point { get; set; }
		}
	}
}

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.Reproduce
{
	public class GitHubIssue3819
	{
		[U]
		[UseCulture("fr-FR")]
		public void WriteWKTCoordinatesWithInvariantCulture()
		{
			var wkt = "POINT (45.1 42.25)";

			var shape = GeoWKTReader.Read(wkt);
			var actual = GeoWKTWriter.Write(shape);
			actual.Should().Be(wkt);
		}
	}
}

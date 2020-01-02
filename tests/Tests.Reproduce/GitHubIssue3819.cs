using Elastic.Xunit.XunitPlumbing;
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

using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class SegmentsTests : IntegrationTests
	{
		[Test]
		public void AllSegments()
		{
			var r = this._client.Segments();
			Assert.True(r.IsValid);
			r.Shards.Successful.Should().BeGreaterThan(0);

			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			var index = r.Indices[ElasticsearchConfiguration.DefaultIndex];
			Assert.NotNull(index);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
			Assert.NotNull(index.Shards["0"]);
			Assert.NotNull(index.Shards["0"].Routing);
			Assert.NotNull(index.Shards["0"].Segments);
			Assert.True(index.Shards["0"].Segments.Count > 0);
			var segment = index.Shards["0"].Segments.First().Value;
			Assert.NotNull(segment);
			Assert.GreaterOrEqual(segment.Generation, 0);
		}

		[Test]
		public void SingleSegment()
		{
			var r = this._client.Segments(s=>s.Index(ElasticsearchConfiguration.DefaultIndex));
			Assert.True(r.IsValid);
			r.Shards.Successful.Should().BeGreaterThan(0);

			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count == 1);
			var index = r.Indices[ElasticsearchConfiguration.DefaultIndex];
			Assert.NotNull(index);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
			Assert.NotNull(index.Shards["0"]);
			Assert.NotNull(index.Shards["0"].Routing);
			Assert.NotNull(index.Shards["0"].Segments);
			Assert.True(index.Shards["0"].Segments.Count > 0);
			var segment = index.Shards["0"].Segments.First().Value;
			Assert.NotNull(segment);
			Assert.GreaterOrEqual(segment.Generation, 0);
		}
		[Test]
		public void MultipleSegment()
		{
			var indices = new [] {ElasticsearchConfiguration.DefaultIndex , ElasticsearchConfiguration.DefaultIndex + "_clone"};
			var r = this._client.Segments(s=>s.Indices(indices));
			Assert.True(r.IsValid);
			r.Shards.Successful.Should().BeGreaterThan(0);

			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count == 2);
			var index = r.Indices[ElasticsearchConfiguration.DefaultIndex];
			Assert.NotNull(index);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
			Assert.NotNull(index.Shards["0"]);
			Assert.NotNull(index.Shards["0"].Routing);
			Assert.NotNull(index.Shards["0"].Segments);
			Assert.True(index.Shards["0"].Segments.Count > 0);
			var segment = index.Shards["0"].Segments.First().Value;
			Assert.NotNull(segment);
			Assert.GreaterOrEqual(segment.Generation, 0);
		}
	}
}
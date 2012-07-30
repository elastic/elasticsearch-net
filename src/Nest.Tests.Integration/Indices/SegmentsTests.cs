using System.Linq;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class SegmentsTests : BaseElasticSearchTests
	{
		[Test]
		public void AllSegments()
		{
			var r = this.ConnectedClient.Segments();
			Assert.True(r.IsValid);
			Assert.True(r.OK);

			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			var index = r.Indices[this.Settings.DefaultIndex];
			Assert.NotNull(index);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
			Assert.NotNull(index.Shards["0"]);
			Assert.NotNull(index.Shards["0"].Routing);
			Assert.NotNull(index.Shards["0"].Segments);
			Assert.True(index.Shards["0"].Segments.Count > 0);
			var segment = index.Shards["0"].Segments.First().Value;
			Assert.NotNull(segment);
			Assert.Greater(segment.Generation, 0);
		}

		[Test]
		public void SingleSegment()
		{
			var r = this.ConnectedClient.Segments(this.Settings.DefaultIndex);
			Assert.True(r.IsValid);
			Assert.True(r.OK);

			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count == 1);
			var index = r.Indices[this.Settings.DefaultIndex];
			Assert.NotNull(index);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
			Assert.NotNull(index.Shards["0"]);
			Assert.NotNull(index.Shards["0"].Routing);
			Assert.NotNull(index.Shards["0"].Segments);
			Assert.True(index.Shards["0"].Segments.Count > 0);
			var segment = index.Shards["0"].Segments.First().Value;
			Assert.NotNull(segment);
			Assert.Greater(segment.Generation, 0);
		}
		[Test]
		public void MultipleSegment()
		{
			var indices = new [] {this.Settings.DefaultIndex , this.Settings.DefaultIndex + "_clone"};
			var r = this.ConnectedClient.Segments(indices);
			Assert.True(r.IsValid);
			Assert.True(r.OK);

			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count == 2);
			var index = r.Indices[this.Settings.DefaultIndex];
			Assert.NotNull(index);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
			Assert.NotNull(index.Shards["0"]);
			Assert.NotNull(index.Shards["0"].Routing);
			Assert.NotNull(index.Shards["0"].Segments);
			Assert.True(index.Shards["0"].Segments.Count > 0);
			var segment = index.Shards["0"].Segments.First().Value;
			Assert.NotNull(segment);
			Assert.Greater(segment.Generation, 0);
		}
	}
}
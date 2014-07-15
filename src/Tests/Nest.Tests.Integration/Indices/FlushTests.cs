using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class FlushTests : IntegrationTests
	{
		[Test]
		public void FlushAll()
		{
			var r = this.Client.Flush(f=>f.AllIndices());
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void FlushIndex()
		{
			var r = this.Client.Flush(f=>f.Index(ElasticsearchConfiguration.DefaultIndex));
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void FlushIndeces()
		{
			var r = this.Client.Flush(f=>f
				.Indices( ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_clone")
			);
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void FlushTyped()
		{
			var r = this.Client.Flush(f=>f.Index<ElasticsearchProject>());
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		
	}
}
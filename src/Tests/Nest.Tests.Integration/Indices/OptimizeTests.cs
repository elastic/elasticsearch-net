using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class OptimizeTests : IntegrationTests
	{
		[Test]
		public void OptimizeAll()
		{
			var r = this._client.Optimize();
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void OptimizeIndex()
		{
			var r = this._client.Optimize(o=>o.Index(ElasticsearchConfiguration.DefaultIndex));
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void OptimizeIndices()
		{
			var r = this._client.Optimize(o=>o.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_clone" ));
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void OptimizeTyped()
		{
			var r = this._client.Optimize(o=>o.Index<ElasticsearchProject>());
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		public void OptimizeAllWithParameters()
		{
			var r = this._client.Optimize(o=>o.MaxNumSegments(2));
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}

	}
}
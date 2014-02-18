using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class RefreshTests : IntegrationTests
	{
		[Test]
		public void RefreshAll()
		{
			var r = this._client.Refresh();
			r.Shards.Should().NotBeNull();
			r.Shards.Total.Should().BeGreaterThan(0);
			r.Shards.Total.Should().Be(r.Shards.Successful);
		}
		[Test]
		public void RefreshIndex()
		{
			var r = this._client.Refresh(e=>e.Index(ElasticsearchConfiguration.DefaultIndex));
			r.Shards.Should().NotBeNull();
			r.Shards.Total.Should().BeGreaterThan(0);
			r.Shards.Total.Should().Be(r.Shards.Successful);
		}
		[Test]
		public void RefreshIndeces()
		{
			var r = this._client.Refresh(rr=>rr
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_clone" )
			);
			r.Shards.Should().NotBeNull();
			r.Shards.Total.Should().BeGreaterThan(0);
			r.Shards.Total.Should().Be(r.Shards.Successful);
		}
		[Test]
		public void RefreshTyped()
		{
			var r = this._client.Refresh(rr => rr.Index<ElasticsearchProject>());
			r.Shards.Should().NotBeNull();
			r.Shards.Total.Should().BeGreaterThan(0);
			r.Shards.Total.Should().Be(r.Shards.Successful);
		}
	}
}
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
			var r = this.Client.Refresh();
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void RefreshIndex()
		{
			var r = this.Client.Refresh(e=>e.Index(ElasticsearchConfiguration.DefaultIndex));
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void RefreshIndeces()
		{
			var r = this.Client.Refresh(rr=>rr
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_clone" )
			);
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void RefreshTyped()
		{
			var r = this.Client.Refresh(rr => rr.Index<ElasticsearchProject>());
			r.Shards.Should().NotBeNull();
			r.Shards.Successful.Should().BeGreaterThan(0);
		}
	}
}
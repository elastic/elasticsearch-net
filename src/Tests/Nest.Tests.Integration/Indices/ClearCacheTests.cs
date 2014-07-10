using System.Collections.Generic;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class ClearCacheTest : IntegrationTests
	{
		[Test]
		public void test_clear_cache()
		{
			var client = this._client;
			var status = client.ClearCache();
			Assert.True(status.IsValid);
			status.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void test_clear_cache_specific()
		{
			var client = this._client;
			var status = client.ClearCache(cc=>cc.Filter().Recycler());
			Assert.True(status.IsValid);
			status.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void test_clear_cache_generic_specific()
		{
			var client = this._client;
			var status = client.ClearCache(cc=>cc.Index<ElasticsearchProject>().Filter().Recycler());
			Assert.True(status.IsValid);
			status.Shards.Successful.Should().BeGreaterThan(0);
		}
		[Test]
		public void test_clear_cache_generic_specific_indices()
		{
			var client = this._client;
			var status = client.ClearCache(cc=>cc.Indices(_settings.DefaultIndex, _settings.DefaultIndex + "_clone").Filter().Recycler());
			Assert.True(status.IsValid);
			status.Shards.Successful.Should().BeGreaterThan(0);
		}
	}
}
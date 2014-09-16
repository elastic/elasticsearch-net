using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Get
{
	[TestFixture]
	public class SearchShardsTests : IntegrationTests
	{
		[Test]
		public void ReturnsErrorOnIndexWithIgnoreUnavailableSetToFalse()
		{
			var response = this.Client.SearchShards<ElasticsearchProject>(m => m
				.Index("this-index-does-not-exists")
				.IgnoreUnavailable(false)
			);

			response.IsValid.Should().BeFalse();
			response.ServerError.Status.Should().Be(404);
			response.ServerError.ExceptionType.Should().Be("IndexMissingException");
		}

		[Test]
		public void ReturnsNodeInformation()
		{
			var response = this.Client.SearchShards(new SearchShardsRequest());
			response.Nodes.Should().NotBeEmpty();
			var firstNode = response.Nodes.First();
			firstNode.Key.Should().NotBeNullOrWhiteSpace();
			firstNode.Value.Should().NotBeNull();
			firstNode.Value.Name.Should().NotBeNullOrWhiteSpace();
			firstNode.Value.TransportAddress.Should().NotBeNullOrWhiteSpace();
		}

		[Test]
		public void ReturnsShardInformation()
		{
			var response = this.Client.SearchShards(new SearchShardsRequest());
			response.Shards.Should().NotBeEmpty();
			var firstShardIndexGroup = response.Shards.ToList();
			firstShardIndexGroup.Should().NotBeEmpty();
			var firstShardIndexShardGroup = firstShardIndexGroup.First().ToList();
			firstShardIndexShardGroup.Should().NotBeEmpty();
			var shard = firstShardIndexShardGroup.First();
			shard.Index.Should().NotBeNullOrWhiteSpace();
			shard.Node.Should().NotBeNullOrWhiteSpace();
		}

	}
}
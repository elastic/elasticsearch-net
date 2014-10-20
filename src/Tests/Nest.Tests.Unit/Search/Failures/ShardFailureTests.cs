using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.Failures
{
	[TestFixture]
	public class ShardFailureTests : BaseJsonTests
	{
		[Test]
		public void ShardFailuresAreExposed()
		{
			var client = GetFixedReturnClient(MethodBase.GetCurrentMethod(), "ShardFailuresResult");

			var result = client.Search<ElasticsearchProject>(s=>s.MatchAll());
			result.IsValid.Should().BeTrue();

			result.Shards.Should().NotBeNull();
			result.Shards.Failures.Should().NotBeEmpty();
			var shardFailure = result.Shards.Failures.First();
			shardFailure.Index.Should().Be("<indexname>");
			shardFailure.Reason.Should().Be("<error message>");
			shardFailure.Status.Should().Be(500);
			shardFailure.Shard.Should().Be(4);
		}
		
	}
}

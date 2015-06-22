using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class SyncedFlushTests : IntegrationTests
	{
		private void Assert(IShardsOperationResponse response)
		{
			response.IsValid.Should().BeTrue();
			response.Shards.Should().NotBeNull();
			response.Shards.Successful.Should().BeGreaterThan(0);
		}

		[Test]
		[SkipVersion("0 - 1.5.9", "Synced Flush added in ES 1.6")]
		public void SyncedFlushAll()
		{
			var r = this.Client.SyncedFlush(sf => sf.AllIndices());
			Assert(r);
		}

		[Test]
		[SkipVersion("0 - 1.5.9", "Synced Flush added in ES 1.6")]
		public void SyncedFlushIndex()
		{
			var r = this.Client.SyncedFlush(f => f.Index(ElasticsearchConfiguration.DefaultIndex));
			Assert(r);
		}

		[Test]
		[SkipVersion("0 - 1.5.9", "Synced Flush added in ES 1.6")]
		public void SyncedFlushIndices()
		{
			var r = this.Client.SyncedFlush(f => f
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_clone")
			);
			Assert(r);
		}

		[Test]
		[SkipVersion("0 - 1.5.9", "Synced Flush added in ES 1.6")]
		public void SyncedFlushTyped()
		{
			var r = this.Client.SyncedFlush(f => f.Index<ElasticsearchProject>());
			Assert(r);
		}
	}
}

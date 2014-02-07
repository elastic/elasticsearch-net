using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class StatusTests : IntegrationTests
	{
		[Test]
		public void StatusAll()
		{
			var r = this._client.Status();
			Assert.True(r.OK);
			Assert.True(r.IsValid);
			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count > 0);
			var index = r.Indices.First().Value;
			Assert.NotNull(index);
			Assert.NotNull(index.Index);
			Assert.NotNull(index.Translog);
			Assert.NotNull(index.IndexDocs);
			Assert.NotNull(index.Flush);
			Assert.NotNull(index.Merges);
			Assert.NotNull(index.Refresh);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
		}
		[Test]
		public void StatusAllWithBothParamsTrue()
		{
			var r = this._client.Status(s => s.Recovery().Snapshot());
			Assert.True(r.OK);
			Assert.True(r.IsValid);
			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count > 0);
			var index = r.Indices.First().Value;
			Assert.NotNull(index);
			Assert.NotNull(index.Index);
			Assert.NotNull(index.Translog);
			Assert.NotNull(index.IndexDocs);
			Assert.NotNull(index.Flush);
			Assert.NotNull(index.Merges);
			Assert.NotNull(index.Refresh);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
		}
		[Test]
		public void StatusAllWithParamsTrueAndFalse()
		{
			var r = this._client.Status(s => s.Recovery().Snapshot(false));
			Assert.True(r.OK);
			Assert.True(r.IsValid);
			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count > 0);
			var index = r.Indices.First().Value;
			Assert.NotNull(index);
			Assert.NotNull(index.Index);
			Assert.NotNull(index.Translog);
			Assert.NotNull(index.IndexDocs);
			Assert.NotNull(index.Flush);
			Assert.NotNull(index.Merges);
			Assert.NotNull(index.Refresh);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
		}
		[Test]
		public void StatusIndex()
		{
			var r = this._client.Status(s => s.Index(ElasticsearchConfiguration.DefaultIndex));
			Assert.True(r.OK);
			Assert.True(r.IsValid);
			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count > 0);
			var index = r.Indices.First().Value;
			Assert.NotNull(index);
			Assert.NotNull(index.Index);
			Assert.NotNull(index.Translog);
			Assert.NotNull(index.IndexDocs);
			Assert.NotNull(index.Flush);
			Assert.NotNull(index.Merges);
			Assert.NotNull(index.Refresh);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
		}
		[Test]
		public void StatusIndeces()
		{
			var r = this._client.Status(s => s
				.Indices(ElasticsearchConfiguration.DefaultIndex, ElasticsearchConfiguration.DefaultIndex + "_clone")
			);
			Assert.True(r.OK);
			Assert.True(r.IsValid);
			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count > 0);
			var index = r.Indices.First().Value;
			Assert.NotNull(index);
			Assert.NotNull(index.Index);
			Assert.NotNull(index.Translog);
			Assert.NotNull(index.IndexDocs);
			Assert.NotNull(index.Flush);
			Assert.NotNull(index.Merges);
			Assert.NotNull(index.Refresh);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
		}
		[Test]
		public void StatusTyped()
		{
			var r = this._client.Status(s=>s.Index<ElasticsearchProject>());
			Assert.True(r.OK);
			Assert.True(r.IsValid);
			Assert.NotNull(r.Shards);
			Assert.NotNull(r.Indices);
			Assert.True(r.Indices.Count > 0);
			var index = r.Indices.First().Value;
			Assert.NotNull(index);
			Assert.NotNull(index.Index);
			Assert.NotNull(index.Translog);
			Assert.NotNull(index.IndexDocs);
			Assert.NotNull(index.Flush);
			Assert.NotNull(index.Merges);
			Assert.NotNull(index.Refresh);
			Assert.NotNull(index.Shards);
			Assert.True(index.Shards.Count > 0);
		}
	}
}

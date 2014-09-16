using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Cat
{
	[TestFixture]
	public class CatTests : IntegrationTests
	{
		private readonly ElasticClient _client;

		public CatTests()
		{
			this._client = this.Client as ElasticClient;
		}

		private void TestCat<TRecord>(Func<ICatResponse<TRecord>> call, Expression<Func<TRecord, bool>> trueForAll)
			where TRecord : ICatRecord
		{
			var response = call();
			response.Should().NotBeNull();
			response.IsValid.Should().BeTrue();
			response.Records.Should().NotBeEmpty().And.OnlyContain(trueForAll);
		}

		private async Task TestCatAsync<TRecord>(Func<Task<ICatResponse<TRecord>>> call, Expression<Func<TRecord, bool>> trueForAll)
			where TRecord : ICatRecord
		{
			var response = await call();
			response.Should().NotBeNull();
			response.IsValid.Should().BeTrue();
			response.Records.Should().NotBeEmpty().And.OnlyContain(trueForAll);
		}


		[Test]
		[SkipVersion("0 - 1.0.3", "Fails on ES < 1.1")]
		public void CatAliases()
		{
			TestCat(() => this._client.CatAliases(s => s.V()), r => !r.Alias.IsNullOrEmpty());
		}

		[Test]
		[SkipVersion("0 - 1.0.3", "Fails on ES < 1.1")]
		public async void CatAliasesAsync()
		{
			await TestCatAsync(() => this._client.CatAliasesAsync(), r => !r.Alias.IsNullOrEmpty());
		}

		[Test]
		public void CatAllocation()
		{
			TestCat(() => this._client.CatAllocation(), r => !r.Node.IsNullOrEmpty());
		}

		[Test]
		public async void CatAllocationAsync()
		{
			await TestCatAsync(() => this._client.CatAllocationAsync(), r => !r.Node.IsNullOrEmpty());
		}

		[Test]
		public void CatCount()
		{
			TestCat(() => this._client.CatCount(), r => !r.Epoch.IsNullOrEmpty());
		}

		[Test]
		public async void CatCountAsync()
		{
			await TestCatAsync(() => this._client.CatCountAsync(), r => !r.Timestamp.IsNullOrEmpty());
		}

		[Test]
		[SkipVersion("0 - 1.1.9", "/_cat/fielddata endpoint added in 1.2")]
		public void CatFielddata()
		{
			TestCat(() => this._client.CatFielddata(
				v => v.Fields<ElasticsearchProject>(p => p.Name)), 
				r => 
					r.FieldSizes.ContainsKey("name"));
		}

		[Test]
		[SkipVersion("0 - 1.1.9", "/_cat/fielddata endpoint added in 1.2")]
		public async void CatFielddataAsync()
		{
			await TestCatAsync(() => this._client.CatFielddataAsync(v => v.Fields<ElasticsearchProject>(p => p.Name)), r => r.FieldSizes.ContainsKey("name"));
		}

		[Test]
		public void CatHealth()
		{
			TestCat(() => this._client.CatHealth(), r => !r.Cluster.IsNullOrEmpty());
		}

		[Test]
		public async void CatHealthAsync()
		{
			await TestCatAsync(() => this._client.CatHealthAsync(), r => !r.NodeTotal.IsNullOrEmpty());
		}

		[Test]
		public void CatIndices()
		{
			TestCat(() => this._client.CatIndices(), r => !r.Index.IsNullOrEmpty());
		}

		[Test]
		public async void CatIndicesAsync()
		{
			await TestCatAsync(() => this._client.CatIndicesAsync(), r => !r.DocsDeleted.IsNullOrEmpty());
		}

		[Test]
		public void CatMaster()
		{
			TestCat(() => this._client.CatMaster(), r => !r.Ip.IsNullOrEmpty());
		}

		[Test]
		public async void CatMasterAsync()
		{
			await TestCatAsync(() => this._client.CatMasterAsync(), r => !r.Node.IsNullOrEmpty());
		}

		[Test]
		public void CatNodes()
		{
			TestCat(() => this._client.CatNodes(v => v.H("b")), r => !r.Build.IsNullOrEmpty());
		}

		[Test]
		public async void CatNodesAsync()
		{
			await TestCatAsync(() => this._client.CatNodesAsync(), r => !r.Master.IsNullOrEmpty());
		}

		[Test]
		public void CatPendingTasks()
		{
			var response = this._client.CatPendingTasks();
			response.Should().NotBeNull();
			response.IsValid.Should().BeTrue();
		}

		[Test]
		public async void CatPendingTasksAsync()
		{
			var response = await this._client.CatPendingTasksAsync();
			response.Should().NotBeNull();
			response.IsValid.Should().BeTrue();
		}

		[Test]
		[SkipVersion("0 - 1.1.0", "Fails on ES <= 1.1.0 (#5778)")]
		public void CatPlugins()
		{
			TestCat(() => this._client.CatPlugins(), r => !r.Version.IsNullOrEmpty());
		}

		[Test]
		[SkipVersion("0 - 1.1.0", "Fails on ES <= 1.1.0 (#5778)")]
		public async void CatPluginsAsync()
		{
			await TestCatAsync(() => this._client.CatPluginsAsync(), r => !r.Type.IsNullOrEmpty());
		}

		[Test]
		[SkipVersion("0 - 1.0.3", "Fails on ES < 1.1")]
		public void CatRecovery()
		{
			TestCat(() => this._client.CatRecovery(), r => !r.Shard.IsNullOrEmpty());
		}

		[Test]
		[SkipVersion("0 - 1.0.3", "Fails on ES < 1.1")]
		public async void CatRecoveryAsync()
		{
			await TestCatAsync(() => this._client.CatRecoveryAsync(), r => !r.Files.IsNullOrEmpty());
		}

		[Test]
		public void CatThreadPool()
		{
			TestCat(() => this._client.CatThreadPool(), r => !r.Ip.IsNullOrEmpty());
		}
		[Test]
		public async void CatThreadPoolAsync()
		{
			await TestCatAsync(() => this._client.CatThreadPoolAsync(v => v.H("bc")), r => r.Bulk != null && !r.Bulk.Completed.IsNullOrEmpty());
		}

		[Test]
		public void CatShards()
		{
			TestCat(() => this._client.CatShards(), r => !r.State.IsNullOrEmpty());
		}

		[Test]
		public async void CatShardsAsync()
		{
			await TestCatAsync(() => this._client.CatShardsAsync(), r => !r.State.IsNullOrEmpty());
		}

	}
}

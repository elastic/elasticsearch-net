// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Document.Multiple.BulkAll
{
	public abstract class BulkAllApiTestsBase : IClusterFixture<IntrusiveOperationCluster>
	{
		protected BulkAllApiTestsBase(IntrusiveOperationCluster cluster) => Client = cluster.Client;

		protected IElasticClient Client { get; }

		protected static string CreateIndexName() => $"project-copy-{Guid.NewGuid().ToString("N").Substring(8)}";

		protected IEnumerable<SmallObject> CreateLazyStreamOfDocuments(int count)
		{
			for (var i = 0; i < count; i++)
				yield return new SmallObject() { Id = i };
		}

		protected async Task CreateIndexAsync(string indexName, int numberOfShards, Func<TypeMappingDescriptor<SmallObject>, ITypeMapping> mappings = null)
		{
			var result = await Client.Indices.CreateAsync(indexName, s => s
				.Settings(settings => settings
					.NumberOfShards(numberOfShards)
					.NumberOfReplicas(0)
				)
				.Map(mappings)
			);
			result.Should().NotBeNull();
			result.ShouldBeValid();
		}

		protected static void OnError(ref Exception ex, Exception e, EventWaitHandle handle)
		{
			ex = e;
			handle.Set();
			throw e;
		}

		protected class SmallObject
		{
			public int Id { get; set; }
			public string Number { get; set; }
		}
	}
}

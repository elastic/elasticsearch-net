// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Mapping;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;

namespace Tests.Document.Multiple;

public abstract class BulkAllApiTestsBase : IClusterFixture<IntrusiveOperationCluster>
{
	protected BulkAllApiTestsBase(IntrusiveOperationCluster cluster) => Client = cluster.Client;

	protected IElasticClient Client { get; }

	protected static string CreateIndexName() => $"project-copy-{Guid.NewGuid().ToString("N")[8..]}";

	protected static IEnumerable<SmallObject> CreateLazyStreamOfDocuments(int count)
	{
		for (var i = 0; i < count; i++)
			yield return new SmallObject() { Id = i };
	}

	//protected async Task CreateIndexAsync(string indexName, int numberOfShards, TypeMapping mappings = null)
	//{
	//	var result = await Client.IndexManagement.CreateIndexAsync(new Elastic.Clients.Elasticsearch.IndexManagement.CreateIndexRequest(indexName)
	//	{
	//		Settings = new Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings
	//		{
	//			NumberOfReplicas = 0,
	//			NumberOfShards = numberOfShards
	//		},
	//		Mappings = mappings
	//	});

	//	//var result = await Client.IndexManagement.CreateIndexAsync(indexName, s => s
	//	//	.Settings(settings => settings
	//	//		.NumberOfShards(numberOfShards)
	//	//		.NumberOfReplicas(0)
	//	//	)
	//	//	.Map(mappings)
	//	//);

	//	result.Should().NotBeNull();
	//	result.ShouldBeValid();
	//}

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

using System;
using System.Linq;
using System.Threading;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Multiple.Reindex
{
	// TODO re-implement these tests once Reindex is implemented
	[CollectionDefinition(IntegrationContext.Reindex)]
	public class ReindexCluster : ClusterBase, ICollectionFixture<ReindexCluster>, IClassFixture<EndpointUsage>
	{
		public override void Boostrap()
		{
			var seeder = new Seeder(this.Node);
			seeder.DeleteIndicesAndTemplates();
			seeder.CreateIndices();
		}
	}

	[Collection(IntegrationContext.Reindex)]
	public class ReindexApiTests : SerializationTestBase
	{
		private readonly IObservable<IReindexResponse<ILazyDocument>> _reindexManyTypesResult;
		private readonly IObservable<IReindexResponse<Project>> _reindexSingleTypeResult;
		private readonly IElasticClient _client;

		public ReindexApiTests(ReindexCluster cluster, EndpointUsage usage)
		{
		}

		[I]
		public void ReturnsExpectedResponse()
		{
		}
	}
}

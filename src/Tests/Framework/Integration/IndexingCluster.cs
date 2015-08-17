using System;
using System.Collections.Generic;
using Nest;
using Xunit;
using System.Reactive.Linq;
using System.Threading;

namespace Tests.Framework.Integration
{
	[CollectionDefinition(IntegrationContext.Indexing)]
	public class IndexingCluster : ClusterBase, ICollectionFixture<IndexingCluster>, IClassFixture<ApiUsage>
	{
		public override void Boostrap() => new Seeder(this.Node.Port).SeedNode();
	}
}

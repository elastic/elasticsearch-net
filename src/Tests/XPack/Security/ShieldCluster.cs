using System.Collections.Generic;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.XPack.Security
{

	[CollectionDefinition(TypeOfCluster.Shield)]
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public class ShieldCluster : ClusterBase, ICollectionFixture<ShieldCluster>, IClassFixture<EndpointUsage>
	{
	}
}

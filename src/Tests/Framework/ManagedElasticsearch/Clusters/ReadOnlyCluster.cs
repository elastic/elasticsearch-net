using System;
using Xunit;

namespace Tests.Framework.Integration
{
	[RequiresPlugin(ElasticsearchPlugin.MapperMurmer3)]
	public class ReadOnlyCluster : ClusterBase
	{
		protected override void AfterNodeStarts() => new Seeder(this.Node).SeedNode();
	}
}

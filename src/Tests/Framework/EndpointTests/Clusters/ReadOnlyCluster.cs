using System;
using Xunit;

namespace Tests.Framework.Integration
{
	[RequiresPlugin(ElasticsearchPlugin.MapperMurmer3)]
	public class ReadOnlyCluster : ClusterBase
	{
		public override void Bootstrap() => new Seeder(this.Node).SeedNode();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.XPack.Shield
{

	[CollectionDefinition(IntegrationContext.Shield)]
	public class ShieldCluster : ClusterBase, ICollectionFixture<ShieldCluster>, IClassFixture<EndpointUsage>
	{
		protected override bool EnableShield => true;

		public override void Boostrap()
		{
		}
	}
}

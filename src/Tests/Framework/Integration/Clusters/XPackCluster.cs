using Tests.Framework.Integration;
using Xunit;

namespace Tests.Framework.Integration
{
	public class XPackCluster : ClusterBase
	{
		protected override bool EnableShield => true;
	}
}

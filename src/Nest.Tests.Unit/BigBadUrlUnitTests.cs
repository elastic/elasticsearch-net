using System;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Cluster
{
	[TestFixture]
	public class BigBadUrlUnitTests : BaseJsonTests
	{
		[Test]
		public void TestAllTheUrls_ExclamationPoint()
		{
			var r = this._client.Health(h=>h.Level(LevelOptions.Cluster));
			var u = new Uri(r.ConnectionStatus.RequestUrl);
			u.AbsolutePath.Should().StartWith("/_cluster/health");
			u.Query.Should().Contain("level=cluster");
		}
	
	}
}
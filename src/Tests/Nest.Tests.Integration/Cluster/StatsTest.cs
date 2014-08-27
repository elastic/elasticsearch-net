using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Cluster
{
	[TestFixture]
	public class StatsTest : IntegrationTests
	{
		[Test]
		public void ClusterStats()
		{
			var r = this.Client.ClusterStats();
			r.IsValid.Should().BeTrue();
			r.ClusterName.Should().NotBeNullOrEmpty();
			r.Status.Should().NotBeNullOrEmpty();
			r.Indices.Should().NotBeNull();
			r.Nodes.Should().NotBeNull();
		}
	}
}

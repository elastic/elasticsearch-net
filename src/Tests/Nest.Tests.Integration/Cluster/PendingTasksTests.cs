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
	public class PendingTasksTests : IntegrationTests
	{
		[Test]
		public void PendingTasks()
		{
			var r = this.Client.ClusterPendingTasks();
			r.IsValid.Should().BeTrue();
		}
	}
}

using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Cluster
{
	[TestFixture]
	public class PendingTasksTests : BaseJsonTests
	{
		[Test]
		public void Url()
		{
			var r = this._client.ClusterPendingTasks();
			var status = r.ConnectionStatus;
			var url = new Uri(status.RequestUrl);
			url.AbsolutePath.Should().StartWith("/_cluster/pending_tasks"); 
		}
	}
}

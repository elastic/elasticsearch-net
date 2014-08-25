using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Core
{
	[TestFixture]
	public class PingTests : IntegrationTests
	{
		[Test]
		public void PingTest()
		{
			var r = this.Client.Ping();
			r.IsValid.Should().BeTrue();
			r.ConnectionStatus.HttpStatusCode.ShouldBeEquivalentTo(200);
		}
	}
}

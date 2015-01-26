using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class UpgradeTests : IntegrationTests
	{
		[Test]
		[SkipVersion("0 - 1.3.9", "Upgrade api added in ES 1.4")]
		public void UpgradeAllTest()
		{
			var response = this.Client.Upgrade();
			response.ConnectionStatus.RequestMethod.Should().Be("POST");
			response.IsValid.Should().BeTrue();
			response.Shards.Should().NotBeNull();
			response.Shards.Total.Should().BeGreaterThan(0);
			response.Shards.Successful.Should().BeGreaterThan(0);
			response.Shards.Failed.Should().Be(0);
			var uri = new Uri(response.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.ShouldBeEquivalentTo("/_upgrade");
		}

		[Test]
		[SkipVersion("0 - 1.3.9", "Upgrade api added in ES 1.4")]
		public void UpgradeTest()
		{
			var index = ElasticsearchConfiguration.DefaultIndex;
			var response = this.Client.Upgrade(u => u.Index(index));
			response.ConnectionStatus.RequestMethod.Should().Be("POST");
			response.IsValid.Should().BeTrue();
			response.Shards.Should().NotBeNull();
			response.Shards.Total.Should().BeGreaterThan(0);
			response.Shards.Successful.Should().BeGreaterThan(0);
			response.Shards.Failed.Should().Be(0);
			var uri = new Uri(response.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.ShouldBeEquivalentTo("/" + index + "/_upgrade");
		}

		[Test]
		[SkipVersion("0 - 1.3.9", "Upgrade api added in ES 1.4")]
		public void UpgradeStatusAllTest()
		{
			var response = this.Client.UpgradeStatus();
			response.IsValid.Should().BeTrue();
			response.ConnectionStatus.RequestMethod.Should().Be("GET");
			var uri = new Uri(response.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.ShouldBeEquivalentTo("/_upgrade");
			response.Upgrades.Should().NotBeNull();
			response.Upgrades.Count.Should().BeGreaterThan(0);
		}

		[Test]
		[SkipVersion("0 - 1.3.9", "Upgrade api added in ES 1.4")]
		public void UpgradeStatusTest()
		{
			var index = ElasticsearchConfiguration.DefaultIndex;

			var response = this.Client.UpgradeStatus(us => us.Index(index));
			response.IsValid.Should().BeTrue();
			response.ConnectionStatus.RequestMethod.Should().Be("GET");
			var uri = new Uri(response.ConnectionStatus.RequestUrl);
			uri.AbsolutePath.ShouldBeEquivalentTo("/" + index + "/_upgrade");
			response.Upgrades.Should().NotBeNull();
			response.Upgrades.Count.Should().Be(1);

			var upgrade = response.Upgrades[index];
			upgrade.Should().NotBeNull();
			upgrade.SizeInBytes.Should().BeGreaterThan(0);
		}
	}
}

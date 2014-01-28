using System;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Settings
{
	[TestFixture]
	public class UsePrettyResponses : BaseJsonTests
	{
		[Test]
		public void UsePrettyResponsesShouldSurviveUrlModififications()
		{
			var settings = new ConnectionSettings(Test.Default.Uri)
				.SetDefaultIndex(Test.Default.DefaultIndex)
				.UsePrettyResponses();
			var connection = new InMemoryConnection(settings);
			var client = new ElasticClient(settings, connection);

			var r = client.Health(h=>h.Level(LevelOptions.Cluster));
			var u = new Uri(r.ConnectionStatus.RequestUrl);
			u.AbsolutePath.Should().StartWith("/_cluster/health");
			u.Query.Should().Contain("level=cluster");

			u.Query.Should().Contain("pretty=true");
		}
		
	}
}
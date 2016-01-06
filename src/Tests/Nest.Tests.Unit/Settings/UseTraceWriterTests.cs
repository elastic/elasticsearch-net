using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.Settings
{
	[TestFixture]
	public class UseTraceWriterTests : BaseJsonTests
	{
		[Test]
		public void EnableTraceShouldWriteToPassedTextWriter()
		{
			var stringBuilder = new StringBuilder();
			using (var writer = new StringWriter(stringBuilder))
			{
				var settings = new ConnectionSettings(UnitTestDefaults.Uri, UnitTestDefaults.DefaultIndex)
					.EnableTrace(true, writer);
				var connection = new InMemoryConnection(settings);
				var client = new ElasticClient(settings, connection);

				var r = client.ClusterHealth(h => h.Level(Level.Cluster));
			}

			var lines = stringBuilder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			lines[0].Should().StartWith("NEST start:");
			lines[1].Should().StartWith("NEST end:");
		}

		[Test]
		public async Task EnableTraceShouldWriteToPassedTextWriterAsync()
		{
			var stringBuilder = new StringBuilder();
			using (var writer = new StringWriter(stringBuilder))
			{
				var settings = new ConnectionSettings(UnitTestDefaults.Uri, UnitTestDefaults.DefaultIndex)
					.EnableTrace(true, writer);
				var connection = new InMemoryConnection(settings);
				var client = new ElasticClient(settings, connection);

				var r = await client.ClusterHealthAsync(h => h.Level(Level.Cluster));
			}

			var lines = stringBuilder.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			lines[0].Should().StartWith("NEST start:");
			lines[1].Should().StartWith("NEST end:");
		}
	}
}
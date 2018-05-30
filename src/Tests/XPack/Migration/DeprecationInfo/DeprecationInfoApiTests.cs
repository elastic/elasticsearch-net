using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.XPack.Migration.DeprecationInfo
{
	public class DeprecationInfoApiTests
	{
		[U]
		public void ShouldDeserialize()
		{
			const string indexName = ".monitoring-es-6-2017.07.21";

			var fixedResponse = new
			{
				cluster_settings = new[]
				{
					new
					{
						level = "info",
						message = "Network settings changes",
						url = "https://www.elastic.co/guide/en/elasticsearch/reference/6.0/breaking_60_indices_changes.html#_index_templates_use_literal_index_patterns_literal_instead_of_literal_template_literal",
						details = "templates using <literal>template</literal> field: watches,.monitoring-alerts,.watch-history-6,.ml-notifications,security-index-template,triggered_watches,.monitoring-es,.ml-meta,.ml-state,.monitoring-logstash,.ml-anomalies-,.monitoring-kibana"
					}
				},
				node_settings = new object[0],
				index_settings = new Dictionary<string, object>
				{
					{
						indexName, new object[]
						{
							new {
								level = "info",
								message = "Coercion of boolean fields",
								url = "https://www.elastic.co/guide/en/elasticsearch/reference/6.0/breaking_60_mappings_changes.html#_coercion_of_boolean_fields",
								details = "<anchor id=\"type: doc\" xreflabel=\"field: spins]\"/>"
							}
						}
					}
				}
			};

			var client = TestClient.GetFixedReturnClient(fixedResponse);

			//warmup
			var response = client.DeprecationInfo();
			response.ShouldBeValid();

			response.ClusterSettings.Should().NotBeNull();
			response.ClusterSettings.Should().HaveCount(1);
			response.ClusterSettings.First().Level.Should().Be(DeprecationWarningLevel.Information);
			response.ClusterSettings.First().Message.Should().Be("Network settings changes");
			response.ClusterSettings.First().Url.Should().Be("https://www.elastic.co/guide/en/elasticsearch/reference/6.0/breaking_60_indices_changes.html#_index_templates_use_literal_index_patterns_literal_instead_of_literal_template_literal");
			response.ClusterSettings.First().Details.Should().Be("templates using <literal>template</literal> field: watches,.monitoring-alerts,.watch-history-6,.ml-notifications,security-index-template,triggered_watches,.monitoring-es,.ml-meta,.ml-state,.monitoring-logstash,.ml-anomalies-,.monitoring-kibana");

			response.NodeSettings.Should().NotBeNull();
			response.NodeSettings.Should().BeEmpty();

			response.IndexSettings.Should().NotBeNull();
			response.IndexSettings.Should().HaveCount(1);
			response.IndexSettings.Should().ContainKey(indexName);
			response.IndexSettings[indexName].Count.Should().Be(1);

			var deprecationInfo = response.IndexSettings[indexName].First();
			deprecationInfo.Details.Should().Be("<anchor id=\"type: doc\" xreflabel=\"field: spins]\"/>");
			deprecationInfo.Url.Should().Be("https://www.elastic.co/guide/en/elasticsearch/reference/6.0/breaking_60_mappings_changes.html#_coercion_of_boolean_fields");
			deprecationInfo.Message.Should().Be("Coercion of boolean fields");
			deprecationInfo.Level.Should().Be(DeprecationWarningLevel.Information);
		}
	}
}

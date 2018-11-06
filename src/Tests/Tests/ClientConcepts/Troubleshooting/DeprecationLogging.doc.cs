using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.ClientConcepts.Troubleshooting
{
	/**
	 * === Deprecation logging
	 *
	 * Elasticsearch will send back `Warn` HTTP Headers when you are using an API feature that is
	 * deprecated and will be removed or rewritten in a future release.
	 *
	 * Elasticsearch.NET and NEST report these back to you so you can log and watch out for them.
	 */
	public class DeprecationLogging : IntegrationDocumentationTestBase, IClusterFixture<ReadOnlyCluster>
	{
		public DeprecationLogging(ReadOnlyCluster cluster) : base(cluster) { }

		[I] public void RequestWithMultipleWarning()
		{
			var response = this.Client.Search<Project>(s => s
				.FielddataFields(fd => fd
					.Field(p => p.State)
					.Field(p => p.NumberOfCommits)
				)
				.ScriptFields(sfs => sfs
					.ScriptField("commit_factor", sf => sf
						.Inline("doc['numberOfCommits'].value * 2")
						.Lang("groovy")
					)
				)
			);

			response.ApiCall.DeprecationWarnings.Should().NotBeNullOrEmpty();

			response.DebugInformation.Should().Contain("Server indicated deprecations:"); // <1> `DebugInformation` also contains the deprecation warnings
		}
	}
}

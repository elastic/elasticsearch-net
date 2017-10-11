using System;
using FluentAssertions;
using Tests.Framework;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
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
			//TODO come up with a new deprecation test since fielddata is gone
			throw new NotImplementedException();

			var response = this.Client.Search<Project>(s => s);

			response.ApiCall.DeprecationWarnings.Should().NotBeNullOrEmpty();

			response.DebugInformation.Should().Contain("Server indicated deprecations:"); // <1> `DebugInformation` also contains the deprecation warnings
        }
	}
}

using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.ClientConcepts
{
	/**== Deprecation Logging
	 * Elasticsearch will send back `Warn` HTTP Headers when you are using an API feature thats deprecated and will soon
	 * be removed or rewritten.
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
			response.DebugInformation.Should().Contain("Server indicated deprecations:");
		}
	}
}

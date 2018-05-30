using System.Linq;
using Elastic.Xunit.Sdk;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Source
{
	public class SourceIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<ReadOnlyCluster>
	{
		public SourceIntegrationTests(ReadOnlyCluster cluster) : base(cluster) { }

		[I] public void SourceReturnsDocument()
		{
			var project = this.Client.Source<Project>(Project.Instance.Name, s=>s.Routing(Project.Routing));
			var p = Project.Projects.FirstOrDefault(i=>i.Name == Project.Instance.Name);
			p.Should().NotBeNull("Test setup failure, project instance not found in projects indexed into readonly cluster");

			project.Name.Should().Be(p.Name);
			project.CuratedTags.Should().HaveCount(p.CuratedTags.Count());
			project.LastActivity.Should().Be(p.LastActivity);
			project.StartedOn.Should().Be(p.StartedOn);
		}
	}
}

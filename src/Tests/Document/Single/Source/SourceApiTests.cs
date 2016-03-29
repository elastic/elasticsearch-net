using System.Linq;
using FluentAssertions;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Document.Single.Source
{
	[Collection(IntegrationContext.ReadOnly)]
	public class SourceIntegrationTests : SimpleIntegration
	{
		public SourceIntegrationTests(ReadOnlyCluster cluster) : base(cluster) { }

		[I] public void SourceReturnsDocument()
		{
			var project = this.Client.Source<Project>(Project.Instance.Name);
			var p = Project.Projects.FirstOrDefault(i=>i.Name == Project.Instance.Name);
			p.Should().NotBeNull("Test setup failure, project instance not found in projects indexed into readonly cluster");

			project.Name.Should().Be(p.Name);
			project.CuratedTags.Should().HaveCount(p.CuratedTags.Count());
			project.LastActivity.Should().Be(p.LastActivity);
			project.StartedOn.Should().Be(p.StartedOn);
		}
	}
}
